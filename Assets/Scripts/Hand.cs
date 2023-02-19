using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hand : MonoBehaviour
{

    public List<Card> handCard;
    public GameObject CardPrefab;
 
    public TMP_Text nameText;

    public Transform [] spawnTransforms;
    public bool [] emptySlot;

   public void DrawCardToHand () {
    //CardPrefabı Son Çekilen eldeki karta göre ayarlancak
  
    GameObject drawedCard= Instantiate(CardPrefab,spawnTransforms[handCard.Count-1]);
   
    
       nameText=drawedCard.GetComponentInChildren<TMP_Text>();
      nameText.text=handCard[handCard.Count-1].name;
      
    drawedCard.transform.position=spawnTransforms[handCard.Count-1].position;
    drawedCard.transform.parent=this.transform;
    emptySlot[handCard.Count-1]=false;
    
   }
}
