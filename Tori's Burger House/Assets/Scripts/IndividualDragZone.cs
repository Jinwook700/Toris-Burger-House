using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IngredientData;

// DragZone을 상속받아 기존 기능을 확장합니다.
public class IndividualDragZone : DragZone
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
                    Debug.Log("찰싹! 드롭 성공!");

                    item.transform.position = transform.position;
                    item.isDragged = true;
                }
            } 
        }
    }
}
