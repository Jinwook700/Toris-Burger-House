using System.Collections.Generic;
using UnityEngine;

public class MenuPlate : MonoBehaviour
{
    public GameObject menuItemPrefab;
    public Transform menuContainer;
    public int menuCount = 4;
    public IngredientData ingredientData;

    private List<Menu> menuList = new List<Menu>();

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
                // 랜덤 색상 선택
                Color randomColor = availableColors[Random.Range(0, availableColors.Length)];

                // 메뉴 초기화
                menu.InitializeMenu(ingredientData, allMenuCombinations, randomColor);

                // 리스트에 저장
                menuList.Add(menu);
                menuColors.Add(randomColor);
                allMenuCombinations.Add(menu.SelectedIngredientIndices);

                Debug.Log($"메뉴 {i + 1}: {string.Join(",", menu.SelectedIngredientIndices)} / 색상: {randomColor}");
            }
        }
    }
}
