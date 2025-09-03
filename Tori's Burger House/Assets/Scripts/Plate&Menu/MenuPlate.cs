using System.Collections.Generic;
using UnityEngine;

public class MenuPlate : MonoBehaviour
{
    public GameObject menuItemPrefab;
    public Transform menuContainer;
    public int menuCount = 4;
    public IngredientData ingredientData;

    public List<Menu> menuList = new List<Menu>();

    // 메뉴 조합 관리용
    public List<List<int>> allMenuCombinations = new List<List<int>>();
    public List<Color> menuColors = new List<Color>();

    // 사용할 색상 후보
    private Color[] availableColors = new Color[]
    {
        Color.red,              // 빨강
        new Color(0.5f, 1f, 0.5f), // 연두색
        new Color(1f, 0.65f, 0f)   // 주황색
    };

    void Start()
    {
        for (int i = 0; i < menuCount; i++)
        {
            GameObject menuObj = Instantiate(menuItemPrefab, menuContainer);
            Menu menu = menuObj.GetComponent<Menu>();
            if (menu != null)
            {
                Color randomColor = availableColors[Random.Range(0, availableColors.Length)];

                // MenuManager에서 기존 조합을 가져와 InitializeMenu에 전달
                List<List<int>> existingCombinations = MenuManager.Instance.GetAllMenuCombinations();
                menu.InitializeMenu(ingredientData, existingCombinations, randomColor);

                menuList.Add(menu);
                menuColors.Add(randomColor);

                // 생성된 조합을 MenuManager에 추가
                MenuManager.Instance.AddMenuCombination(menu.SelectedIngredientIndices);
            }
        }
    }

    public void SpawnNewMenu()
    {
        GameObject menuObj = Instantiate(menuItemPrefab, menuContainer);
        Menu menu = menuObj.GetComponent<Menu>();
        if (menu != null)
        {
            Color randomColor = availableColors[Random.Range(0, availableColors.Length)];

            // 기존 조합 가져오기
            List<List<int>> existingCombinations = MenuManager.Instance.GetAllMenuCombinations();
            menu.InitializeMenu(ingredientData, existingCombinations, randomColor);

            menuList.Add(menu);
            menuColors.Add(randomColor);

            // MenuManager에 추가
            MenuManager.Instance.AddMenuCombination(menu.SelectedIngredientIndices);
        }
    }

}
