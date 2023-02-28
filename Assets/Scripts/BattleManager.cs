using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public enum State {
    sortState,
    choseSkill,

    battleState
}
public class BattleManager :MonoSingeleton<BattleManager>
{
    #region Variables
    public GameObject [] quadsPlayer;
    public GameObject [] quadsOther;
    public TextMeshProUGUI EnemyTotalDisplay, PLayerTotalDisplay;
    public Garbage playerArmyGarbage,playerSupportGarbage;
    public Garbage otherArmyGarbage,otherSupportGarbage;
    public GameObject [] allQuands=new GameObject [8];
    public State state=State.sortState;
    public int playerStrongTotal,otherStrongTotal;
    public int []playerStrongs=new int[4];
    public int []otherStrongs=new int[4];

    public bool prensIsBlockedPlayer=false;
      public bool prensIsBlockedOther=false;
    public List<int>temp =new List<int>();

    #endregion
     Sequence sequance;
    private void Start() {
        sequance= DOTween.Sequence();
        for(int i = 0; i < quadsPlayer.Length; i++) {
            allQuands[i]=quadsPlayer[i];
            allQuands[i+1]=quadsOther[i];
        }
        WriteScore();
    }
    private void Update()
    {
       
    }
    #region GucHesap
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
    #endregion
    public void StartBattle () {
      

        if(state==State.sortState){
         ChancePowers(quadsPlayer,true);
         ChancePowers(quadsOther,false);
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
      

      //for(int i = 0; i < playerStrongs.Length; i++) {
        
         if(playerStrongTotal>otherStrongTotal){// eger player otherdan yuksekse
           
          //  if(otherStrongs[i]>0){
             
                for(int j = 0; j < playerStrongs.Length; j++) {
                   for(int k = 0; k < playerStrongs.Length ; k++) {
                    if(otherStrongs[j]>playerStrongs[k] && otherStrongs[j]!=0&&playerStrongs[k]!=0){
                      temp.Add(playerStrongs[k]);
                      Debug.Log("1");
                   // }
                   }
                }
                temp.Sort();// othern herhangi bi karti bizim herhangi bi karttan yuksekse bizim dusuk kalan kartlari hangisi cope gidecek belirlemek için siraliyor

                for(int m = 0; m < playerStrongs.Length; m++) {
                    if(quadsPlayer[m].GetComponentInChildren<CardDisplay>()){
                        if(temp.Count>0)
                        if(quadsPlayer[m].GetComponentInChildren<CardDisplay>().card.strong==temp[0]){// cope gidecek karti belirliyor
                            playerArmyGarbage.AddGarbage(quadsPlayer[m].GetComponentInChildren<CardDisplay>().card);
                            Debug.Log("2");
                              temp.RemoveRange(0,temp.Count); //tüm tempi sıfırla;
                                Destroy(quadsPlayer[m].GetComponentInChildren<CardDisplay>().gameObject);
                                break;
                            //yine animasyonlu şekilce çöpe yollanır
                        }
                    }

                }
                for(int i = 0; i <otherStrongs.Length ; i++) {
                    
                  if(quadsOther[i].GetComponentInChildren<CardDisplay>())  {
                    if(quadsOther[i].GetComponentInChildren<CardDisplay>().card.strong>0){
                 
                    otherArmyGarbage.AddGarbage(quadsOther[i].GetComponentInChildren<CardDisplay>().card);
                    // dotween aniamsyonu girer kartı yokk ederiz
                    Destroy(quadsOther[i].GetComponentInChildren<CardDisplay>().gameObject);
                    Debug.Log("3");
                    }
                  
                  }
                 
                }
                    
            }
            
           
        }
        else if(playerStrongTotal==otherStrongTotal){

            for(int i = 0; i < playerStrongs.Length; i++) {
                 if(playerStrongs[i]>0){
                CardManager.Instance.playerArmyDeck.deck.Add(quadsPlayer[i].GetComponentInChildren<CardDisplay>().card);
               sequance.Append(quadsPlayer[i].GetComponentInChildren<CardDisplay>().transform.DOMove( CardManager.Instance.playerArmyDeck.hand.firstSpawnPos.position,.5f).OnComplete(()=> quadsPlayer[i].GetComponentInChildren<CardDisplay>().transform.DORotate(new Vector3(90, 0, 0), .1f).OnComplete(()=> Destroy(quadsPlayer[i].GetComponentInChildren<CardDisplay>().gameObject)))) ;
                    // dotween aniamsyonu girer kartı yokk ederiz
                    

                } 
            }
           
           for(int i = 0; i < otherStrongs.Length; i++) {
             if(otherStrongs[i]>0){
                
                 CardManager.Instance.otherArmyDeck.deck.Add(quadsOther[i].GetComponentInChildren<CardDisplay>().card);// her quadı army deck e eklemeye çalışıyor fixlencek
                sequance.Append(quadsOther[i].GetComponentInChildren<CardDisplay>().transform.DOMove( CardManager.Instance.otherArmyDeck.hand.firstSpawnPos.position,.5f).OnComplete(()=> quadsOther[i].GetComponentInChildren<CardDisplay>().transform.DORotate(new Vector3(90, 0, 0), .1f).OnComplete(() => Destroy(quadsOther[i].GetComponentInChildren<CardDisplay>().gameObject))))  ;
                 // dotween aniamsyonu girer kartı yokk ederiz
            }
           }
           


        }
        else{
            // if(playerStrongs[i]>0){
                  for(int j = 0; j < playerStrongs.Length; j++) {
                   for(int k = 0; k < playerStrongs.Length ; k++) {
                    if(playerStrongs[j]>otherStrongs[k]&& otherStrongs[k]!=0&&playerStrongs[j]!=0){
                      temp.Add(otherStrongs[k]);
                    }
                   }
               // }
                temp.Sort();

                for(int m = 0; m < playerStrongs.Length; m++) {
                    if(quadsOther[m].GetComponentInChildren<CardDisplay>()){
                        if(temp.Count>0)
                        if(quadsOther[m].GetComponentInChildren<CardDisplay>().card.strong==temp[0]){
                            otherArmyGarbage.AddGarbage(quadsOther[m].GetComponentInChildren<CardDisplay>().card);
                            temp.RemoveRange(0,temp.Count);
                             quadsOther[m].GetComponentInChildren<CardDisplay>().transform.SetParent( otherArmyGarbage.transform);

                                Destroy(quadsOther[m].GetComponentInChildren<CardDisplay>().gameObject);
                                //yine animasyonlu şekilce çöpe yollanır
                                break;
                            }
                    }

                }

                for(int i = 0; i <playerStrongs.Length ; i++) {
                    if(quadsPlayer[i].GetComponentInChildren<CardDisplay>()){
                    playerArmyGarbage.AddGarbage(quadsPlayer[i].GetComponentInChildren<CardDisplay>().card);
                    // dotween aniamsyonu girer kartı yokk ederiz
                    Destroy(quadsPlayer[i].GetComponentInChildren<CardDisplay>().gameObject);
                  
                    }
                   
                }

              
                } 
        }
      
        
      //}
        GoToDeck();
        WriteScore();
        resetPowers();
    }
   
    private void GoToDeck () {
    for(int i = 0; i < quadsPlayer.Length; i++) {
        if(quadsPlayer[i].GetComponentInChildren<CardDisplay>()){
            if(quadsPlayer[i].GetComponentInChildren<CardDisplay>().card.strong>0){
            CardManager.Instance.playerArmyDeck.deck.Add(quadsPlayer[i].GetComponentInChildren<CardDisplay>().card);
            sequance.Append( quadsPlayer[i].GetComponentInChildren<CardDisplay>().transform.DOMove( CardManager.Instance.playerArmyDeck.hand.firstSpawnPos.position,.5f).SetEase(Ease.OutCubic));
            sequance.Append( quadsPlayer[i].GetComponentInChildren<CardDisplay>().transform.DORotate(new Vector3(90,0,0),0.1f).OnComplete(()=>Destroy(quadsPlayer[i].GetComponentInChildren<CardDisplay>().gameObject)));
            StartCoroutine(DestroyObj(quadsPlayer[i].GetComponentInChildren<CardDisplay>()));
            }// birlik kartı desteye yolla
            else{
            CardManager.Instance.playerSuprotDeck.deck.Add(quadsPlayer[i].GetComponentInChildren<CardDisplay>().card);
            sequance.Append( quadsPlayer[i].GetComponentInChildren<CardDisplay>().transform.DOMove( CardManager.Instance.playerSuprotDeck.hand.firstSpawnPos.position,.5f).SetEase(Ease.OutCubic));
            sequance.Append( quadsPlayer[i].GetComponentInChildren<CardDisplay>().transform.DORotate(new Vector3(90,0,0),0.1f).OnComplete(()=>Destroy(quadsPlayer[i].GetComponentInChildren<CardDisplay>().gameObject)));
            StartCoroutine(DestroyObj(quadsPlayer[i].GetComponentInChildren<CardDisplay>()));
            }
           
        }
        
    }
     for(int i = 0; i < quadsOther.Length; i++) {
        if(quadsOther[i].GetComponentInChildren<CardDisplay>()){
            if(quadsOther[i].GetComponentInChildren<CardDisplay>().card.strong>0){
            CardManager.Instance.otherArmyDeck.deck.Add(quadsOther[i].GetComponentInChildren<CardDisplay>().card);
            sequance.Append( quadsOther[i].GetComponentInChildren<CardDisplay>().transform.DOMove( CardManager.Instance.otherArmyDeck.hand.firstSpawnPos.position,.5f).SetEase(Ease.OutCubic));
            sequance.Append( quadsOther[i].GetComponentInChildren<CardDisplay>().transform.DORotate(new Vector3(90,0,0),0.1f));
            StartCoroutine(DestroyObj(quadsOther[i].GetComponentInChildren<CardDisplay>()));
            }// birlik kartı desteye yolla
            else{
            CardManager.Instance.otherSuportDeck.deck.Add(quadsOther[i].GetComponentInChildren<CardDisplay>().card);
            sequance.Append( quadsOther[i].GetComponentInChildren<CardDisplay>().transform.DOMove( CardManager.Instance.otherSuportDeck.hand.firstSpawnPos.position,.5f).SetEase(Ease.OutCubic));
            sequance.Append( quadsOther[i].GetComponentInChildren<CardDisplay>().transform.DORotate(new Vector3(90,0,0),0.1f));
            StartCoroutine(DestroyObj(quadsOther[i].GetComponentInChildren<CardDisplay>()));
            }
           
        }
        
    }
    }

    private void resetPowers()
    {
        for (int i = 0; i < playerStrongs.Length; i++)
        {
            playerStrongs[i] = 0;
            otherStrongs[i] = 0;
            playerStrongTotal = 0;
            otherStrongTotal = 0;
        }
    }
    private void WriteScore()
    {
        PLayerTotalDisplay.text = playerStrongTotal.ToString();
        EnemyTotalDisplay.text = otherStrongTotal.ToString();
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
IEnumerator DestroyObj(CardDisplay obj){
    yield return new WaitForSeconds(.6f);
       if(obj)
       Destroy(obj.gameObject);
   
  }

}
