using System.Collections.Generic;
using UnityEngine;

public class JuiceBowl : MonoBehaviour
{
    private PlateController plate;
    private List<int> ingredients = new List<int>();

    public void SetPlate(PlateController plateController)
    {
        plate = plateController;
    }

    public void AddIngredient(int ingredientIndex)
    {
        ingredients.Clear(); // 음료도 단일
        ingredients.Add(ingredientIndex);

        // 음료 들어오면 제출 실행
        plate.OnJuiceAdded();
    }

    public List<int> GetIngredients()
    {
        return new List<int>(ingredients);
    }
}
