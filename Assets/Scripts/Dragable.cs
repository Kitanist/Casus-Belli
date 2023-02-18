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
    //offset=transform.position - MouseWorldPos();
    transform.GetComponent<Collider>().enabled=false;
    firstPos=transform.position;
    transform.position=MouseWorldPos()+offset;
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
                transform.position=info.transform.position;
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
