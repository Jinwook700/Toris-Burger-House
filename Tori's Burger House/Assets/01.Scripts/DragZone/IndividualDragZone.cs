using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static IngredientData;

public class IndividualDragZone : DragZone
{
    [SerializeField]
    public List<IngredientType> thisIngredientTypes;

    [Header("Zone 위를 검사할 반경 (작게 설정)")]
    [SerializeField] private float checkRadius = 0.2f;

    void OnTriggerStay2D(Collider2D other)
    {
        Drag item = other.GetComponent<Drag>();

        if (item != null && !item.isDragging && !item.isDragged)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, checkRadius);

            bool alreadyOccupied = false;
            foreach (var hit in hits)
            {
                if (hit == other) continue;

                Drag existing = hit.GetComponent<Drag>();
                if (existing != null)
                {
                    foreach (IngredientType acceptedType in thisIngredientTypes)
                    {
                        if (existing.IngredientType == acceptedType)
                        {
                            alreadyOccupied = true;
                            break;
                        }
                    }
                }
                if (alreadyOccupied) break;
            }

            if (alreadyOccupied) return;

            foreach (IngredientType acceptedType in thisIngredientTypes)
            {
                if (item.IngredientType == acceptedType)
                {
                    Vector3 pos = new Vector3(transform.position.x, transform.position.y, -1f);
                    item.transform.position = pos;
                    item.isDragged = true;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
