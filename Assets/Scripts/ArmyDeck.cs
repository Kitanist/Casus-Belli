using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyDeck : Deck
{
    // Start is called before the first frame update
    void Start()
    {
         Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)){
            Shuffle();
            Debug.Log("kar0");
        }
    }
}
