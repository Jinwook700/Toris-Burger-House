using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "Scriptable Objects/Ingredient Data")]
public class IngredientData : ScriptableObject
{
    public List<GameObject> ingredientPrefabs;
}
