using System.Collections.Generic;
using UnityEngine;
using static IngredientData;

public class CupZone : MonoBehaviour
{
    [Header("���� ������ �� ��ü�� ������ (�ݶ�, ���̴�, �������꽺 ��)")]
    [SerializeField] private GameObject drinkPrefab;

    [Header("���� �� �ִ� IngredientType (�⺻������ Cup)")]
    [SerializeField] public List<IngredientType> thisIngredientTypes;

    private void OnTriggerStay2D(Collider2D other)
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

                    // ��ü ������ ����
                    if (drinkPrefab != null)
                    {
                        item.SetChangePrefab(drinkPrefab);
                    }
                }
            }
        }
    }
}
