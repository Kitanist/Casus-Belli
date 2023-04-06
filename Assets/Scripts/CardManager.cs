using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardManager : MonoSingeleton<CardManager>
{
   
   public ArmyDeck playerArmyDeck ,otherArmyDeck;
   public SupportDeck playerSuprotDeck,otherSuportDeck;
   public GameObject cardPrefab;
   public void UseSkill (int id , int Chosen,GameObject [] myQuads,GameObject [] quads ,bool isPlayer ,GameObject chosenCard) {
    // destek
      bool canUseSkill=false;
   if(isPlayer){
     for(int i = 0; i <BattleManager.Instance.quadsPlayer.Length ; i++) {
        if( BattleManager.Instance.quadsPlayer[i].GetComponentInChildren<CardDisplay>())
       if( BattleManager.Instance.quadsPlayer[i].GetComponentInChildren<CardDisplay>().card.cardID==id)
        canUseSkill=true;
    }
   }
   else{

     for(int i = 0; i <BattleManager.Instance.quadsOther.Length ; i++) {
        if( BattleManager.Instance.quadsPlayer[i].GetComponentInChildren<CardDisplay>())
       if( BattleManager.Instance.quadsOther[i].GetComponentInChildren<CardDisplay>().card.cardID==id)
        canUseSkill=true;
    }
   }
    // birlik
     
    if(canUseSkill){ //eğer kart yok edilmediyse kullan
        
        switch(id){
            case 16:
            for(int i = 0; i < quads.Length; i++) {
                if(Chosen==1){
               
                        //destek kartını yok et
                    if(isPlayer){
                     BattleManager.Instance.otherSupportGarbage.AddGarbage(chosenCard.GetComponentInChildren<CardDisplay>().card);
                     chosenCard.GetComponentInChildren<CardDisplay>().isBlocked=true;
                     //StartCoroutine(DestroyCard(chosenCard.transform.GetComponentInChildren<CardDisplay>().gameObject,.5f)) ;
                     
                     // animasyonlu bir şekilde çöpe yolla
                    }else{
                     BattleManager.Instance.playerSupportGarbage.AddGarbage(chosenCard.GetComponentInChildren<CardDisplay>().card);
                     chosenCard.GetComponentInChildren<CardDisplay>().isBlocked=true;
                     //StartCoroutine(DestroyCard(chosenCard.transform.GetComponentInChildren<CardDisplay>().gameObject,.5f)) ;
                    }
                    
                    
                    
                }
                else if(Chosen==2){
                    //birlik kartını desteye geri yola
                    if(isPlayer){
                     otherArmyDeck.deck.Add(chosenCard.GetComponentInChildren<CardDisplay>().card);
                      chosenCard.GetComponentInChildren<CardDisplay>().isBlocked=true;
                    chosenCard.transform.DOMove(otherArmyDeck.hand.firstSpawnPos.position,0.5f).SetEase(Ease.InQuad);
                     BattleManager.Instance.ChancePowers(quads,isPlayer);
                   //  StartCoroutine(DestroyCard(chosenCard.transform.GetComponentInChildren<CardDisplay>().gameObject,.5f)) ;
                     Debug.Log("yok ettim");
                       
                    }
                    else{
                    playerArmyDeck.deck.Add(chosenCard.GetComponentInChildren<CardDisplay>().card);
                     chosenCard.GetComponentInChildren<CardDisplay>().isBlocked=true;
                    chosenCard.transform.DOMove(playerArmyDeck.hand.firstSpawnPos.position,0.5f).SetEase(Ease.InQuad);
                      BattleManager.Instance.ChancePowers(quads,isPlayer);
                   //  StartCoroutine(DestroyCard(chosenCard.transform.GetComponentInChildren<CardDisplay>().gameObject,.5f)) ;
                       Debug.Log("yok ettim");
                    }
                    
                    }

               
            }
            break;

        }

    }
   }
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
        
        switch(id){
            case 17:
            
            break;

        }

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
                    BattleManager.Instance.otherSupportGarbage.AddGarbage(quads[i].GetComponentInChildren<CardDisplay>().card);
                       quads[i].GetComponentInChildren<CardDisplay>().transform.SetParent( BattleManager.Instance.otherSupportGarbage.transform);
                       // burda şekilli bir animasyonla kartı yok edicez
                    break;
                    case 20:
                     BattleManager.Instance.otherSupportGarbage.AddGarbage(quads[i].GetComponentInChildren<CardDisplay>().card);
                       quads[i].GetComponentInChildren<CardDisplay>().transform.SetParent( BattleManager.Instance.otherSupportGarbage.transform);
                       // burda şekilli bir animasyonla kartı yok edicez
                    break;
                    case 23:
                     BattleManager.Instance.otherSupportGarbage.AddGarbage(quads[i].GetComponentInChildren<CardDisplay>().card);
                       quads[i].GetComponentInChildren<CardDisplay>().transform.SetParent( BattleManager.Instance.otherSupportGarbage.transform);
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
                    BattleManager.Instance.playerSupportGarbage.AddGarbage(quads[i].GetComponentInChildren<CardDisplay>().card);
                       quads[i].GetComponentInChildren<CardDisplay>().transform.SetParent( BattleManager.Instance.playerSupportGarbage.transform);
                       // burda şekilli bir animasyonla kartı yok edicez
                    break;
                    case 20:
                     BattleManager.Instance.playerSupportGarbage.AddGarbage(quads[i].GetComponentInChildren<CardDisplay>().card);
                       quads[i].GetComponentInChildren<CardDisplay>().transform.SetParent( BattleManager.Instance.playerSupportGarbage.transform);
                       // burda şekilli bir animasyonla kartı yok edicez
                    break;
                    case 23:
                     BattleManager.Instance.playerSupportGarbage.AddGarbage(quads[i].GetComponentInChildren<CardDisplay>().card);
                       quads[i].GetComponentInChildren<CardDisplay>().transform.SetParent( BattleManager.Instance.playerSupportGarbage.transform);
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
        case 5: // Oynadığınızdansonraki tur rakip, destek kartı kullanamaz. burda effectler olmalı abi. effectlerden birisi bu olmalı
           if (isPlayer)
           {
                // efekti rakibe bascak
           }
           else
           {
                // efekti bize bascak
           }   
        break;
                   case 6:
                   
                    for (int i = 0; i < myQuads.Length; i++)
                    {
                        if (myQuads[i].GetComponentInChildren<CardDisplay>().card.cardID != id && myQuads[i].GetComponentInChildren<CardDisplay>().card.strong > 0 && myQuads[i].GetComponentInChildren<CardDisplay>().card.cardID != 8)
                        {// ben değilsem ve ordu varsa ve barbar değilse o 
                            if (isPlayer)
                            {
                                BattleManager.Instance.playerStrongs[i] += 3;
                            }
                            else
                            {
                                BattleManager.Instance.otherStrongs[i] += 3;
                            }
                           
                        }
                    }
                    BattleManager.Instance.CalculateTotalPower(isPlayer);
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
                case 7:
                    for (int i = 0; i < quads.Length; i++)
                    {
                        if (quads[i].GetComponentInChildren<CardDisplay>().card.strong != 0 && quads[i].GetComponentInChildren<CardDisplay>().card.strong %2 == 0 && quads[i].GetComponentInChildren<CardDisplay>().card.cardID != 8)
                        {
                            if (isPlayer)
                            {
                                BattleManager.Instance.otherStrongs[i] = 2;
                            }
                            else
                            BattleManager.Instance.playerStrongs[i] =2;
                        }
                    }
                    BattleManager.Instance.CalculateTotalPower(isPlayer);
                    break;
                case 8:
                    // barbar la bu ne skilli
                    break;
                case 11:
                    for (int i = 0; i < myQuads.Length; i++)
                    {
                        if (myQuads[i].GetComponentInChildren<CardDisplay>().card.strong == 0 && myQuads[i].GetComponentInChildren<CardDisplay>())
                        {
                            // Çöpe gönderme 
                        }
                    }

                    break;
                case 12:
                    for (int i = 0; i < quads.Length; i++)
                    {
                        if (quads[i].GetComponentInChildren<CardDisplay>().card.strong == 0 && quads[i].GetComponentInChildren<CardDisplay>() && quads[i].GetComponentInChildren<CardDisplay>().card.fast == 1)
                        {
                            if (isPlayer)
                            {
                                otherSuportDeck.deck.Add(quads[i].GetComponentInChildren<CardDisplay>().card);
                                Destroy(quads[i].GetComponentInChildren<CardDisplay>());
                                //animasyon eklencek
                            }
                            else
                            {
                                playerSuprotDeck.deck.Add(quads[i].GetComponentInChildren<CardDisplay>().card);
                                Destroy(quads[i].GetComponentInChildren<CardDisplay>());
                            }
                        }
                    }
                    break;
    }
   }

   IEnumerator DestroyCard(GameObject obj,float time){

    yield return new WaitForSeconds(time);
    Destroy(obj.gameObject);
   }
}
