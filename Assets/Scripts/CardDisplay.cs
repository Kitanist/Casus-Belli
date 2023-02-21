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
    public bool ui;
    public Image artworkImage,ClassImage,skill1Image,skill2Image;
    public GameObject ArtworkMat, ClassMat, skill1Mat, skill2Mat,speedMat,KartUI;

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
    public void InitUI()
    {
        if (ui)
        {
            KartUI.SetActive(true);
            ui = false;
        }
        else if (!ui)
        {
            KartUI.SetActive(false);
            ui = true;
        }
        
            artworkImage.sprite = card.artwork;
            ClassImage.sprite = card.Class;
            skill1Image.sprite = card.skill1;
            skill2Image.sprite = card.skill2;
            nameText.text = card.name;
            descriptionText.text = card.description1;
            descriptionText2.text = card.description2;

    }
}
