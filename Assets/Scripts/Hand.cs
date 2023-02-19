using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hand : MonoBehaviour
{

    public List<Card> handCard;
    public GameObject CardPrefab;
 
    

    public Transform [] spawnTransforms;
    public bool [] emptySlot;

   public void DrawCardToHand () {
   
   // oluştur
    GameObject drawedCard= Instantiate(CardPrefab,spawnTransforms[handCard.Count-1]);
    drawedCard.GetComponent<CardDisplay>().card=handCard[handCard.Count-1];//cartın özelliklerini elimdeki son karta göre değiştir
    drawedCard.GetComponent<CardDisplay>().Init();// kartı çalıştır
    
      
    if(emptySlot[handCard.Count-1])  {
    drawedCard.transform.position=spawnTransforms[handCard.Count-1].position;
    emptySlot[handCard.Count-1]=false;
    }
    else{
      for(int i=0;i<emptySlot.Length;i++){
         if(emptySlot[i]){
            drawedCard.transform.position=spawnTransforms[i].position;
            drawedCard.transform.parent=this.transform;
            emptySlot[i]=false;
            break;
         }
      }
    }
  
    
   }
}
