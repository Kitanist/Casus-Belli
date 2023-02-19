using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hand : MonoBehaviour
{

    public List<Card> handCard;
    public GameObject CardPrefab;
 
    public TMP_Text nameText;

    public Transform spawnTransform;

   public void DrawCardToHand () {
    //CardPrefabı Son Çekilen eldeki karta göre ayarlancak
  
    GameObject drawedCard= Instantiate(CardPrefab,spawnTransform);
    
       nameText=drawedCard.GetComponentInChildren<TMP_Text>();
      nameText.text=handCard[handCard.Count-1].name;
      
    drawedCard.transform.position=spawnTransform.position+new Vector3(handCard.Count*1f,0,0);
    drawedCard.transform.parent=this.transform;
    
   }
}
