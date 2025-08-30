using System.Collections.Generic;
using UnityEngine;

public class MenuPlate : MonoBehaviour
{
    public GameObject menuItemPrefab;
    public Transform menuContainer;
    public int menuCount = 4;
    public IngredientData ingredientData;

    private List<Menu> menuList = new List<Menu>();

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
                // ���� ���� ����
                Color randomColor = availableColors[Random.Range(0, availableColors.Length)];

                // �޴� �ʱ�ȭ
                menu.InitializeMenu(ingredientData, allMenuCombinations, randomColor);

                // ����Ʈ�� ����
                menuList.Add(menu);
                menuColors.Add(randomColor);
                allMenuCombinations.Add(menu.SelectedIngredientIndices);

                Debug.Log($"�޴� {i + 1}: {string.Join(",", menu.SelectedIngredientIndices)} / ����: {randomColor}");
            }
        }
    }
}
