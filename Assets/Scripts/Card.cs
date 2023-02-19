using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Card",menuName ="Card")]
public class Card : ScriptableObject
{
    public new string name;
    public string description1, description2;
    public int cardID,Güç,Hýz;
    
    public Sprite artwork,Class,skill1,skill2,speed;// kartýn resmini ve 4 köþesindeki yuvalaklarýn iconlarýný yerleþtirmek için
    public Material artworkMat, ClassMat, Skill1Mat, Skill2Mat, Speedmat;

    
   
     
    
}
