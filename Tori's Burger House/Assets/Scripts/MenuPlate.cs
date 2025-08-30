using System.Collections.Generic;
using UnityEngine;

public class MenuPlate : MonoBehaviour
{
    public GameObject menuItemPrefab;
    public Transform menuContainer;
    public int menuCount = 4;
    public IngredientData ingredientData;

    private List<Menu> menuList = new List<Menu>();
    private List<int> usedIndices = new List<int>();

    void Start()
    {
        for (int i = 0; i < menuCount; i++)
        {
            GameObject menuObj = Instantiate(menuItemPrefab, menuContainer);
            Menu menu = menuObj.GetComponent<Menu>();
            if (menu != null)
            {
                menu.InitializeMenu(ingredientData, usedIndices);
                menuList.Add(menu);
            }
        }
    }
}
