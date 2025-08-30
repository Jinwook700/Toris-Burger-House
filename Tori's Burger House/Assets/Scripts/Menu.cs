using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Ingredient UI")]
    public Transform ingredientContainer;
    public GameObject ingredientSlotPrefab;

    private List<int> selectedIngredientIndices = new List<int>();
    public List<string> SelectedIngredientNames { get; private set; } = new List<string>();

    public void InitializeMenu(IngredientData ingredientData, HashSet<string> usedCombinations)
    {
        selectedIngredientIndices.Clear();
        SelectedIngredientNames.Clear();

        GridLayoutGroup grid = ingredientContainer.GetComponent<GridLayoutGroup>();
        if (grid == null)
        {
            Debug.LogError("Ingredient Container에 GridLayoutGroup 없음!");
            return;
        }

        int ingredientCount = 3;

        // 조합 찾기
        string comboKey;
        do
        {
            selectedIngredientIndices.Clear();

            for (int i = 0; i < ingredientCount; i++)
            {
                int ingredientIndex = Random.Range(0, ingredientData.ingredientPrefabs.Count);
                selectedIngredientIndices.Add(ingredientIndex);
            }

            comboKey = string.Join(",", selectedIngredientIndices);
        }
        while (usedCombinations.Contains(comboKey)); // 이미 있으면 다시 뽑기

        usedCombinations.Add(comboKey);

        // UI 표시
        foreach (int ingredientIndex in selectedIngredientIndices)
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

            SelectedIngredientNames.Add(prefab.name);
        }
    }
}
