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
        if (isSubmitted) return; // 중복 방지
        isSubmitted = true;

        List<int> combination = GetPlateCombination();

        Debug.Log("제출된 조합: " + string.Join(", ", combination));

        // MenuManager의 메뉴와 비교
        bool isCorrect = false;
        foreach (var menuCombo in MenuManager.Instance.GetAllMenuCombinations())
        {
            if (AreCombinationsEqual(menuCombo, combination))
            {
                isCorrect = true;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("정답! 메뉴와 일치합니다 ");
            // 메뉴 제거 & 새로 생성 로직 실행
        }
        else
        {
            Debug.Log("실패! 메뉴와 다릅니다 ");
        }
    }

    // 현재 Plate의 조합 추출
    private List<int> GetPlateCombination()
    {
        List<int> result = new List<int>();

        // 햄버거는 5겹 (BurgerBowl 내부 리스트 그대로 가져오기)
        result.AddRange(burgerBowl.GetIngredients());

        // 감자튀김 1개
        result.AddRange(potatoBowl.GetIngredients());

        // 음료 1개
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
