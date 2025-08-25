using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        Drag item = other.GetComponent<Drag>();

        if (item != null && !item.isDragging && item.isDragged == false)
        {
            Debug.Log("Âû½Ï! µå·Ó ¼º°ø!");

            item.transform.position = transform.position;
            item.isDragged = true;
        }
    }
}
