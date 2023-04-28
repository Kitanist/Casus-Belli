using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Enemy : MonoBehaviour
{
    public ArmyDeck ad;
    public GameObject []quads;
    public List<GameObject>drawedCard;
     void Start()
    {
       StartCoroutine(DrawHandCard());
       StartCoroutine(Delay());
    }

  public IEnumerator  DrawHandCard () {
    if(ad.deck.Count>=2){
        yield return new WaitForSeconds(0.1f);
        ad.DrawCard();
        yield return new WaitForSeconds(2);
        ad.DrawCard();
    }
     else if(ad.deck.Count==1){
        ad.DrawCard();
    
    }
    else {
        Debug.Log("LoseGame");
    }
    yield return null;
   }

   public IEnumerator Delay (){

    yield return new WaitForSeconds (5);
    PlayCard();
   }
   public void  PlayCard () {
    Debug.Log("ZAAAAAAAAAAAAAAAAAA");
   for(int i = 0; i <  ad.hand.transform.childCount; i++) {
   if(ad.hand.transform.GetChild(i).transform.childCount >0)
   if(ad.hand.transform.GetChild(i).transform.GetChild(0).GetComponent<CardDisplay>().gameObject != null)
  drawedCard.Add(ad.hand.transform.GetChild(i).transform.GetChild(0).GetComponent<CardDisplay>().gameObject) ;
   }

   for(int i = 0; i < drawedCard.Count; i++) {
    if(i==0){
        if(Random.Range(0,2)==1){
            drawedCard[i].transform.SetParent(quads[i].transform);
            drawedCard[i].transform.localPosition=new Vector3(0,0,-0.1f);
        }
    }
    else{
         drawedCard[i].transform.SetParent(quads[i].transform);
          drawedCard[i].transform.localPosition=new Vector3(0,0,-0.1f);
    }
   
   }
   }
   public void  HandReset () {
    ad.hand.cardCount=0;
    ad.hand.handCard.Clear();
    drawedCard.Clear();
    for(int i = 0; i < ad.hand.emptySlot.Length ; i++) {
    ad.hand.emptySlot[i]=true;   //hepsibi boşaltmak lazım
   }
   }
}
