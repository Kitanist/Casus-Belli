using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUIDisplay : MonoBehaviour
{
    public RectTransform rt,rt1,rt2;
    public bool ui;
    public GameObject KartUI;
    public Image artworkImage, ClassImage, skill1Image, skill2Image;
    public Card card;
    public TMP_Text nameText;
    public TMP_Text descriptionText, descriptionText2;
    public void InitUI()
    {
        ui = true;
            KartUI.SetActive(true);
        nameText.text = card.name;
        descriptionText.text = card.description1;
        descriptionText2.text = card.description2;
        artworkImage.sprite = card.artwork;
        ClassImage.sprite = card.Class;
        skill1Image.sprite = card.skill1;
        skill2Image.sprite = card.skill2;
        


    }
}
