using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager :MonoSingeleton<BattleManager>
{
    public GameObject [] quadsPlayer;
    public GameObject [] quadsOther;

    public Garbage playerGarbage;
    public Garbage otherGarbage;
    

    public int playerStrongTotal,otherStrongTotal;
    public int []playerStrongs=new int[4];
    public int []otherStrongs=new int[4];

    public bool prensIsBlockedPlayer=false;
      public bool prensIsBlockedOther=false;

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
    //karþýlaþtýma yabcaz
    public void RefreshGrapichs () {
        
    }

}
