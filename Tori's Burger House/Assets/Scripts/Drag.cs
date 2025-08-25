using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public Vector2 offset;
    public bool isDragging;
    public bool isDragged;

    private void OnMouseDown()
    {
        isDragging = true;
        isDragged = false;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = (Vector2)transform.position - mousePos;
    }

    private void OnMouseDrag()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = (Vector2)mousePos + offset;
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}
