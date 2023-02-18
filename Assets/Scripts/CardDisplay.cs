using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public Text nameText;
    public Text descriptionText, descriptionText2;
    
    public Image artworkImage,ClassImage,skill1Image,skill2Image;

    private void Start()
    {
        nameText.text = card.name;
        descriptionText.text = card.description1;
        descriptionText2.text = card.description2;
        Debug.Log(card.cardID);
        artworkImage.sprite = card.artwork;
        ClassImage.sprite = card.Class;
        skill1Image.sprite = card.skill1;
        skill2Image.sprite = card.skill2;

    }
}
