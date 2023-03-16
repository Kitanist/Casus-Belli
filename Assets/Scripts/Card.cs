using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Card",menuName ="Card")]
public class Card : ScriptableObject
{
    public new string name;
    public string description1, description2;
    public int cardID,strong,fast;
    public TypeCard typeCard;
    public Sprite artwork,Class,skill1,skill2,speed;// kart�n resmini ve 4 k��esindeki yuvalaklar�n iconlar�n� yerle�tirmek i�in
    public Material artworkMat, ClassMat, Skill1Mat, Skill2Mat, Speedmat;

   
   
     
    
}
