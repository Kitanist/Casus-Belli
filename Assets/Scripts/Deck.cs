using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Deck : MonoBehaviour
{
    public List<Card> deck;
    public List<Image> DeckImage;

    public Hand  hand;
 
  
    public void Shuffle () {

        for(int i=0;i<deck.Count;i++){
            var rand=Random.RandomRange(0,deck.Count);
            Card tmp =deck[i];
            deck[i]=deck[rand];
            deck[rand]=tmp;
        }
    }
    public void DrawCard () {
        //çekilen kartı tut listeden çıkar  eline ekle elindeki çekme fonksiyonunu calistir
        Card tmpCard= deck[0];
        deck.RemoveAt(0);
        hand.handCard.Add(tmpCard);
        hand.DrawCardToHand();
        // cekilen kartin imagesini kapa
       Image img= DeckImage[DeckImage.Count-1];
       DeckImage.RemoveAt(DeckImage.Count-1);
       Destroy(img);
       
    }
}
