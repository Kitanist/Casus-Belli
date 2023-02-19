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
    
    public Image artworkImage,ClassImage,skill1Image,skill2Image;
    public GameObject ArtworkMat, ClassMat, skill1Mat, skill2Mat;

    private void Start()
    {
        
        nameText.text = card.name;
        descriptionText.text = card.description1;
        descriptionText2.text = card.description2;
        Debug.Log(card.cardID);
        //   artworkImage.sprite = card.artwork;
        //   ClassImage.sprite = card.Class;
        //   skill1Image.sprite = card.skill1;
        //   skill2Image.sprite = card.skill2;
        ArtworkMat.GetComponent<Renderer>().material = card.artworkMat;
        ClassMat.GetComponent<Renderer>().material = card.ClassMat;
        skill1Mat.GetComponent<Renderer>().material = card.Skill1Mat;
        skill2Mat.GetComponent<Renderer>().material = card.Skill2Mat;
    }
}
