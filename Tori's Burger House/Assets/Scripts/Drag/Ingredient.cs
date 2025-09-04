using System.Collections;
using System.Collections.Generic;
using System.Sound;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField]
    public GameObject ingredientPrefab;

    public GameObject currentIngredient;

    public bool canDrag = true;

    private void OnMouseDown()
    {
        if (ingredientPrefab != null && canDrag)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentIngredient = Instantiate(ingredientPrefab, mousePos, Quaternion.identity);

            Drag drag = currentIngredient.GetComponent<Drag>();
            if (drag != null)
            {
                drag.isDragging = true;
                drag.isDragged = false;

                SoundObject _soundObject;
                _soundObject = Sound.Play("DragStart", false);
                _soundObject.SetVolume(1.3f);

                drag.offset = (Vector2)currentIngredient.transform.position - mousePos;
            }
        }
    }

    private void OnMouseDrag()
    {
        if (currentIngredient != null && canDrag)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentIngredient.transform.position = (Vector2)mousePos
                                                   + currentIngredient.GetComponent<Drag>().offset;
        }
    }

    private void OnMouseUp()
    {
        if (currentIngredient != null && canDrag)
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
