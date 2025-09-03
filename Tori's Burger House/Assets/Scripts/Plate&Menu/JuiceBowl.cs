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
        ingredients.Clear(); // ���ᵵ ����
        ingredients.Add(ingredientIndex);

        // ���� ������ ���� ����
        plate.OnJuiceAdded();
    }

    public List<int> GetIngredients()
    {
        return new List<int>(ingredients);
    }
}
