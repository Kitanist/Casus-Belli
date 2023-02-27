using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum State {
    sortState,
    choseSkill
}
public class BattleManager :MonoSingeleton<BattleManager>
{
    public GameObject [] quadsPlayer;
    public GameObject [] quadsOther;

    public Garbage playerGarbage;
    public Garbage otherGarbage;
    public GameObject [] allQuands=new GameObject [8];
    public State state=State.sortState;
    public int playerStrongTotal,otherStrongTotal;
    public int []playerStrongs=new int[4];
    public int []otherStrongs=new int[4];

    public bool prensIsBlockedPlayer=false;
      public bool prensIsBlockedOther=false;

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
            SkillSort();
        }
        else if(state==State.choseSkill){
            ChoseSkill();
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
