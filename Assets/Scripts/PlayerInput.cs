using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour, IDragHandler
{
    public Transform myPlayer;
    private float mouseSense = 175f;
    private float LimitMin = -4.3f;
    private float LimitMax= 4.3f;
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentPos = myPlayer.position;
        currentPos.x = Mathf.Clamp(currentPos.x + (eventData.delta.x / -mouseSense), LimitMin,LimitMax);
        myPlayer.position = new Vector3(currentPos.x, myPlayer.position.y, myPlayer.position.z);
    }



}
