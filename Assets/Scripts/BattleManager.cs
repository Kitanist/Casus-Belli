using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public enum State {
    sortState,
    choseSkill,

    battleState
}
public class BattleManager :MonoSingeleton<BattleManager>
{

    public GameObject [] quadsPlayer;
    public GameObject [] quadsOther;

    public Garbage playerArmyGarbage,playerSupportGarbage;
    public Garbage otherArmyGarbage,otherSupportGarbage;
    public GameObject [] allQuands=new GameObject [8];
    public State state=State.sortState;
    public int playerStrongTotal,otherStrongTotal;
    public int []playerStrongs=new int[4];
    public int []otherStrongs=new int[4];

    public bool prensIsBlockedPlayer=false;
      public bool prensIsBlockedOther=false;
    [HideInInspector] private List<int>temp =new List<int>();
    private void Start() {
       
        for(int i = 0; i < quadsPlayer.Length; i++) {
            allQuands[i]=quadsPlayer[i];
            allQuands[i+1]=quadsOther[i];
        }
    }
    public void ChancePowers (GameObject [] quads,bool isPlayer) {
          if(isPlayer)
        {
            for(int i = 0; i < quads.Length; i++) {
              if(quads[i].GetComponentInChildren<CardDisplay>())
              playerStrongs[i]=quads[i].GetComponentInChildren<CardDisplay>().card.strong;
              else
              playerStrongs[i]=0;
            }
        }
        else{
             for(int i = 0; i < quads.Length; i++) {
              if(quads[i].GetComponentInChildren<CardDisplay>())
              otherStrongs[i]=quads[i].GetComponentInChildren<CardDisplay>().card.strong;
              else
              otherStrongs[i]=0;
            }
        }
    }
    public void CalculateTotalPower(bool isPlayer) {
        if(isPlayer)
        {
            for(int i = 0; i < playerStrongs.Length; i++) {
                playerStrongTotal+=playerStrongs[i];
            }
        }
        else{
            for(int i = 0; i < otherStrongs.Length; i++) {
               otherStrongTotal+=otherStrongs[i];
            }
        }
        
    }
      public void StartBattle () {
      

        if(state==State.sortState){
         BattleManager.Instance.ChancePowers(quadsPlayer,true);
         BattleManager.Instance.ChancePowers(quadsOther,false);
            SkillSort();
        }
        else if(state==State.choseSkill){
            ChoseSkill();
        }
        else if(state==State.battleState){
            BattleStart();
        }

        
    }

