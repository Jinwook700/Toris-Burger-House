using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IngredientData;

public class FoodPrefabManager : MonoBehaviour
{
    public static FoodPrefabManager Instance { get; private set; }

    [SerializeField] private List<GameObject> prefabs;

    private Dictionary<IngredientType, GameObject> prefabDict;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        prefabDict = new Dictionary<IngredientType, GameObject>();
        for (int i = 0; i < prefabs.Count; i++)
        {
            prefabDict[(IngredientType)i] = prefabs[i];
        }
    }

    public GameObject GetPrefab(IngredientType type)
    {
        return prefabDict[type];
    }
}
