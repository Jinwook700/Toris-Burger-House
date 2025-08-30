using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "Scriptable Objects/Ingredient Data")]
public class IngredientData : ScriptableObject
{
    public enum IngredientType
    {
        Bun = 0,
        Tomato = 1,
        Lettuce = 2,
        Onion = 3,
        Pickle = 4,
        Meat = 5,
        Cheese = 6,
        FriedPotato = 7,
        Cola = 8,
        Cider = 9,
        OrangeJuice = 10,
    }

    [Header("Ingredients")]
    public List<GameObject> ingredientPrefabs;
}