    private void BattleStart()
    {
       
        ChancePowers(quadsPlayer,true);
        ChancePowers(quadsOther,false);
        CalculateTotalPower(true);
        CalculateTotalPower(false);
      

      for(int i = 0; i < playerStrongs.Length; i++) {
        
         if(playerStrongTotal>otherStrongTotal){
           
            if(otherStrongs[i]>0){
                for(int j = 0; j < playerStrongs.Length; j++) {
                   for(int k = 0; k < playerStrongs.Length ; k++) {
                    if(otherStrongs[j]>playerStrongs[k]){
                      temp.Add(playerStrongs[k]);
                    }
                   }
                }
                temp.Sort();

                for(int m = 0; m < playerStrongs.Length; m++) {
                    if(quadsPlayer[m].GetComponentInChildren<CardDisplay>()){
                        if(quadsPlayer[m].GetComponentInChildren<CardDisplay>().card.strong==temp[0]){
                            playerArmyGarbage.AddGarbage(quadsPlayer[m].GetComponentInChildren<CardDisplay>().card);
                            quadsPlayer[m].GetComponentInChildren<CardDisplay>().transform.SetParent( playerArmyGarbage.transform);

                            //yine animasyonlu şekilce çöpe yollanır
                        }
                    }

                }
                

                 otherArmyGarbage.AddGarbage(quadsOther[i].GetComponentInChildren<CardDisplay>().card);
                 // dotween aniamsyonu girer kartı yokk ederiz
            }
            
           
        }
        else if(playerStrongTotal==otherStrongTotal){

            if(playerStrongs[i]>0){
                CardManager.Instance.playerArmyDeck.deck.Add(quadsPlayer[i].GetComponentInChildren<CardDisplay>().card);
                quadsPlayer[i].GetComponentInChildren<CardDisplay>().transform.DOMove( CardManager.Instance.playerArmyDeck.hand.firstSpawnPos.position,.5f).OnComplete(()=>Destroy(quadsPlayer[i].GetComponentInChildren<CardDisplay>().gameObject));
                 // dotween aniamsyonu girer kartı yokk ederiz
            } 
            if(otherStrongs[i]>0){
                
                 CardManager.Instance.otherArmyDeck.deck.Add(quadsOther[i].GetComponentInChildren<CardDisplay>().card);
                  quadsOther[i].GetComponentInChildren<CardDisplay>().transform.DOMove( CardManager.Instance.otherArmyDeck.hand.firstSpawnPos.position,.5f).OnComplete(()=>Destroy(quadsOther[i].GetComponentInChildren<CardDisplay>().gameObject));
                 // dotween aniamsyonu girer kartı yokk ederiz
            }


        }
        else{
             if(playerStrongs[i]>0){
                  for(int j = 0; j < playerStrongs.Length; j++) {
                   for(int k = 0; k < playerStrongs.Length ; k++) {
                    if(playerStrongs[j]>otherStrongs[k]){
                      temp.Add(otherStrongs[k]);
                    }
                   }
                }
                temp.Sort();

                for(int m = 0; m < playerStrongs.Length; m++) {
                    if(quadsOther[m].GetComponentInChildren<CardDisplay>()){
                        if(quadsOther[m].GetComponentInChildren<CardDisplay>().card.strong==temp[0]){
                            otherArmyGarbage.AddGarbage(quadsOther[m].GetComponentInChildren<CardDisplay>().card);
                             quadsOther[m].GetComponentInChildren<CardDisplay>().transform.SetParent( otherArmyGarbage.transform);
                            //yine animasyonlu şekilce çöpe yollanır
                        }
                    }

                }



                 playerArmyGarbage.AddGarbage(quadsPlayer[i].GetComponentInChildren<CardDisplay>().card);
                 // dotween aniamsyonu girer kartı yokk ederiz
            } 
        }
        temp.RemoveRange(0,temp.Count); //tüm tempi sıfırla;
        
      }
    }

    public void  SkillSort () {
    int lastIndex=7;
    for(int i = 0; i< allQuands.Length; i++) {  //bütün içi dolu olan quadsları dizinin başlarına taşıdım
        if(!allQuands[i].GetComponentInChildren<CardDisplay>()){
            GameObject tmp=allQuands[i];
            allQuands[i]=allQuands[lastIndex];
            allQuands[lastIndex]=tmp;
            i=0;
            lastIndex--;
        }
    }



      for(int i = 0; i <allQuands.Length; i++) {  //eğer  i si ve j si de varsa sıralama algoritmasını uygula
        for(int j = 0; j<  allQuands.Length; j++) {
            if(allQuands[i].GetComponentInChildren<CardDisplay>() && allQuands[j].GetComponentInChildren<CardDisplay>()){
                if(allQuands[i].GetComponentInChildren<CardDisplay>().card.fast<allQuands[j].GetComponentInChildren<CardDisplay>().card.fast){
                GameObject tmp = allQuands[i];
                allQuands[i]=allQuands[j];
                allQuands[j]=tmp;
            }
            }
           
            
        } 
      }
      state=State.choseSkill;
      StartBattle();
       
    }
public void ChoseSkill () {

    for(int i = 0; i < allQuands.Length; i++) {
       if(allQuands[i].tag=="DropZone"){
        // ekran gircek
        //SkillSelectionManager.Instance.SkillSelect(allQuands[i].GetComponentInChildren<CardDisplay>());
        
       }

    }

    for(int i = 0; i < allQuands.Length; i++) {
       // image ler ayarlancak
        if(allQuands[i].GetComponentInChildren<CardDisplay>()){
          
        }
        
       }
}

    //kar��la�t�ma yabcaz
    public void RefreshGrapichs () {
        
    }


}
