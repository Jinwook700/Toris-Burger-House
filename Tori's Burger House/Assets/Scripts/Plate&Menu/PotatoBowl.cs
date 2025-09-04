using System.Collections.Generic;
using UnityEngine;

public class PotatoBowl : MonoBehaviour, IBowlHandler
{
    private PlateController plate;
    private List<int> ingredients = new List<int>();

    public void SetPlate(PlateController plateController)
    {
        plate = plateController;
    }

    public void OnIngredientAdded(int ingredientIndex)
    {
        ingredients.Clear();
        ingredients.Add(ingredientIndex);
        plate.OnBowlUpdated();
    }

    public List<int> GetIngredients()
    {
        return new List<int>(ingredients);
    }

    public void ClearIngredients()
    {
        ingredients.Clear();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
