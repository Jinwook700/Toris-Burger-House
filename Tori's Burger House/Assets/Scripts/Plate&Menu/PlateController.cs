using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    public BurgerBowl burgerBowl;
    public PotatoBowl potatoBowl;
    public JuiceBowl juiceBowl;

    private bool isSubmitted = false;

    private void Start()
    {
        // Bowl���� �ڱ� Plate�� �˵��� ����
        burgerBowl.SetPlate(this);
        potatoBowl.SetPlate(this);
        juiceBowl.SetPlate(this);
    }

    // Bowl���� ��ᰡ �߰��� �� Plate�� �˸�
    public void OnBowlUpdated()
    {
        // �ʿ��ϸ� ���⼭ UI ���ŵ� ����
    }

    // ������� ���� �� ȣ��� (���� Ʈ����)
    public void OnJuiceAdded()
    {
        if (isSubmitted) return;
        isSubmitted = true;

        List<int> combination = GetPlateCombination();
        Debug.Log("����� ���� ����: " + string.Join(", ", combination));

        // MenuManager�� �޴��� ��
        bool isCorrect = false;
        foreach (var menuCombo in MenuManager.Instance.GetAllMenuCombinations())
        {
            Debug.Log("�޴� ���հ� ��: " + string.Join(", ", menuCombo));

            if (AreCombinationsEqual(menuCombo, combination))
            {
                isCorrect = true;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log(" ����! �޴��� ��ġ�մϴ�");
        }
        else
        {
            Debug.Log(" ����! �޴��� �ٸ��ϴ�");
        }
    }

    // ���� Plate�� ���� ����
    private List<int> GetPlateCombination()
    {
        List<int> result = new List<int>();

        // �ܹ���: �� - [3��] - �� �� ��� 3���� ���
        List<int> burger = burgerBowl.GetIngredients();
        if (burger.Count >= 5)
        {
            result.Add(burger[1]); // �� ��° (�� ����)
            result.Add(burger[2]); // �� ��°
            result.Add(burger[3]); // �� ��°
        }

        // ����Ƣ��
        result.AddRange(potatoBowl.GetIngredients());

        // ����
        result.AddRange(juiceBowl.GetIngredients());

        return result;
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
