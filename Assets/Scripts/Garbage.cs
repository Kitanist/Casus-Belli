using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Garbage : Deck
{
    public GameObject imagePrefab;
   public void AddGarbage (Card card) {
    deck.Add(card);
    GameObject imgObj=Instantiate(imagePrefab,this.transform);
    imgObj.transform.parent=this.transform;
    DeckImage.Add(imgObj.GetComponent<Image>());

   }
}
