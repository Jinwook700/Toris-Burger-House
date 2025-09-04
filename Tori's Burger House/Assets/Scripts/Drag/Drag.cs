using System.Collections;
using System.Sound;
using UnityEngine;
using static IngredientData;

public class Drag : MonoBehaviour
{
    public IngredientType IngredientType;

    public Vector2 offset;
    public bool isDragging;
    public bool isDragged;
    public bool canDrag;
    public bool isChange = false;
    public bool firstDrag = false;

    public string soundName;

    protected SpriteRenderer spriteRenderer;
    private bool spriteChanged = false;

    public float delayTime = 2f;

    [SerializeField] private GameObject changePrefab;

    [SerializeField] protected Sprite normalSprite;
    [SerializeField] private Sprite changedSprite;

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
            _soundObject = Sound.Play("DragStart", false);
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
            _soundObject = Sound.Play(soundName, false);
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
