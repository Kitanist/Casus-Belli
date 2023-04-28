using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoSingeleton<BuffManager>
{
    
    public bool king1IsOpen=false;
    public bool king2IsOpen=false;

    public int king1Stack=0;
    public void ActiveBuff (int id,int chosed) {
        if(id==18){
            if(chosed==1){
                king1IsOpen=true;
                king1Stack+=3; // 3 ekliyoruz her açtıgımızda
            }
             if(chosed==2){
                king2IsOpen=true;
            }        
        }
    }
     public void ActiveBuff (int id) {
        if(id==7075){
                 
        }
    }
    public void UseBuff (int id) {
        if(id==18){
            if(king1IsOpen){
                if(king1Stack>0){
                  king1Stack--;
                }  // her tur buff kulanıldığında biriken stack azalcak
                else{
                  king1IsOpen=false;
                }
            // tur sonu kukkanılmalı
            }
            if(king2IsOpen){

            // tur başı kullanılmalı
            }
        }
    }

}
//buffların kullanıldığı zaman farklılıkları burda önemli