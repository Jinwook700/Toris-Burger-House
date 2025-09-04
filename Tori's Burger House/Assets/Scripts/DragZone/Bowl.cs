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
                    // ��ġ ����
                    item.transform.position = transform.position;
                    item.transform.SetParent(transform);

                    item.isDragged = true;
                    item.canDrag = false;

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

    // �ܺο��� Bowl ��� �� (PlateController.ClearPlate ���� ������ ȣ��)
    public void ResetBowl()
    {
        currentCount = 0;
    }

    // �ڽĵ� Destroy�Ǹ� currentCount�� �ڵ� �ʱ�ȭ
    public void ClearBowlContents()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        currentCount = 0;
    }
}
