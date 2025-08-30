using System.Collections.Generic;
using UnityEngine;

public class MenuPlate : MonoBehaviour
{
    public GameObject menuItemPrefab;
    public Transform menuContainer;
    public int menuCount = 4;
    public IngredientData ingredientData;

    private List<Menu> menuList = new List<Menu>();
    private HashSet<string> usedCombinations = new HashSet<string>(); // ���� ����

    void Start()
    {
        for (int i = 0; i < menuCount; i++)
        {
            GameObject menuObj = Instantiate(menuItemPrefab, menuContainer);
            Menu menu = menuObj.GetComponent<Menu>();
            if (menu != null)
            {
                menu.InitializeMenu(ingredientData, usedCombinations);
                menuList.Add(menu);
            }
        }

        // ������ �޴� �α� ���
        int count = 1;
        foreach (var menu in menuList)
        {
            Debug.Log($"�޴� {count}: {string.Join(", ", menu.SelectedIngredientNames)}");
            count++;
        }
    }
}
