using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Dragable : MonoBehaviour
{
  public Vector3 offset;
  
  
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
                if(transform.root!=info.transform){
                    for(int i=0;i<info.transform.gameObject.GetComponent<Hand>().emptySlot.Length;i++){
                    if(info.transform.gameObject.GetComponent<Hand>().emptySlot[i]){

                        transform.position=info.transform.gameObject.GetComponent<Hand>().spawnTransforms[i].position;
                        info.transform.gameObject.GetComponent<Hand>().emptySlot[i]=false;
                        info.transform.gameObject.GetComponent<Hand>().handCard.Add(gameObject.GetComponent<CardDisplay>().card);
                        transform.parent=info.transform.GetChild(i); //bu slotdaki childinı parentı yaptık
                        break;
                    }
                }
               
                }
                 else{
                    transform.position=firstPos;
                }
                
                

               }
                
                else{
                  
                for(int i=0;i<GameManager.Instance. hand.handCard.Count;i++){
                    if( GameManager.Instance.hand.handCard[i].name==GetComponent<CardDisplay>().card.name){
                        GameManager.Instance.hand.handCard.RemoveAt(i);
                        // eğer parentların konumu aynı ise 
                        for(int j=0;j< GameManager.Instance.hand.emptySlot.Length;j++){
                            if(transform.parent.position== GameManager.Instance.hand.spawnTransforms[j].position){
                                 GameManager.Instance.hand.emptySlot[j]=true;
                                 Debug.Log("konumlar ayni");
                                 break;
                            }
                        }
                            
                            
                        Debug.Log("esit "+i);
                        break;
                    }
                }
                transform.position=info.transform.position;
                transform.parent=info.transform;
                }
                
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
