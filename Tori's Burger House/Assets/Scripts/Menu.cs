using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Ingredient UI")]
    public Transform ingredientContainer;    // GridLayoutGroup 있는 오브젝트
    public GameObject ingredientSlotPrefab;  // UI 슬롯 (Image만 있으면 됨)

    private List<GameObject> selectedIngredients = new List<GameObject>();

    public void InitializeMenu(IngredientData ingredientData, List<int> usedIndices)
    {
        selectedIngredients.Clear();

        GridLayoutGroup grid = ingredientContainer.GetComponent<GridLayoutGroup>();
        if (grid == null)
        {
            Debug.LogError("Ingredient Container에 GridLayoutGroup 없음!");
            return;
        }

        int ingredientCount = 3;
        List<int> availableIndices = new List<int>();

        for (int i = 0; i < ingredientData.ingredientPrefabs.Count; i++)
        {
            if (!usedIndices.Contains(i))
                availableIndices.Add(i);
        }

        if (availableIndices.Count < ingredientCount)
        {
            Debug.LogError("재료가 부족합니다! IngredientData에 프리팹을 더 넣으세요.");
            return;
        }

        for (int i = 0; i < ingredientCount; i++)
        {
            int randIndex = Random.Range(0, availableIndices.Count);
            int ingredientIndex = availableIndices[randIndex];
            availableIndices.RemoveAt(randIndex);
            usedIndices.Add(ingredientIndex);

            // prefab 대신 UI 오브젝트 (Image)로 가정
            GameObject prefab = ingredientData.ingredientPrefabs[ingredientIndex];

            // UI 슬롯 생성
            GameObject ingredientUI = Instantiate(ingredientSlotPrefab, ingredientContainer);

            // UI에 Sprite 할당 (Image에서 바로 가져옴)
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
}
