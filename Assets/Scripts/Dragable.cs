using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Dragable : MonoBehaviour
{
  public Vector3 offset;
  
  public Hand hand;
 public Vector3 firstPos;
private void OnMouseDown() {
    offset=transform.position - MouseWorldPos();
    transform.GetComponent<Collider>().enabled=false;
    firstPos=transform.position;
   
   /*
    for(int i=0;i<hand.handCard.Count;i++){
        if(hand.handCard[i]==this.gameObject.GetComponent<Card>()){
            Debug.Log(i);
        }
    }*/
}
private void OnMouseDrag() {
        transform.position=MouseWorldPos();

    }
private void OnMouseUp() {
        var rayOrgin=Camera.main.transform.position;
        var rayDirection = MouseWorldPos()-Camera.main.transform.position;
        RaycastHit info;
        if(Physics.Raycast(rayOrgin,rayDirection, out info))
        {
            if(info.transform.tag=="DropZone"){
               if(info.transform.gameObject.GetComponent<Hand>())
               {
                for(int i=0;i<info.transform.gameObject.GetComponent<Hand>().emptySlot.Length;i++){
                    if(info.transform.gameObject.GetComponent<Hand>().emptySlot[i]){

                        transform.position=info.transform.gameObject.GetComponent<Hand>().spawnTransforms[i].position;
                        transform.parent=info.transform;
                        break;
                    }
                }
                

               }
                
                else{
               // hand.handCard.RemoveAt();    
                for(int i=0;i<hand.handCard.Count;i++){
                    if(hand.handCard[i].name==GetComponent<CardDisplay>().name){
                        hand.emptySlot[i]=true;
                        
                        Debug.Log("esit "+i);
                    }
                }
                transform.position=info.transform.position;
                }
                Debug.Log("dropzone"); 
              //  transform.GetComponent<Collider>().enabled=false;
              //tur sonu kitlesin simdi deil
            }
            
        }
        else{
                transform.position=firstPos;
                     
            }
               transform.GetComponent<Collider>().enabled=true;   
        
    }
private Vector3 MouseWorldPos()
    {
    var mouseScreenPos= Input.mousePosition;
    mouseScreenPos.z=Camera.main.WorldToScreenPoint(transform.position).z;
    return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    
}
