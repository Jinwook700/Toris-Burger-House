using System.Collections;
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

    [SerializeField] private GameObject changePrefab; // 바꿀 Prefab을 Inspector에서 할당

    private void Update()
    {
        if (!isDragging && !isDragged)
        {
            StartCoroutine(DestoryThis());
        }

        // Zone에 장착된 상태이고, 교체 가능할 때
        if (isDragged && isChange)
        {
            isChange = false; // 중복 실행 방지
            StartCoroutine(ChangeAfterDelay(2f));
        }
    }

    private void OnMouseDown()
    {
        if (canDrag)
        {
            isDragging = true;
            isDragged = false;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = (Vector2)transform.position - mousePos;
        }
    }

    private void OnMouseDrag()
    {
        if (canDrag)
        {
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
