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
    private int maxCapacity = 3; // �ִ� ���� ���� (Inspector���� ���� ����)

    private int currentCount = 0; // ���� ���� ������ ����

    [SerializeField]
    private float stackOffsetY = 0.1f; // ������ �� y�� ����

    void OnTriggerStay2D(Collider2D other)
    {
        Drag item = other.GetComponent<Drag>();

        if (item != null && !item.isDragging && !item.isDragged)
        {
            // �뷮 �� á���� ����
            if (currentCount >= maxCapacity)
                return;

            foreach (IngredientType acceptedType in thisIngredientTypes)
            {
                if (item.IngredientType == acceptedType)
                {
                    // ���̴� ��ġ ���
                    Vector3 newPos = transform.position + new Vector3(0, stackOffsetY * currentCount, 0);
                    item.transform.position = newPos;
                    item.transform.SetParent(transform);

                    item.isDragged = true;
                    item.canDrag = false;

                    //  SpriteRenderer sortingOrder ����
                    SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        sr.sortingOrder = currentCount + 4;
                    }

                    currentCount++; // ���� ���� ����

                    // BowlController(��ü���� Bowl ��ũ��Ʈ)�� �˸�
                    IBowlHandler handler = GetComponent<IBowlHandler>();
                    if (handler != null)
                    {
                        handler.OnIngredientAdded((int)item.IngredientType);
                    }

                    break; // ��Ī�� Ÿ�� ã���� �ݺ��� ����
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

