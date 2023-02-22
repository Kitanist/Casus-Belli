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
    public CanvasGroup a1,a2,a3,a4;
    private void Start()
    {
        transform.localScale = Vector2.zero;
    }
    public void InitUI()
    {
        ui = true;

        transform.LeanScale(Vector2.one, 1f).setEaseOutQuint() ;
        nameText.text = card.name;
        descriptionText.text = card.description1;
        descriptionText2.text = card.description2;
        artworkImage.sprite = card.artwork;
        ClassImage.sprite = card.Class;
        skill1Image.sprite = card.skill1;
        skill2Image.sprite = card.skill2;
        a1.LeanAlpha(1f,.75f);
        a2.LeanAlpha(1f,.75f);
        a3.LeanAlpha(1f,1.5f);
        a4.LeanAlpha(1f,1.5f);
    }
    public void CloseUI()
    {
        ui = false;
        transform.LeanScale(Vector2.zero, 1f).setEaseInBack();
        a1.LeanAlpha(0f, .2f);
        a2.LeanAlpha(0f, .2f);
        a3.LeanAlpha(0f, .2f);
        a4.LeanAlpha(0f, .2f);
    }
}
