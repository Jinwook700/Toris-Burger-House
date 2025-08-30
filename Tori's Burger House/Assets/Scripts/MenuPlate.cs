using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPlate : MonoBehaviour
{
    public GameObject menuItemPrefab;
    public Transform menuContainer;

    public int menuCount = 4;

    void Start()
    {
        GridLayoutGroup gridLayoutGroup = menuContainer.GetComponent<GridLayoutGroup>();
        if (gridLayoutGroup == null)
        {
            Debug.LogError("Menu Container에 GridLayoutGroup 컴포넌트가 없습니다.");
            return;
        }

        for (int i = 0; i < menuCount; i++)
        {
            GameObject menuItem = Instantiate(menuItemPrefab, menuContainer);
        }
    }
}
