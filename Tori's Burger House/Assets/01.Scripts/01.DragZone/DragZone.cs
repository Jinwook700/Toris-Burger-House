using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        Drag item = other.GetComponent<Drag>();

        if (item != null && !item.isDragging && !item.isDragged)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, -1f);
            item.transform.position = pos;
            item.isDragged = true;
        }
    }
}
