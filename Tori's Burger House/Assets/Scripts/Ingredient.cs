using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField]
    public GameObject ingredientPrefab;

    public GameObject currentIngredient;

    private void OnMouseDown()
    {
        if (ingredientPrefab != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentIngredient = Instantiate(ingredientPrefab, mousePos, Quaternion.identity);

            Drag drag = currentIngredient.GetComponent<Drag>();
            if (drag != null)
            {
                drag.isDragging = true;
                drag.isDragged = false;

                drag.offset = (Vector2)currentIngredient.transform.position - mousePos;
            }
        }
    }

    private void OnMouseDrag()
    {
        if (currentIngredient != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentIngredient.transform.position = (Vector2)mousePos
                                                   + currentIngredient.GetComponent<Drag>().offset;
        }
    }

    private void OnMouseUp()
    {
        if (currentIngredient != null)
        {
            Drag drag = currentIngredient.GetComponent<Drag>();
            if (drag != null)
            {
                drag.isDragging = false;
            }
            currentIngredient = null;
        }
    }
}
