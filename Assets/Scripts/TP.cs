using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TP : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    private Vector2 origin,direction,smoothDirection;
    public float smoothing;

    private int pointerID;
    private bool touched;

    private void Awake(){
        direction = Vector2.zero;
        touched = false;
    }
    
    public void OnPointerDown(PointerEventData eventData){
        if(!touched){
            origin = eventData.position;
            touched = true;
            pointerID = eventData.pointerId;

        }
    }

    public void OnDrag(PointerEventData eventData){

        if(eventData.pointerId == pointerID) {
            Vector2 currentPosition = eventData.position;
            Vector2 directionRaw = currentPosition - origin;
            direction = directionRaw.normalized;
        }
    }

    public void OnPointerUp(PointerEventData eventData){
        if(eventData.pointerId == pointerID) {
            direction = Vector2.zero;
            touched = false;
        } 
    }

    public Vector2 GetDirection(){
        smoothDirection = Vector2.MoveTowards(smoothDirection,direction,smoothing); 
        return smoothDirection;
    }
}
