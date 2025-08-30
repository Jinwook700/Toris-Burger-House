using System.Collections.Generic;
using UnityEngine;

public class MenuPlate : MonoBehaviour
{
    public GameObject menuItemPrefab;
    public Transform menuContainer;
    public int menuCount = 4;
    public IngredientData ingredientData;

    private List<Menu> menuList = new List<Menu>();
    private HashSet<string> usedCombinations = new HashSet<string>(); // 조합 저장

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

        // 생성된 메뉴 로그 출력
        int count = 1;
        foreach (var menu in menuList)
        {
            Debug.Log($"메뉴 {count}: {string.Join(", ", menu.SelectedIngredientNames)}");
            count++;
        }
    }
}
