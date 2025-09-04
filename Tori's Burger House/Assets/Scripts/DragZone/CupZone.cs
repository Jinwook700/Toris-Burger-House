using System.Collections.Generic;
using UnityEngine;
using static IngredientData;

public class CupZone : MonoBehaviour
{
    [Header("���� ������ �� ��ü�� ������ (�ݶ�, ���̴�, �������꽺 ��)")]
    [SerializeField] private GameObject drinkPrefab;

    [Header("���� �� �ִ� IngredientType (�⺻������ Cup)")]
    [SerializeField] public List<IngredientType> thisIngredientTypes;

    [Header("Zone ���� �˻��� �ݰ� (�۰� ����)")]
    [SerializeField] private float checkRadius = 0.2f;

    private void OnTriggerStay2D(Collider2D other)
    {
        Drag item = other.GetComponent<Drag>();

        if (item != null && !item.isDragging && !item.isDragged)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, checkRadius);

            bool hasCup = false;
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
                            hasCup = true;
                            break;
                        }
                    }
                }
            }

            if (hasCup) return;

            foreach (IngredientType acceptedType in thisIngredientTypes)
            {
                if (item.IngredientType == acceptedType)
                {
                    Vector3 pos = new Vector3(transform.position.x, transform.position.y, -1f);
                    item.transform.position = pos;
                    item.isDragged = true;

                    if (drinkPrefab != null)
                    {
                        item.SetChangePrefab(drinkPrefab);
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
