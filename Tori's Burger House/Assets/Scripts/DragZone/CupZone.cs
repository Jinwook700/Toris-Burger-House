using System.Collections.Generic;
using UnityEngine;
using static IngredientData;

public class CupZone : MonoBehaviour
{
    [Header("컵이 들어왔을 때 교체될 프리팹 (콜라, 사이다, 오렌지쥬스 등)")]
    [SerializeField] private GameObject drinkPrefab;

    [Header("받을 수 있는 IngredientType (기본적으로 Cup)")]
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

                    // 교체 프리팹 설정
                    if (drinkPrefab != null)
                    {
                        item.SetChangePrefab(drinkPrefab);
                    }
                }
            }
        }
    }
}
