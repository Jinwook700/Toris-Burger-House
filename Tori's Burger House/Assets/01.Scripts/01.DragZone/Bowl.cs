using System.Collections;
using System.Collections.Generic;
using System.Sound;
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
    private int maxCapacity = 3;

    private int currentCount = 0;

    [SerializeField]
    private float stackOffsetY = 0.1f;

    void OnTriggerStay2D(Collider2D other)
    {
        Drag item = other.GetComponent<Drag>();

        if (item != null && !item.isDragging && !item.isDragged)
        {
            if (currentCount >= maxCapacity)
                return;

            foreach (IngredientType acceptedType in thisIngredientTypes)
            {
                if (item.IngredientType == acceptedType)
                {
                    SoundObject _soundObject;
                    _soundObject = Sound.Play("DragEnd", false);
                    _soundObject.SetVolume(1.3f);

                    Vector3 newPos = transform.position + new Vector3(0, stackOffsetY * currentCount, 0);
                    item.transform.position = newPos;
                    item.transform.SetParent(transform);

                    item.isDragged = true;
                    item.canDrag = false;

                    SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
                    if (sr != null)
                    {
                        sr.sortingOrder = currentCount + 4;
                    }

                    currentCount++;

                    IBowlHandler handler = GetComponent<IBowlHandler>();
                    if (handler != null)
                    {
                        handler.OnIngredientAdded((int)item.IngredientType);
                    }

                    break;
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

