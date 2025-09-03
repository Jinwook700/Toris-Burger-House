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
        // Bowl들이 자기 Plate를 알도록 세팅
        burgerBowl.SetPlate(this);
        potatoBowl.SetPlate(this);
        juiceBowl.SetPlate(this);
    }

    // Bowl에서 재료가 추가될 때 Plate로 알림
    public void OnBowlUpdated()
    {
        // 필요하면 여기서 UI 갱신도 가능
    }

    // 음료수가 들어올 때 호출됨 (제출 트리거)
    public void OnJuiceAdded()
    {
        if (isSubmitted) return;
        isSubmitted = true;

        List<int> combination = GetPlateCombination();
        Debug.Log("제출된 접시 조합: " + string.Join(", ", combination));

        // MenuManager의 메뉴와 비교
        bool isCorrect = false;
        foreach (var menuCombo in MenuManager.Instance.GetAllMenuCombinations())
        {
            Debug.Log("메뉴 조합과 비교: " + string.Join(", ", menuCombo));

            if (AreCombinationsEqual(menuCombo, combination))
            {
                isCorrect = true;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log(" 정답! 메뉴와 일치합니다");
        }
        else
        {
            Debug.Log(" 실패! 메뉴와 다릅니다");
        }
    }

    // 현재 Plate의 조합 추출
    private List<int> GetPlateCombination()
    {
        List<int> result = new List<int>();

        // 햄버거: 빵 - [3개] - 빵 → 가운데 3개만 사용
        List<int> burger = burgerBowl.GetIngredients();
        if (burger.Count >= 5)
        {
            result.Add(burger[1]); // 두 번째 (빵 다음)
            result.Add(burger[2]); // 세 번째
            result.Add(burger[3]); // 네 번째
        }

        // 감자튀김
        result.AddRange(potatoBowl.GetIngredients());

        // 음료
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
