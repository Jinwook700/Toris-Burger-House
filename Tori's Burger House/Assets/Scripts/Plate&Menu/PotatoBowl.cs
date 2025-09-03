using System.Collections.Generic;
using UnityEngine;

public class PotatoBowl : MonoBehaviour
{
    private PlateController plate;
    private List<int> ingredients = new List<int>();

    public void SetPlate(PlateController plateController)
    {
        plate = plateController;
    }

    public void AddIngredient(int ingredientIndex)
    {
        ingredients.Clear(); // ����Ƣ���� �����̴ϱ� �׻� �ϳ���
        ingredients.Add(ingredientIndex);
        plate.OnBowlUpdated();
    }

    public List<int> GetIngredients()
    {
        return new List<int>(ingredients);
    }
}
