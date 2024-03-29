using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoSingeleton<GameManager>
{
   public Hand hand;
    public GameObject CardUI;
   
   public int defaultMaxCardCount=2;
   public int playerMaxCardCount=2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        if (Input.GetMouseButtonDown(1))
        {
            var rayOrgin = Camera.main.transform.position;
            var rayDirection = MouseWorldPos() - Camera.main.transform.position;
            RaycastHit info;
            if (Physics.Raycast(rayOrgin, rayDirection, out info))
            {
                if (info.transform.GetComponent<CardDisplay>())
                {
                    CardUI.GetComponent<CardUIDisplay>().card = info.transform.GetComponent<CardDisplay>().card;
                    CardUI.GetComponent<CardUIDisplay>().InitUI();
                }
              
              
            }
        }
        else if(Input.GetMouseButtonDown(0))
        {
            if (CardUI.GetComponent<CardUIDisplay>().ui)
            {
                CardUI.GetComponent<CardUIDisplay>().CloseUI();

            }
        }
    }
   
    private Vector3 MouseWorldPos()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
