using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    public List<List<int>> menuCombinations = new List<List<int>>();

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
    }
    public void AddMenuCombination(List<int> combination)
    {
        if (combination != null && combination.Count == 3)
        {
            menuCombinations.Add(combination);

            Debug.Log($"MenuManager에 조합 추가됨: {string.Join(", ", combination)}");
        }
        else
        {
            Debug.LogError("유효하지 않은 메뉴 조합이 전달되었습니다.");
        }
    }

    public List<List<int>> GetAllMenuCombinations()
    {
        return menuCombinations;
    }
}
