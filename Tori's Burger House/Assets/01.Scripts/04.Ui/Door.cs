using System.Collections;
using System.Collections.Generic;
using System.Sound;
using UnityEngine;

/// <summary>
/// 냉장고 문 열기와 닫기 관리
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

    // 문 열림 상태
    private bool isOpened = false;

    // 원래 위치 저장
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
    /// 문 열기
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
    /// 문 닫기
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
    /// 문이 열려 있는 동안 골드 감소
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
    /// 재료 드래그 가능 여부 설정
    /// </summary>
    /// <param name="canDrag">드래그 가능 여부</param>
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
