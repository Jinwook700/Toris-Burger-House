using System.Collections;
using System.Sound;
using UnityEngine;
using static IngredientData;

/// <summary>
/// 드래그 할 수 있는 재료들의 최상단 스크립트
/// </summary>
public class Drag : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] public IngredientType IngredientType;
    [SerializeField] private string soundName;
    [SerializeField] private GameObject changePrefab;
    [SerializeField] protected Sprite normalSprite;
    [SerializeField] protected Sprite changedSprite;

    //내부 컴포넌트
    protected SpriteRenderer spriteRenderer;

    //상태 제어
    public bool firstDrag = false;
    public bool canDrag;
    public bool isDragged;
    public bool isDragging;
    private bool isChange = false;
    private bool spriteChanged = false;

    //수치 & 로직 제어
    public Vector2 offset;
    private float delayTime = 2f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }
    }

    private void Update()
    {
        if (!isDragging && !isDragged)
        {
            StartCoroutine(DestoryThis());
        }

        if (isDragged && isChange)
        {
            isChange = false;
            StartCoroutine(ChangeAfterDelay(delayTime));
        }
    }

    private void OnMouseDown()
    {
        if (canDrag)
        {
            isDragging = true;
            isDragged = false;

            SoundObject _soundObject;
            _soundObject = SoundPlayer.Play("DragStart", false);
            _soundObject.SetVolume(1.3f);

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = (Vector2)transform.position - mousePos;
        }
    }

    private void OnMouseDrag()
    {
        if (!spriteChanged && changedSprite != null)
        {
            spriteRenderer.sprite = changedSprite;
            spriteChanged = true;

            transform.localScale = transform.localScale * 0.7f;
        }

        if (canDrag)
        {
            if (!firstDrag)
            {
                firstDrag = true;
            }

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = (Vector2)mousePos + offset;
        }
    }

    private void OnMouseUp()
    {
        if (canDrag)
        {
            isDragging = false;
        }
    }

    IEnumerator DestoryThis()
    {
        yield return new WaitForSeconds(0.05f);
        if (!isDragged)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ChangeAfterDelay(float delay)
    {
        if (soundName != null)
        {
            SoundObject _soundObject;
            _soundObject = SoundPlayer.Play(soundName, false);
            _soundObject.SetVolume(1.3f);
        }

        yield return new WaitForSeconds(delay);

        if (changePrefab != null)
        {
            Instantiate(changePrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public void SetChangePrefab(GameObject prefab)
    {
        this.changePrefab = prefab;
    }
}
