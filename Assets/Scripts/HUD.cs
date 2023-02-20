using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
  
  public Hand hand;
  public ArmyDeck armyDeck;
  public SupportDeck supDeck;
  public void  EndTurn () {
    
    GameManager.Instance.playerMaxCardCount=GameManager.Instance.defaultMaxCardCount;// max çekilen kartı default kart çekme sayısına getir
    
    for(int i=0;i<hand.cardCount;i++){
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
    hand.handCard.RemoveRange(0,hand.handCard.Count);
    GameManager.Instance.hand.cardCount=0;
   CardDisplay[] obj= hand.GetComponentsInChildren<CardDisplay>();
   for(int i=0;i<obj.Length;i++){
    Destroy(obj[i].gameObject);
   }
  }

}
