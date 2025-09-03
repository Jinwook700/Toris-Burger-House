using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Ingredient UI")]
    public Transform ingredientContainer;
    public GameObject ingredientSlotPrefab;

    private List<GameObject> selectedIngredients = new List<GameObject>();

    // 메뉴가 가진 조합 (인덱스 저장)
    public List<int> SelectedIngredientIndices { get; private set; } = new List<int>();

    // 배경 색상 적용용
    private Image background;

    private void Awake()
    {
        background = GetComponent<Image>();
    }

    public void InitializeMenu(IngredientData ingredientData, List<List<int>> existingCombinations, Color menuColor)
    {
        selectedIngredients.Clear();
        SelectedIngredientIndices.Clear();

        // 배경 색상 설정
        if (background != null)
            background.color = menuColor;

        GridLayoutGroup grid = ingredientContainer.GetComponent<GridLayoutGroup>();
        if (grid == null)
        {
            Debug.LogError("Ingredient Container에 GridLayoutGroup 없음!");
            return;
        }

        int ingredientCount = 3;

        // 새로운 조합 찾기
        bool validCombination = false;
        int maxTries = 100; // 무한 루프 방지
        while (!validCombination && maxTries-- > 0)
        {
            SelectedIngredientIndices.Clear();

            for (int i = 0; i < ingredientCount; i++)
            {
                int ingredientIndex = Random.Range(0, ingredientData.ingredientPrefabs.Count);
                SelectedIngredientIndices.Add(ingredientIndex);
            }

            // 기존 조합과 동일하지 않은지 검사
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

        // UI 생성
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

    // 두 조합이 동일한지 비교 (순서 고려)
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
