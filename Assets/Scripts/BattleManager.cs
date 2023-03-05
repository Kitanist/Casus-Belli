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
    public List<GameObject> allQuands;
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
              playerStrongs[i]+=quads[i].GetComponentInChildren<CardDisplay>().card.strong;
              else
              playerStrongs[i]=0;
            }
        }
        else{
             for(int i = 0; i < quads.Length; i++) {
              if(quads[i].GetComponentInChildren<CardDisplay>())
              otherStrongs[i]+=quads[i].GetComponentInChildren<CardDisplay>().card.strong;
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
         
           
            ChoseSkill(0);
        }
        else if(state==State.battleState){
            BattleStart();
        }

        
    }
    #region Savas
    private void BattleStart()
    {
       
        ChancePowers(quadsPlayer,true);
        ChancePowers(quadsOther,false);
        CalculateTotalPower(true);
        CalculateTotalPower(false);
      

      //for(int i = 0; i < playerStrongs.Length; i++) {
        
         if(playerStrongTotal>otherStrongTotal){// eger player otherdan yuksekse

            //  if(otherStrongs[i]>0){

            for (int j = 0; j < playerStrongs.Length; j++)
            {
                for (int k = 0; k < playerStrongs.Length; k++)
                {
                    if (otherStrongs[j] > playerStrongs[k] && otherStrongs[j] != 0 && playerStrongs[k] != 0)
                    {
                        temp.Add(playerStrongs[k]);
                      
                        // }
                    }
                }
            }
                temp.Sort();// othern herhangi bi karti bizim herhangi bi karttan yuksekse bizim dusuk kalan kartlari hangisi cope gidecek belirlemek için siraliyor

                for(int m = 0; m < playerStrongs.Length; m++) {
                    if(quadsPlayer[m].GetComponentInChildren<CardDisplay>()){
                        if(temp.Count>0)
                        if(quadsPlayer[m].GetComponentInChildren<CardDisplay>().card.strong==temp[0]){// cope gidecek karti belirliyor
                            playerArmyGarbage.AddGarbage(quadsPlayer[m].GetComponentInChildren<CardDisplay>().card);
                        
                              temp.RemoveRange(0,temp.Count); //tüm tempi sıfırla;
                            sequance.Append(quadsPlayer[m].GetComponentInChildren<CardDisplay>().gameObject.transform.DOMove(playerArmyGarbage.hand.GarbageTransform.position, .2f).SetEase(Ease.InCubic));
                            StartCoroutine(DestroyObj(quadsPlayer[m].GetComponentInChildren<CardDisplay>(),.5f));
                            
                            quadsPlayer[m].GetComponentInChildren<CardDisplay>().gameObject.SetActive(false);
                                break;

                            //transform.DOMove(spawnTransforms[handCard.Count-1].position,.2f).SetEase(Ease.InCubic);
                            //yine animasyonlu şekilce çöpe yollanır
                        }
                }

                }
                for(int i = 0; i <otherStrongs.Length ; i++) {
                   
                   
                        if (quadsOther[i].GetComponentInChildren<CardDisplay>())
                        {
                            if (quadsOther[i].GetComponentInChildren<CardDisplay>().card.strong > 0)
                            {

                                otherArmyGarbage.AddGarbage(quadsOther[i].GetComponentInChildren<CardDisplay>().card);
                        // dotween aniamsyonu girer kartı yokk ederiz
                        sequance.Append(quadsOther[i].GetComponentInChildren<CardDisplay>().gameObject.transform.DOMove(otherArmyGarbage.hand.GarbageTransform.position, .2f).SetEase(Ease.InCubic));
                        StartCoroutine(DestroyObj(quadsOther[i].GetComponentInChildren<CardDisplay>(),.7f));
                        
                               
                                
                            

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
                }
                temp.Sort();

                for(int m = 0; m < playerStrongs.Length; m++) {
                    if(quadsOther[m].GetComponentInChildren<CardDisplay>()){
                        if(temp.Count>0)
                        if(quadsOther[m].GetComponentInChildren<CardDisplay>().card.strong==temp[0]){
                            otherArmyGarbage.AddGarbage(quadsOther[m].GetComponentInChildren<CardDisplay>().card);
                            temp.RemoveRange(0,temp.Count);
                             

                                Destroy(quadsOther[m].GetComponentInChildren<CardDisplay>().gameObject);
                            quadsOther[m].GetComponentInChildren<CardDisplay>().gameObject.SetActive(false);
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


        //}
        StartCoroutine(Waiter(1));
        WriteScore();
        resetPowers();
    }
   
    public void GoToDeck () {
    for(int i = 0; i < quadsPlayer.Length; i++) {
        if(quadsPlayer[i].GetComponentInChildren<CardDisplay>()){
                Debug.Log("1");
            if(quadsPlayer[i].GetComponentInChildren<CardDisplay>().card.strong>0){
            CardManager.Instance.playerArmyDeck.deck.Add(quadsPlayer[i].GetComponentInChildren<CardDisplay>().card);
            sequance.Append( quadsPlayer[i].GetComponentInChildren<CardDisplay>().transform.DOMove( CardManager.Instance.playerArmyDeck.hand.firstSpawnPos.position,.5f).SetEase(Ease.OutCubic));
            sequance.Append( quadsPlayer[i].GetComponentInChildren<CardDisplay>().transform.DORotate(new Vector3(90,0,0),0.1f));
            StartCoroutine(DestroyObj(quadsPlayer[i].GetComponentInChildren<CardDisplay>(),.6f));
            }// birlik kartı desteye yolla
            else{
            CardManager.Instance.playerSuprotDeck.deck.Add(quadsPlayer[i].GetComponentInChildren<CardDisplay>().card);
            sequance.Append( quadsPlayer[i].GetComponentInChildren<CardDisplay>().transform.DOMove( CardManager.Instance.playerSuprotDeck.hand.firstSpawnPos.position,.5f).SetEase(Ease.OutCubic));
            sequance.Append( quadsPlayer[i].GetComponentInChildren<CardDisplay>().transform.DORotate(new Vector3(90,0,0),0.1f));
            StartCoroutine(DestroyObj(quadsPlayer[i].GetComponentInChildren<CardDisplay>(),.6f));
            }
           
        }
        
    }
     for(int i = 0; i < quadsOther.Length; i++) {
        if(quadsOther[i].GetComponentInChildren<CardDisplay>()){
                Debug.Log("2");
                if (quadsOther[i].GetComponentInChildren<CardDisplay>().card.strong>0){
            CardManager.Instance.otherArmyDeck.deck.Add(quadsOther[i].GetComponentInChildren<CardDisplay>().card);
            sequance.Append( quadsOther[i].GetComponentInChildren<CardDisplay>().transform.DOMove( CardManager.Instance.otherArmyDeck.hand.firstSpawnPos.position,.5f).SetEase(Ease.OutCubic));
            sequance.Append( quadsOther[i].GetComponentInChildren<CardDisplay>().transform.DORotate(new Vector3(90,0,0),0.1f));
            StartCoroutine(DestroyObj(quadsOther[i].GetComponentInChildren<CardDisplay>(),.6f));
            }// birlik kartı desteye yolla
            else{
            CardManager.Instance.otherSuportDeck.deck.Add(quadsOther[i].GetComponentInChildren<CardDisplay>().card);
            sequance.Append( quadsOther[i].GetComponentInChildren<CardDisplay>().transform.DOMove( CardManager.Instance.otherSuportDeck.hand.firstSpawnPos.position,.5f).SetEase(Ease.OutCubic));
            sequance.Append( quadsOther[i].GetComponentInChildren<CardDisplay>().transform.DORotate(new Vector3(90,0,0),0.1f));
            StartCoroutine(DestroyObj(quadsOther[i].GetComponentInChildren<CardDisplay>(),.6f));
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
    #endregion
    private void WriteScore()
    {
        PLayerTotalDisplay.text = playerStrongTotal.ToString();
        EnemyTotalDisplay.text = otherStrongTotal.ToString();
    }

    public void  SkillSort () {

        for(int i = 0; i < quadsPlayer.Length; i++) {
            if(quadsOther[i].GetComponentInChildren<CardDisplay>()){
                allQuands.Add(quadsOther[i]);
            }
            if(quadsPlayer[i].GetComponentInChildren<CardDisplay>()){
                 allQuands.Add(quadsPlayer[i]);
            }
        }



      for(int i = 0; i <allQuands.Count; i++) {  //eğer  i si ve j si de varsa sıralama algoritmasını uygula
        for(int j = 0; j<  allQuands.Count; j++) {
            
                if(allQuands[i].GetComponentInChildren<CardDisplay>().card.fast>allQuands[j].GetComponentInChildren<CardDisplay>().card.fast){
                GameObject tmp = allQuands[i];
                allQuands[i]=allQuands[j];
                allQuands[j]=tmp;
            }
            
           
            
        } 
      }
      state=State.choseSkill;
      StartBattle();
       
    }
    public void ChoseSkill(int i)
    {
        Debug.Log("1");

        Debug.Log(allQuands[i].transform.tag);

        //kartı parlat

        if (allQuands[i].tag == "DropZone")
        {
            // ekran gircek

            Debug.Log("2");
            if (allQuands[i].GetComponentInChildren<CardDisplay>().card.strong > 0 && (allQuands[i].GetComponentInChildren<CardDisplay>().card.typeCard != TypeCard.empty))
            {
                //ORdu ama yeteneği var
                Debug.Log("5");
            }

            else if (TypeCard.empty == allQuands[i].GetComponentInChildren<CardDisplay>().card.typeCard)
            {
                //ordu yeteneksiz vasıfsız luzumusz
                Debug.Log("6");
            }
            else
            {
                // yetenek kartı
                Debug.Log("0");
                SkillSelectionManager.Instance.SkillSelect(allQuands[i].GetComponentInChildren<CardDisplay>());
                if (SkillSelectionManager.Instance.SkillSelecte)
                {
                    // eğer yetenek seçildiyse
                    Debug.Log("3");
                    allQuands[i].GetComponentInChildren<CardDisplay>().Choosed = SkillSelectionManager.Instance.CD.Choosed;
                    //SkillSelectionManager.Instance.SkillSelecte = false;
                    //next 2 falseyi unutma herşey bitince çalışcak üstteki lavuk
                }
                else
                {
                    // eğer yetenek seçilmediyse
                    StartCoroutine(ChooseSkillI(i, 1));
                }
           
            }


        }
        else
        {
            Debug.Log("Enes");
            // yabay zeka
        }
    }


     // else if (TypeCard.cardPick == allQuands[i].GetComponentInChildren<CardDisplay>().typeCard)
     //   {
     //   }
     //   
     //   else if (TypeCard.deckPick == allQuands[i].GetComponentInChildren<CardDisplay>().typeCard)
     //   {
     //   }

        //kar��la�t�ma yabcaz
        public void RefreshGrapichs () {
        
    }
IEnumerator DestroyObj(CardDisplay obj , float a){
    yield return new WaitForSeconds(a);
       if(obj)
       Destroy(obj.gameObject);
   
  }
    IEnumerator Waiter(int a)
    {
        yield return new WaitForSeconds(a);
        GoToDeck();
    }
   public  IEnumerator ChooseSkillI(int i,int a)
    {
       yield return new WaitForSeconds(a);
         if (!SkillSelectionManager.Instance.SkillSelecte)
        {
            ChoseSkill(i);
        }
        
        
    }
}