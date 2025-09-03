using System.Collections.Generic;
using UnityEngine;

public class MenuPlate : MonoBehaviour
{
    public GameObject menuItemPrefab;
    public Transform menuContainer;
    public int menuCount = 4;
    public IngredientData ingredientData;

    public List<Menu> menuList = new List<Menu>();

    // �޴� ���� ������
    public List<List<int>> allMenuCombinations = new List<List<int>>();
    public List<Color> menuColors = new List<Color>();

    // ����� ���� �ĺ�
    private Color[] availableColors = new Color[]
    {
        Color.red,              // ����
        new Color(0.5f, 1f, 0.5f), // ���λ�
        new Color(1f, 0.65f, 0f)   // ��Ȳ��
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

                // MenuManager���� ���� ������ ������ InitializeMenu�� ����
                List<List<int>> existingCombinations = MenuManager.Instance.GetAllMenuCombinations();
                menu.InitializeMenu(ingredientData, existingCombinations, randomColor);

                menuList.Add(menu);
                menuColors.Add(randomColor);

                // ������ ������ MenuManager�� �߰�
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

            // ���� ���� ��������
            List<List<int>> existingCombinations = MenuManager.Instance.GetAllMenuCombinations();
            menu.InitializeMenu(ingredientData, existingCombinations, randomColor);

            menuList.Add(menu);
            menuColors.Add(randomColor);

            // MenuManager�� �߰�
            MenuManager.Instance.AddMenuCombination(menu.SelectedIngredientIndices);
        }
    }

}
