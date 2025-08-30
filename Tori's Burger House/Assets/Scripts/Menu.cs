using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Ingredient UI")]
    public Transform ingredientContainer;    // GridLayoutGroup �ִ� ������Ʈ
    public GameObject ingredientSlotPrefab;  // UI ���� (Image�� ������ ��)

    private List<GameObject> selectedIngredients = new List<GameObject>();

    public void InitializeMenu(IngredientData ingredientData, List<int> usedIndices)
    {
        selectedIngredients.Clear();

        GridLayoutGroup grid = ingredientContainer.GetComponent<GridLayoutGroup>();
        if (grid == null)
        {
            Debug.LogError("Ingredient Container�� GridLayoutGroup ����!");
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
            Debug.LogError("��ᰡ �����մϴ�! IngredientData�� �������� �� ��������.");
            return;
        }

        for (int i = 0; i < ingredientCount; i++)
        {
            int randIndex = Random.Range(0, availableIndices.Count);
            int ingredientIndex = availableIndices[randIndex];
            availableIndices.RemoveAt(randIndex);
            usedIndices.Add(ingredientIndex);

            // prefab ��� UI ������Ʈ (Image)�� ����
            GameObject prefab = ingredientData.ingredientPrefabs[ingredientIndex];

            // UI ���� ����
            GameObject ingredientUI = Instantiate(ingredientSlotPrefab, ingredientContainer);

            // UI�� Sprite �Ҵ� (Image���� �ٷ� ������)
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
