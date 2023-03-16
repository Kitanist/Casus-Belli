using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{

    public List<Card> handCard;
    public GameObject CardPrefab;
    public int cardCount;
    public TextMeshProUGUI leftCard;
    [Header(" Do Tween Animation")]
    #region animatian Region
   public Transform firstSpawnPos;
   public Transform secondSpawnPos;
   public Transform GarbageTransform;
   
   public Button armyDeckButton;
   public Button supportDeckButton;

    public bool Player=false;
    #endregion


    private void Update()
    {
        writeUI();
    }
    public Transform [] spawnTransforms;
    public bool [] emptySlot;

   public void DrawCardToHand () {
   
   // oluştur
    cardCount++;
    GameObject drawedCard= Instantiate(CardPrefab,spawnTransforms[handCard.Count-1]);
    drawedCard.GetComponent<CardDisplay>().card=handCard[handCard.Count-1];//cartın özelliklerini elimdeki son karta göre değiştir
    drawedCard.GetComponent<CardDisplay>().Init();// kartı çalıştır
  
   drawedCard.GetComponent<CardDisplay>().isPlayer=Player;
       


    drawedCard.transform .position=firstSpawnPos.position;

    //animasyon butonunu inaktif hale getirmeliyiz  1 sn sonrada açarsak sorun kalmaz
      armyDeckButton.enabled=false;
      supportDeckButton.enabled=false;
      drawedCard.GetComponent<Collider>().enabled=false; //bunuda inaktif etmeliyiz ki animasyon oynarken tıklayamasın
      drawedCard.transform.DOMove(secondSpawnPos.position, 1).SetEase(Ease.OutSine).OnComplete(()=>drawedCard.transform.DORotate(new Vector3(90, 0, 180), .5f).SetEase(Ease.OutSine).OnComplete(()=> SetPositon(drawedCard)));
      StartCoroutine(resetForAnimation(drawedCard));
            


   }

   public void  SetPositon (GameObject drawedCard) {
       if(emptySlot[handCard.Count-1])  {
      drawedCard.transform.DOMove(spawnTransforms[handCard.Count-1].position,.2f).SetEase(Ease.InCubic);
      emptySlot[handCard.Count-1]=false;
   // drawedCard.transform.position=spawnTransforms[handCard.Count-1].position;
   
    }
    else{
      for(int i=0;i<emptySlot.Length;i++){
         if(emptySlot[i]){
            drawedCard.transform.DOMove(spawnTransforms[i].position,.5f).SetEase(Ease.InCubic);
           // drawedCard.transform.position=spawnTransforms[i].position;
            drawedCard.transform.parent=this.transform;
            emptySlot[i]=false;
            break;
         }
      }
    }
   }
   IEnumerator resetForAnimation(GameObject dc){

      yield return new WaitForSeconds(1);
      armyDeckButton.enabled=true;
      supportDeckButton.enabled=true;
      dc.GetComponent<Collider>().enabled=true;

   }
    public void writeUI()
    {
        int a = 2;
        a -= cardCount;
        leftCard.text = a.ToString();
        a = 2;
    }
}
