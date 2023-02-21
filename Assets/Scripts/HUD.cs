using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HUD : MonoBehaviour
{
  
  public Hand hand;
  public ArmyDeck armyDeck;
  public SupportDeck supDeck;
  private bool playerIsPlay=false;
  public GameObject[] dropAreaObjects;
  
  public void  EndTurn () {
if(hand.cardCount==GameManager.Instance.playerMaxCardCount){
       
       for(int i=0;i<dropAreaObjects.Length;i++){
        if(dropAreaObjects[i].transform.childCount>0){
          playerIsPlay=true; //drop areada kart varmı yani en az bir kart oynandı mı kontrolü
          break;
        }
       }
  if(playerIsPlay){
       GameManager.Instance.playerMaxCardCount=GameManager.Instance.defaultMaxCardCount;// max çekilen kartı default kart çekme sayısına getir
    
    for(int i=0;i<hand.handCard.Count;i++){
      
        Card tmpCard=hand.handCard[i];
        if(tmpCard.strong > 0){
            //birlik kartıdır
            armyDeck.deck.Add(tmpCard);
          
        }
        else{
            //destek kartıdır
            supDeck.deck.Add(tmpCard);
        
        }
    }
    hand.handCard.RemoveRange(0,hand.handCard.Count);// eldeki kartları temizle
    GameManager.Instance.hand.cardCount=0;//çekilen kart sayısını sıfırla
   CardDisplay[] obj= hand.GetComponentsInChildren<CardDisplay>();
   playerIsPlay=false;
   for(int i=0;i<obj.Length;i++){
    if(obj[i]){
   obj[i].transform.DORotate(new Vector3(90,0,0),.1f);
   obj[i].transform.DOMove(hand.firstSpawnPos.position,0.5f).SetEase(Ease.InCubic);
    }
   
   }
   StartCoroutine(DestroyObj(obj));//objlerin yok edilmesi için  sahne düzenlendi
   
   for(int i = 0; i < hand.emptySlot.Length ; i++) {
    hand.emptySlot[i]=true;   //hepsibi boşaltmak lazım
   }
       }
       else{
        Debug.Log("EN an bir kart oynamalı");
       }
       
    }
    else{

      Debug.Log("kart çekilmeden tur bitiremezsiniz");
    }


   
  } 
  IEnumerator DestroyObj(CardDisplay[]obj){
    yield return new WaitForSeconds(.5f);
    for(int i = 0; i < obj.Length; i++) {
      if(obj[i])
       Destroy(obj[i].gameObject);
   }
  }

}
