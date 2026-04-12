using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 재료 프리팹을 관리하는 스크립터블 오브젝트
/// </summary>
[CreateAssetMenu(fileName = "IngredientData", menuName = "Scriptable Objects/Ingredient Data")]
public class IngredientData : ScriptableObject
{ 
    [Header("Ingredients")]
    public List<GameObject> ingredientPrefabs;

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
        BunCircle1 = 12,
        BunCircle2 = 13,
        MeatCircle1 = 14,
        MeatCircle2 = 15,
        PotatoCircle1 = 16,
        PotatoCircle2 = 17,
        PotatoCircle3 = 18,
    }
}
