using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;
    
    public TMP_Text nameText;
    public TMP_Text descriptionText, descriptionText2;
   
    
    public GameObject ArtworkMat, ClassMat, skill1Mat, skill2Mat,speedMat;
    public int Choosed = 0;
    private void Start()
    {
      
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
