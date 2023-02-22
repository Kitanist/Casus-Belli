using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardManager : MonoSingeleton<CardManager>
{
   
   public ArmyDeck playerArmyDeck ,otherArmyDeck;
   public SupportDeck playerSuprotDeck,otherSuportDeck;
   public GameObject cardPrefab;
   public void UseSkill (int id , int Chosen,GameObject [] myQuads,GameObject [] quads ,bool isPlayer) {
    // destek
      bool canUseSkill=false;
   if(isPlayer){
     for(int i = 0; i <BattleManager.Instance.quadsPlayer.Length ; i++) {
       if( BattleManager.Instance.quadsPlayer[i].GetComponentInChildren<CardDisplay>().card.cardID==id)
        canUseSkill=true;
    }
   }
   else{
     for(int i = 0; i <BattleManager.Instance.quadsOther.Length ; i++) {
       if( BattleManager.Instance.quadsOther[i].GetComponentInChildren<CardDisplay>().card.cardID==id)
        canUseSkill=true;
    }
   }
    // birlik
     
    if(canUseSkill){ //eğer kart yok edilmediyse kullan


    }
   }
   public void UseSkill (int id,GameObject [] myQuads, GameObject [] quads,bool isPlayer) {
    bool canUseSkill=false;
   if(isPlayer){
     for(int i = 0; i <BattleManager.Instance.quadsPlayer.Length ; i++) {
       if( BattleManager.Instance.quadsPlayer[i].GetComponentInChildren<CardDisplay>().card.cardID==id)
        canUseSkill=true;
    }
   }
   else{
     for(int i = 0; i <BattleManager.Instance.quadsOther.Length ; i++) {
       if( BattleManager.Instance.quadsOther[i].GetComponentInChildren<CardDisplay>().card.cardID==id)
        canUseSkill=true;
    }
   }
    // birlik
     
    if(canUseSkill)//eğer kart yok edilmediyse kullan
    switch(id){
        case 1:
        bool IsHapend=false;
        int thisCardIndex=0;
        for(int i = 0; i < myQuads.Length; i++) {
            if(myQuads[i].GetComponentInChildren<CardDisplay>().card.cardID!=id &&myQuads[i].GetComponentInChildren<CardDisplay>()){// kendi kartım deiğilse
                if(myQuads[i].GetComponentInChildren<CardDisplay>().card.strong>0){// yanında baska birlik kartı varmı
                 IsHapend=true;
                }
            }
            else{
                thisCardIndex=i;
            }
           
        }
        if(IsHapend){
               if(isPlayer)
                  BattleManager.Instance.playerStrongs[thisCardIndex]=4;
               else
                  BattleManager.Instance.otherStrongs[thisCardIndex]=4;

             BattleManager.Instance.CalculateTotalPower(isPlayer);  
             BattleManager.Instance.RefreshGrapichs();
        }

        break;
        case 2:

           // bu karta düsmanın quadsıını vermemiz gerek onemli !!!!!!!!!

            if(isPlayer){
                
                for(int i = 0; i <quads.Length ; i++) {
                    //kraliyet kartlarını elemece
                    if(quads[i].GetComponentInChildren<CardDisplay>()) {// eger varsa
                      int QuadsId=quads[i].GetComponentInChildren<CardDisplay>().card.cardID;
                   switch(QuadsId){
                    case 6:
                    BattleManager.Instance.prensIsBlockedOther=true;
                    break;
                    case 18:
                    BattleManager.Instance.otherGarbage.AddGarbage(quads[i].GetComponentInChildren<CardDisplay>().card);
                       quads[i].GetComponentInChildren<CardDisplay>().transform.SetParent( BattleManager.Instance.otherGarbage.transform);
                       // burda şekilli bir animasyonla kartı yok edicez
                    break;
                    case 20:
                     BattleManager.Instance.otherGarbage.AddGarbage(quads[i].GetComponentInChildren<CardDisplay>().card);
                       quads[i].GetComponentInChildren<CardDisplay>().transform.SetParent( BattleManager.Instance.otherGarbage.transform);
                       // burda şekilli bir animasyonla kartı yok edicez
                    break;
                    case 23:
                     BattleManager.Instance.otherGarbage.AddGarbage(quads[i].GetComponentInChildren<CardDisplay>().card);
                       quads[i].GetComponentInChildren<CardDisplay>().transform.SetParent( BattleManager.Instance.otherGarbage.transform);
                       // burda şekilli bir animasyonla kartı yok edicez
                    break;
                   }
                    } 
                 
                }
            }
            else{

                 for(int i = 0; i <quads.Length ; i++) {
                    //kraliyet kartlarını elemece
                    if(quads[i].GetComponentInChildren<CardDisplay>()){
                     int QuadsId=quads[i].GetComponentInChildren<CardDisplay>().card.cardID;
                   switch(QuadsId){
                    case 6:
                    BattleManager.Instance.prensIsBlockedPlayer=true;
                    break;
                    case 18:
                    BattleManager.Instance.playerGarbage.AddGarbage(quads[i].GetComponentInChildren<CardDisplay>().card);
                       quads[i].GetComponentInChildren<CardDisplay>().transform.SetParent( BattleManager.Instance.playerGarbage.transform);
                       // burda şekilli bir animasyonla kartı yok edicez
                    break;
                    case 20:
                     BattleManager.Instance.playerGarbage.AddGarbage(quads[i].GetComponentInChildren<CardDisplay>().card);
                       quads[i].GetComponentInChildren<CardDisplay>().transform.SetParent( BattleManager.Instance.playerGarbage.transform);
                       // burda şekilli bir animasyonla kartı yok edicez
                    break;
                    case 23:
                     BattleManager.Instance.playerGarbage.AddGarbage(quads[i].GetComponentInChildren<CardDisplay>().card);
                       quads[i].GetComponentInChildren<CardDisplay>().transform.SetParent( BattleManager.Instance.playerGarbage.transform);
                       // burda şekilli bir animasyonla kartı yok edicez
                    break;
                   }
                    }
                  
                }

            }

        break;
        case 3:
        bool use=true;
        int index=0;
        for(int i = 0; i < myQuads.Length; i++) {
             
             if(myQuads[i].GetComponentInChildren<CardDisplay>().card.cardID!=id && myQuads[i].GetComponentInChildren<CardDisplay>()){ // ben değilsem
                 // ve yinede varsam kullanma yeteneği
                 use=false;
             }
             else
             index=i;

        }
        if(use){
            if(isPlayer)
                  BattleManager.Instance.playerStrongs[index]=6;
               else
                  BattleManager.Instance.otherStrongs[index]=6;
        }
        BattleManager.Instance.CalculateTotalPower(isPlayer);
        
        break;
        case 4:
        if(isPlayer){
          Card p_tmpCard=playerArmyDeck.deck[0];
          playerArmyDeck.deck.RemoveAt(0); //listeden çıkar
          GameObject obj=Instantiate(cardPrefab,playerArmyDeck.hand.firstSpawnPos);
          obj.GetComponent<CardDisplay>().Init(); // oluşan objenin özeliklerini atadık
          for(int i = 0; i <myQuads.Length; i++) {
            if(myQuads[i].transform.childCount==0) // boş bir quad alanı bul
            obj.transform.DOMove(myQuads[i].transform.position,.5f).SetEase(Ease.OutSine).OnComplete(()=>obj.transform.DORotate(new Vector3(90,0,180),.1f).OnComplete(()=>obj.transform.parent=myQuads[i].transform));
            UseSkill(obj.GetComponent<CardDisplay>().card.cardID,myQuads,quads,isPlayer);  // çekilen kart skillerini uygular
            break; //boş bi slota spawnlatım parentını değiştirdim
          }
          
        }
        else{
           Card p_tmpCard=otherArmyDeck.deck[0];
          otherArmyDeck.deck.RemoveAt(0); //listeden çıkar
          GameObject obj=Instantiate(cardPrefab,otherArmyDeck.hand.firstSpawnPos);
          obj.GetComponent<CardDisplay>().Init();
          for(int i = 0; i <myQuads.Length; i++) {
            if(myQuads[i].transform.childCount==0) // boş bir quad alanı bul
            obj.transform.DOMove(myQuads[i].transform.position,.5f).SetEase(Ease.OutSine).OnComplete(()=>obj.transform.DORotate(new Vector3(90,0,180),.1f).OnComplete(()=>obj.transform.parent=myQuads[i].transform));
            UseSkill(obj.GetComponent<CardDisplay>().card.cardID,myQuads,quads,isPlayer);  // çekilen kart skillerini uygular
            break; //boş bi slota spawnlatım parentını değiştirdim
          }

        }
        break;
    }
   }
}
