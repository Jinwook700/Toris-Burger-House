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

    [SerializeField]
    private float stackOffsetY = 0.1f; // 아이템 간 y축 간격

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
                    // 쌓이는 위치 계산
                    Vector3 newPos = transform.position + new Vector3(0, stackOffsetY * currentCount, 0);
                    item.transform.position = newPos;
                    item.transform.SetParent(transform);

                    item.isDragged = true;
                    item.canDrag = false;

                    //  SpriteRenderer sortingOrder 적용
                    SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        sr.sortingOrder = currentCount + 4;
                    }

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

    public void ResetBowl()
    {
        currentCount = 0;
    }

    public void ClearBowlContents()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        currentCount = 0;
    }
}

