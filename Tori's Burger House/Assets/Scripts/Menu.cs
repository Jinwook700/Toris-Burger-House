using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Ingredient UI")]
    public Transform ingredientContainer;
    public GameObject ingredientSlotPrefab;

    private List<GameObject> selectedIngredients = new List<GameObject>();

    // �޴��� ���� ���� (�ε��� ����)
    public List<int> SelectedIngredientIndices { get; private set; } = new List<int>();

    // ��� ���� �����
    private Image background;

    private void Awake()
    {
        background = GetComponent<Image>();
    }

    public void InitializeMenu(IngredientData ingredientData, List<List<int>> existingCombinations, Color menuColor)
    {
        selectedIngredients.Clear();
        SelectedIngredientIndices.Clear();

        // ��� ���� ����
        if (background != null)
            background.color = menuColor;

        GridLayoutGroup grid = ingredientContainer.GetComponent<GridLayoutGroup>();
        if (grid == null)
        {
            Debug.LogError("Ingredient Container�� GridLayoutGroup ����!");
            return;
        }

        int ingredientCount = 3;

        // ���ο� ���� ã��
        bool validCombination = false;
        int maxTries = 100; // ���� ���� ����
        while (!validCombination && maxTries-- > 0)
        {
            SelectedIngredientIndices.Clear();

            for (int i = 0; i < ingredientCount; i++)
            {
                int ingredientIndex = Random.Range(0, ingredientData.ingredientPrefabs.Count);
                SelectedIngredientIndices.Add(ingredientIndex);
            }

            // ���� ���հ� �������� ������ �˻�
            validCombination = true;
            foreach (var combo in existingCombinations)
            {
                if (AreCombinationsEqual(combo, SelectedIngredientIndices))
                {
                    validCombination = false;
                    break;
                }
            }
        }

        // UI ����
        foreach (int ingredientIndex in SelectedIngredientIndices)
        {
            GameObject prefab = ingredientData.ingredientPrefabs[ingredientIndex];
            GameObject ingredientUI = Instantiate(ingredientSlotPrefab, ingredientContainer);

            Image img = ingredientUI.GetComponent<Image>();
            if (img != null)
            {
                Image prefabImage = prefab.GetComponent<Image>();
                if (prefabImage != null)
                    img.sprite = prefabImage.sprite;
            }

            selectedIngredients.Add(prefab);
        }
    }

    // �� ������ �������� �� (���� ���)
    private bool AreCombinationsEqual(List<int> combo1, List<int> combo2)
    {
        if (combo1.Count != combo2.Count) return false;
        for (int i = 0; i < combo1.Count; i++)
        {
            if (combo1[i] != combo2[i])
                return false;
        }
        return true;
    }
}
