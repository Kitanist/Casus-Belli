using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class Dragable : MonoBehaviour
{
    #region Variables
    public Vector3 offset;

    public GameObject CardUI;
 public Vector3 firstPos;

    #endregion

    public void Update()
    {
        

    }
    private void OnMouseDown() {
    offset=transform.position - MouseWorldPos();
    transform.GetComponent<Collider>().enabled=false;
    firstPos=transform.position;
   
    
}
private void OnMouseDrag() {
        
        
        transform.position+=new Vector3(0,0,-0.1f);
        float z=Mathf.Clamp(transform.position.z,-7,-8);
        transform.position=new Vector3(MouseWorldPos().x,MouseWorldPos().y,z);
 

    }
private void OnMouseUp() {
        var rayOrgin=Camera.main.transform.position;
        var rayDirection = MouseWorldPos()-Camera.main.transform.position;
        RaycastHit info;
      
        if(Physics.Raycast(rayOrgin,rayDirection, out info))
        {
            if(info.transform.tag=="Card"){
             transform.DOMove(firstPos,.3f).SetEase(Ease.InBack);
            }
              
            if(info.transform.tag=="DropZone"){
               if(info.transform.gameObject.GetComponent<Hand>())
               {
                if(transform.root!=info.transform){
                    for(int i=0;i<info.transform.gameObject.GetComponent<Hand>().emptySlot.Length;i++){
                    if(info.transform.gameObject.GetComponent<Hand>().emptySlot[i]){
                        transform.DOMove(GameManager.Instance.hand.spawnTransforms[i].position,0.5f).SetEase(Ease.InOutCubic).OnComplete(()=>transform.DORotate(new Vector3(90,0,180),0.05f));
                        
                       // transform.position=GameManager.Instance.hand.spawnTransforms[i].position;
                        info.transform.gameObject.GetComponent<Hand>().emptySlot[i]=false;
                        info.transform.gameObject.GetComponent<Hand>().handCard.Add(gameObject.GetComponent<CardDisplay>().card);
                        transform.parent=info.transform.GetChild(i); //bu slotdaki childinı parentı yaptık
                        break;
                    }
                }
               
                }
                 else{
                    transform.DOMove(firstPos,.3f).SetEase(Ease.InBack);
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
                        
                                 break;
                            }
                        }
                            
                            
                        Debug.Log("esit "+i);
                        break;
                    }
                }
                transform.DOMove(info.transform.position + new Vector3(0,0,-0.01f),.5f).SetEase(Ease.OutBounce).OnComplete(()=>transform.DORotate(new Vector3(90,0,0),0.05f));
               
                transform.parent=info.transform;
                    
                  
                   
                  
                
                }
                
              //  transform.GetComponent<Collider>().enabled=false;
              //tur sonu kitlesin simdi deil
            }
            
        }
        else{
            Debug.Log("ilkpos");
                transform.DOMove(firstPos,.3f).SetEase(Ease.InBack);
               
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
