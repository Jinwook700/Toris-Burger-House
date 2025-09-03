using System.Collections.Generic;
using UnityEngine;

public class BurgerBowl : MonoBehaviour, IBowlHandler
{
    private PlateController plate;
    private List<int> ingredients = new List<int>();

    public void SetPlate(PlateController plateController)
    {
        plate = plateController;
    }

    public void OnIngredientAdded(int ingredientIndex)
    {
        ingredients.Add(ingredientIndex);
        plate.OnBowlUpdated();
    }

    public List<int> GetIngredients()
    {
        return new List<int>(ingredients);
    }

    public void ClearIngredients()
    {
        // 리스트 비우기
        ingredients.Clear();

        // Bowl 안의 실제 오브젝트도 다 제거
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
