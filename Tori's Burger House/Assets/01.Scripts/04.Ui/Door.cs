using System.Collections;
using System.Collections.Generic;
using System.Sound;
using UnityEngine;

/// <summary>
/// ����� ���� ���� Door Class
/// </summary>
public class Door : MonoBehaviour
{
    public List<GameObject> ingredients = new List<GameObject>();

    [Header("Sprite Setting")]
    [SerializeField] private SpriteRenderer otherFridgeSpriteRenderer;
    [SerializeField] private Sprite openedSprite;
    [SerializeField] private Sprite originalSprite;

    [Header("Value Setting")]
    [SerializeField] private float rightMove;
    [SerializeField] private int spendMoney;
    [SerializeField] private float time;

    //���� ����
    private bool isOpened = false;

    //��ġ & ���� ����
    private Vector3 originalPosition;


    void Start()
    {
        originalPosition = transform.position;

        if (otherFridgeSpriteRenderer != null)
        {
            originalSprite = otherFridgeSpriteRenderer.sprite;
        }
    }

    public void OnMouseDown()
    {
        if (!isOpened)
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    /// <summary>
    /// ����� �� ����
    /// </summary>
    private void OpenDoor()
    {
        SoundObject _soundObject;
        _soundObject = SoundPlayer.Play("DoorSound", false);
        _soundObject.SetVolume(1.3f);

        transform.position = originalPosition + new Vector3(rightMove, 0, 0);

        if (otherFridgeSpriteRenderer != null && openedSprite != null)
        {
            otherFridgeSpriteRenderer.sprite = openedSprite;
        }

        isOpened = true;

        SetIngredientsDraggable(true);

        StartCoroutine(DecreaseGoldOverTime());
    }
    
    /// <summary>
    /// ����� �� �ݱ�
    /// </summary>
    private void CloseDoor()
    {
        SoundObject _soundObject;
        _soundObject = SoundPlayer.Play("DoorSound", false);
        _soundObject.SetVolume(1.3f);

        transform.position = originalPosition;

        if (otherFridgeSpriteRenderer != null && originalSprite != null)
        {
            otherFridgeSpriteRenderer.sprite = originalSprite;
        }

        isOpened = false;

        SetIngredientsDraggable(false);

        StopAllCoroutines();
    }

    /// <summary>
    /// ����������� �� ��������
    /// </summary>
    private IEnumerator DecreaseGoldOverTime()
    {
        while (isOpened)
        {
            yield return new WaitForSeconds(time);
            if (GoldManager.Instance != null && isOpened)
            {
                GoldManager.Instance.AddGold(-spendMoney);
            }
        }
    }

    /// <summary>
    /// ������� �������� �� ��� Drag �Ұ����ϰ� ����
    /// </summary>
    /// <param name="canDrag"></param>
    private void SetIngredientsDraggable(bool canDrag)
    {
        foreach (GameObject ingredient in ingredients)
        {
            if (ingredient != null)
            {
                Ingredient ing = ingredient.GetComponent<Ingredient>();
                if (ing != null)
                {
                    ing.canDrag = canDrag;
                }
            }
        }
    }
}
