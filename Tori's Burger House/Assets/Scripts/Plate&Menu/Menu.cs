using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Ingredient UI")]
    public Transform ingredientContainer;
    public GameObject ingredientSlotPrefab;

    private List<GameObject> selectedIngredients = new List<GameObject>();

    public List<int> SelectedIngredientIndices { get; private set; } = new List<int>();

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

        SelectedIngredientIndices.Add((int)IngredientData.IngredientType.PotatoCircle2);

        int drinkIndex = -1;
        if (menuColor == Color.red)
            drinkIndex = (int)IngredientData.IngredientType.Cola;
        else if (menuColor == new Color(0.5f, 1f, 0.5f))
            drinkIndex = (int)IngredientData.IngredientType.Cider;
        else if (menuColor == new Color(1f, 0.65f, 0f))
            drinkIndex = (int)IngredientData.IngredientType.OrangeJuice;

        if (drinkIndex >= 0)
            SelectedIngredientIndices.Add(drinkIndex);

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
