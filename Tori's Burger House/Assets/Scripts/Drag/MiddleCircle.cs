using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleCircle : Drag
{
    [SerializeField] private GameObject autoChangePrefab;
    public float ChangeTime = 5f;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (normalSprite != null)
        {
            spriteRenderer.sprite = normalSprite;
        }

        StartCoroutine(AutoChangeRoutine(ChangeTime));
    }

    private IEnumerator AutoChangeRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (!firstDrag)
        {
            if (autoChangePrefab != null)
            {
                Instantiate(autoChangePrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
