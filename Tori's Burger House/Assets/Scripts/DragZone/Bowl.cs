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

    [SerializeField]
    private int maxCapacity = 3; // 최대 수용 개수 (Inspector에서 설정 가능)

    private int currentCount = 0; // 현재 들어온 아이템 개수

    void OnTriggerStay2D(Collider2D other)
    {
        Drag item = other.GetComponent<Drag>();

        if (item != null && !item.isDragging && !item.isDragged)
        {
            // 용량 꽉 찼으면 리턴
            if (currentCount >= maxCapacity)
                return;

            foreach (IngredientType acceptedType in thisIngredientTypes)
            {
                if (item.IngredientType == acceptedType)
                {
                    // 위치 고정
                    item.transform.position = transform.position;
                    item.transform.SetParent(transform);

                    item.isDragged = true;
                    item.canDrag = false;

                    currentCount++; // 현재 개수 증가

                    // BowlController(구체적인 Bowl 스크립트)에 알림
                    IBowlHandler handler = GetComponent<IBowlHandler>();
                    if (handler != null)
                    {
                        handler.OnIngredientAdded((int)item.IngredientType);
                    }

                    break; // 매칭된 타입 찾으면 반복문 종료
                }
            }
        }
    }

    // 외부에서 Bowl 비울 때 (PlateController.ClearPlate 같은 곳에서 호출)
    public void ResetBowl()
    {
        currentCount = 0;
    }

    // 자식들 Destroy되면 currentCount도 자동 초기화
    public void ClearBowlContents()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        currentCount = 0;
    }
}
