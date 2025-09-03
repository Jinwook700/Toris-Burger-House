using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IngredientData;

public class Bowl : DragZone
{
    [SerializeField]
    public List<IngredientType> thisIngredientTypes;

    void OnTriggerStay2D(Collider2D other)
    {
        Drag item = other.GetComponent<Drag>();

        if (item != null && !item.isDragging && !item.isDragged)
        {
            foreach (IngredientType acceptedType in thisIngredientTypes)
            {
                if (item.IngredientType == acceptedType)
                {
                    item.transform.position = transform.position;
                    item.isDragged = true;
                    item.canDrag = false;
                }
            }
        }
    }
}
