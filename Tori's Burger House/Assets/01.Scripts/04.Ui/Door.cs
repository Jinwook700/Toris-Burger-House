using System.Collections;
using System.Collections.Generic;
using System.Sound;
using UnityEngine;

/// <summary>
/// 냉장고 문을 여는 Door Class
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

    //상태 변수
    private bool isOpened = false;

    //수치 & 로직 제어
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
    /// 냉장고 문 열기
    /// </summary>
    private void OpenDoor()
    {
        SoundObject _soundObject;
        _soundObject = Sound.Play("DoorSound", false);
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
    /// 냉장고 문 닫기
    /// </summary>
    private void CloseDoor()
    {
        SoundObject _soundObject;
        _soundObject = Sound.Play("DoorSound", false);
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
    /// 문열어놓으면 돈 빠져나감
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
    /// 냉장고가 닫혀있을 시 재료 Drag 불가능하게 해줌
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
