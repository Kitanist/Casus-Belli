using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Hand : MonoBehaviour
{

    public List<Card> handCard;
    public GameObject CardPrefab;

   [Header(" Do Tween Animation")]
    #region animatian Region
   public Transform firstSpawnPos;
   public Transform secondSpawnPos;
   
    #endregion  
 
    

    public Transform [] spawnTransforms;
    public bool [] emptySlot;

   public void DrawCardToHand () {
   
   // oluştur
    GameObject drawedCard= Instantiate(CardPrefab,spawnTransforms[handCard.Count-1]);
    drawedCard.GetComponent<CardDisplay>().card=handCard[handCard.Count-1];//cartın özelliklerini elimdeki son karta göre değiştir
    drawedCard.GetComponent<CardDisplay>().Init();// kartı çalıştır

    drawedCard.transform .position=firstSpawnPos.position;
    drawedCard.transform.DOMove(secondSpawnPos.position, 1).SetEase(Ease.OutSine).OnComplete(()=>drawedCard.transform.DORotate(new Vector3(90, 0, 180), .5f).SetEase(Ease.OutSine).OnComplete(()=> SetPositon(drawedCard)));
        //drawedCard.transform.DOMove(secondSpawnPos.position,1).SetEase(Ease.OutSine).OnComplete(()=> SetPositon(drawedCard));
        
            


   }

   public void  SetPositon (GameObject drawedCard) {
       if(emptySlot[handCard.Count-1])  {
      drawedCard.transform.DOMove(spawnTransforms[handCard.Count-1].position,.5f).SetEase(Ease.InCubic);
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
   
}
