using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AreaButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    private int pointerID;
    private bool touched,canFire;
    
    private void Awake(){
        touched = false;
    }

    public void OnPointerDown(PointerEventData eventData){
        if(!touched){
            canFire = true;
            touched = true;
            pointerID = eventData.pointerId;

        }
    }

    public void OnPointerUp(PointerEventData eventData){
        if(eventData.pointerId == pointerID) {
            canFire = false;
            touched = false;
        } 
    }

    public bool CanFire(){
        return canFire;
    } 
}
