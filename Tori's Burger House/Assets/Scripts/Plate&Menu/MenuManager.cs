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
        if (combination != null && combination.Count == 5)
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

    public void RemoveMenuCombination(List<int> combination)
    {
        for (int i = 0; i < menuCombinations.Count; i++)
        {
            if (AreCombinationsEqual(menuCombinations[i], combination))
            {
                menuCombinations.RemoveAt(i);
                Debug.Log($"메뉴 삭제됨: {string.Join(", ", combination)}");
                return;
            }
        }
    }
    private bool AreCombinationsEqual(List<int> combo1, List<int> combo2)
    {
        if (combo1.Count != combo2.Count) return false;
        for (int i = 0; i < combo1.Count; i++)
        {
            if (combo1[i] != combo2[i])
                return false;
        }
        return true;
    }
}
