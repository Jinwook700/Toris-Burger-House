using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IngredientData;
public interface IBowlHandler
{
    void OnIngredientAdded(int ingredientIndex);
}

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
                    // ��ġ ����
                    item.transform.position = transform.position;

                    item.transform.SetParent(transform);

                    item.isDragged = true;
                    item.canDrag = false;

                    // BowlController(��ü���� Bowl ��ũ��Ʈ)�� �˸�
                    IBowlHandler handler = GetComponent<IBowlHandler>();
                    if (handler != null)
                    {
                        handler.OnIngredientAdded((int)item.IngredientType);
                    }
                }
            }
        }
    }
}
