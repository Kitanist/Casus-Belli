using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSelectionManager : MonoBehaviour
{
    public bool ui;
    public CanvasGroup a1, a2, a3, a4;
    public CardDisplay CD;
    private void Start()
    {
        transform.localScale = Vector2.zero;
    }
    public void SkillSelect(CardDisplay CD)
    {
        ui = true;
        transform.LeanScale(Vector2.one, 1f).setEaseOutQuint();
        a1.LeanAlpha(1f, .75f);
        a2.LeanAlpha(1f, .75f);
        a3.LeanAlpha(1f, 1.5f);
        a4.LeanAlpha(1f, 1.5f);
        this.CD = CD;
    }
    public void CloseUI()
    {
        ui = false;
        transform.LeanScale(Vector2.zero, 1f).setEaseInQuint();
        a1.LeanAlpha(0f, .2f);
        a2.LeanAlpha(0f, .2f);
        a3.LeanAlpha(0f, .2f);
        a4.LeanAlpha(0f, .2f);

    }
    
    public void Skill1Selected()
    {
        CD.Choosed = 1;
        
    }
    public void Skill2Selected()
    {
        CD.Choosed = 2;
    }
}
