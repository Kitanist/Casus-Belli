using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class SkillSelectionManager : MonoSingeleton<SkillSelectionManager>
{
    
    public bool ui = false;
    public CanvasGroup a1, a2, a3, a4;
    public CardDisplay CD;
    public bool SkillSelecte= false;
    public TextMeshProUGUI Skill1, Skill2;
    private void Start()
    {
        transform.localScale = Vector2.zero;
    }
    public void SkillSelect(CardDisplay CD)
    {
        // cart tiplerini değişmesi gerekiyorsa değiştir
        Debug.Log("7");


if(!ui){
 ui = true;
            Skill1.text = CD.card.description1;
            Skill2.text = CD.card.description2;
            transform.LeanScale(Vector2.one, 1f).setEaseOutQuint();
        a1.LeanAlpha(1f, 1f);
        a2.LeanAlpha(1f, 1f);
       //a3.LeanAlpha(1f, 1.5f);
       // a4.LeanAlpha(1f, 1.5f);
     this.CD = CD;
}
          
       
       
    }
    public void CloseUI()
    {
        
        if(ui){
            ui = false;
        transform.LeanScale(Vector2.zero, 1f).setEaseInQuint();
        a1.LeanAlpha(0f, .2f);
        a2.LeanAlpha(0f, .2f);
       // a3.LeanAlpha(0f, .2f);
       // a4.LeanAlpha(0f, .2f);
        }
   
      
    }
    
    public void SkillSelected(int a)
    {
        CD.Choosed = a;
        StopCoroutine(BattleManager.Instance.ChooseSkillI(0,0));
        CloseUI();
        SkillController();
        SkillSelecte = true;
    }
    
    public void SkillController()
    {

        switch (CD.card.cardID)
        {
            case 16:
                break;
            case 17:
                if (CD.Choosed == 1)
                {

                }
                else
                {
                    CD.card.typeCard = TypeCard.effect;
                }
                break;
            default:
                break;
        }
    }
    
   
}
