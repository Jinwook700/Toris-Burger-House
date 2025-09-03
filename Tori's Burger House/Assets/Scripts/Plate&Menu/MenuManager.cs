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

            Debug.Log($"MenuManager�� ���� �߰���: {string.Join(", ", combination)}");
        }
        else
        {
            Debug.LogError("��ȿ���� ���� �޴� ������ ���޵Ǿ����ϴ�.");
        }
    }

    public List<List<int>> GetAllMenuCombinations()
    {
        return menuCombinations;
    }
}
