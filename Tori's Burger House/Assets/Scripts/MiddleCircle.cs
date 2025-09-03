using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleCircle : Drag
{
    [SerializeField] private GameObject autoChangePrefab;
    private void Start()
    {
        StartCoroutine(AutoChangeRoutine(5f));
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
