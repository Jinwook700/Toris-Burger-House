using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public Vector2 offset;
    public bool isDraging;

    private void OnMouseDown()
    {
        isDraging = true;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = (Vector2)transform.position - mousePos;
    }

    private void OnMouseDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = (Vector2)mousePos + offset;
    }
}
