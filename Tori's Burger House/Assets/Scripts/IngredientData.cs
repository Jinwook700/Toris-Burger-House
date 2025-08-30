using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "Scriptable Objects/Ingredient Data")]
public class IngredientData : ScriptableObject
{
    public enum IngredientType
    {
        BunCircle = 0,
        CheeseCircle = 1,
        Cider = 2,
        Cola = 3,
        Cup = 4,
        LettuceCircle = 5,
        MeatCircle = 6,
        OnionCircle = 7,
        OrangeJuice = 8,
        PickleCircle = 9,
        PotatoCircle = 10,
        TomatoCircle = 11,
    }

    [Header("Ingredients")]
    public List<GameObject> ingredientPrefabs;
}
