using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IngredientData;

public class Drag : MonoBehaviour
{
    public IngredientType IngredientType;

    public Vector2 offset;
    public bool isDragging;
    public bool isDragged;
    public bool canDrag;

    private void Update()
    {
        if (!isDragging &&  !isDragged)
        {
            StartCoroutine(DestoryThis());
        }
    }
    private void OnMouseDown()
    {
        if (canDrag)
        {
            isDragging = true;
            isDragged = false;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = (Vector2)transform.position - mousePos;
        }
    }

    private void OnMouseDrag()
    {
        if (canDrag)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = (Vector2)mousePos + offset;
        }
            
    }

    void OnMouseUp()
    {
        if (canDrag)
        {
            isDragging = false;
        }
    }

    IEnumerator DestoryThis()
    {
        yield return new WaitForSeconds(0.05f);
        if (!isDragged)
        {
            Destroy(gameObject);
        }
    }
}
