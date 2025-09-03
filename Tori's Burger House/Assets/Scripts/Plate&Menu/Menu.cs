using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Ingredient UI")]
    public Transform ingredientContainer;
    public GameObject ingredientSlotPrefab;

    private List<GameObject> selectedIngredients = new List<GameObject>();

    // 메뉴가 가진 조합 (인덱스 저장: enum 기준)
    public List<int> SelectedIngredientIndices { get; private set; } = new List<int>();

    // 사용할 수 있는 재료 후보 (enum 값)
    private int[] availableBurgerIngredients = new int[]
    {
        (int)IngredientData.IngredientType.TomatoCircle,
        (int)IngredientData.IngredientType.LettuceCircle,
        (int)IngredientData.IngredientType.OnionCircle,
        (int)IngredientData.IngredientType.PickleCircle,
        (int)IngredientData.IngredientType.MeatCircle, 
        (int)IngredientData.IngredientType.CheeseCircle
    };

    public void InitializeMenu(IngredientData ingredientData, List<List<int>> existingCombinations, Color menuColor)
    {
        selectedIngredients.Clear();
        SelectedIngredientIndices.Clear();

        Image background = GetComponent<Image>();
        if (background != null)
        {
            background.color = menuColor;
        }

        int ingredientCount = 3;

        // 새로운 햄버거 3개 조합 찾기
        bool validCombination = false;
        int maxTries = 100;
        while (!validCombination && maxTries-- > 0)
        {
            SelectedIngredientIndices.Clear();

            for (int i = 0; i < ingredientCount; i++)
            {
                int ingredientIndex = availableBurgerIngredients[Random.Range(0, availableBurgerIngredients.Length)];
                SelectedIngredientIndices.Add(ingredientIndex);
            }

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

        // 감자튀김 추가 (기본값: 잘 만든 감자튀김)
        SelectedIngredientIndices.Add((int)IngredientData.IngredientType.PotatoCircle2);

        // 음료 추가 (색깔에 따라 결정)
        int drinkIndex = -1;
        if (menuColor == Color.red)
            drinkIndex = (int)IngredientData.IngredientType.Cola;
        else if (menuColor == new Color(0.5f, 1f, 0.5f)) // 초록
            drinkIndex = (int)IngredientData.IngredientType.Cider;
        else if (menuColor == new Color(1f, 0.65f, 0f)) // 주황
            drinkIndex = (int)IngredientData.IngredientType.OrangeJuice;

        if (drinkIndex >= 0)
            SelectedIngredientIndices.Add(drinkIndex);

        // UI 생성 (햄버거 3개만 표시)
        foreach (int ingredientIndex in SelectedIngredientIndices.GetRange(0, 3))
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

        Debug.Log($"생성된 메뉴 조합(enum 번호): {string.Join(", ", SelectedIngredientIndices)}");
    }

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
