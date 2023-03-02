using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum TypeCard {
  empty,
  cardPick,
  deckPick,
  
}
public class CardDisplay : MonoBehaviour
{
    public Card card;
    
    public TypeCard typeCard;
    public TMP_Text nameText;
    public TMP_Text descriptionText, descriptionText2;
   
    
    public GameObject ArtworkMat, ClassMat, skill1Mat, skill2Mat,speedMat;
    public int Choosed = 0;
    public bool isBlocked=false;

    
    private void Start()
    {
      Init();
    }
    public void Init () {
        nameText.text = card.name;
        descriptionText.text = card.description1;
        descriptionText2.text = card.description2;
        


        ArtworkMat.GetComponent<Renderer>().material = card.artworkMat;
        ClassMat.GetComponent<Renderer>().material = card.ClassMat;
        skill1Mat.GetComponent<Renderer>().material = card.Skill1Mat;
        skill2Mat.GetComponent<Renderer>().material = card.Skill2Mat;
        speedMat.GetComponent<Renderer>().material = card.Speedmat;
    }
  
}
