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

        int score = CalculateScore(combination);

        if (score > 0)
        {
            Debug.Log("정답! 메뉴와 일치합니다");
            Debug.Log($"획득 점수: {score}");

            // 맞춘 메뉴 삭제
            MenuManager.Instance.RemoveMenuCombination(combination);

            // 새 메뉴 생성
            FindObjectOfType<MenuPlate>().SpawnNewMenu();
        }
        else
        {
            Debug.Log("실패! 메뉴와 다릅니다");

            ClearPlate();

            isSubmitted = false;
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

    private int CalculateScore(List<int> plateCombo)
    {
        int score = 1000;

        // 1. 음료 확인 (메뉴판에 해당 음료가 있어야 함)
        int drink = plateCombo[plateCombo.Count - 1];
        bool hasDrinkMenu = false;
        foreach (var menuCombo in MenuManager.Instance.GetAllMenuCombinations())
        {
            if (menuCombo.Count >= 5 && menuCombo[4] == drink)
            {
                hasDrinkMenu = true;
                break;
            }
        }
        if (!hasDrinkMenu)
        {
            Debug.Log("해당 음료가 포함된 메뉴 없음 → 0점");
            return 0;
        }

        // 2. 햄버거 확인 (재료 5개 이상인지)
        List<int> burger = burgerBowl.GetIngredients();
        if (burger.Count < 5)
        {
            Debug.Log("햄버거가 5겹 미만 → 0점");
            return 0;
        }

        // 3. 양쪽 빵 확인
        int bunStart = burger[0];
        int bunEnd = burger[burger.Count - 1];
        if (!IsBun(bunStart) || !IsBun(bunEnd))
        {
            Debug.Log("햄버거의 시작/끝이 빵이 아님 → 0점");
            return 0;
        }

        // 4. 중간 재료 확인 (3개만)
        List<int> middleIngredients = new List<int>
    {
        burger[1],
        burger[2],
        burger[3]
    };

        // 5. 메뉴와 일치 여부 확인
        bool match = false;
        foreach (var menuCombo in MenuManager.Instance.GetAllMenuCombinations())
        {
            if (menuCombo.Count < 5) continue;

            // 메뉴의 버거 중간 재료
            List<int> menuBurger = new List<int>
    {
        menuCombo[0],
        menuCombo[1],
        menuCombo[2]
    };

            // 중간 재료 비교 (고기/탄고기 구분 없음)
            bool middleMatch = true;
            for (int i = 0; i < 3; i++)
            {
                if (!(middleIngredients[i] == menuBurger[i] ||
                      (IsMeat(middleIngredients[i]) && IsMeat(menuBurger[i]))))
                {
                    middleMatch = false;
                    break;
                }
            }

            // 빵 확인 (시작과 끝이 둘 다 빵인지)
            bool bunMatch = IsBun(burger[0]) && IsBun(burger[burger.Count - 1]);

            // 메뉴 음료 비교
            bool drinkMatch = (menuCombo[4] == drink);

            if (middleMatch && bunMatch && drinkMatch)
            {
                match = true;
                break;
            }
        }

        if (!match)
        {
            Debug.Log("일치하는 메뉴 없음 → 0점");
            return 0;
        }

        // 6. 감점 규칙 적용
        // 빵 → BunCircle2(13) 태운 빵
        if (bunStart == (int)IngredientData.IngredientType.BunCircle2) score -= 100;
        if (bunEnd == (int)IngredientData.IngredientType.BunCircle2) score -= 100;

        // 고기 → MeatCircle2(15) 탄 고기
        foreach (int mid in middleIngredients)
        {
            if (mid == (int)IngredientData.IngredientType.MeatCircle2) score -= 100;
        }

        // 감자 → PotatoCircle2 = 정상, PotatoCircle3 = 태움 (-200)
        int potato = plateCombo[3];
        if (potato != (int)IngredientData.IngredientType.PotatoCircle2)
        {
            if (potato == (int)IngredientData.IngredientType.PotatoCircle3)
            {
                Debug.Log("태운 감자튀김 → -200점");
                score -= 200;
            }
            else
            {
                Debug.Log("감자 없음 → -400점");
                score -= 400;
            }
        }

        return Mathf.Max(score, 0);
    }

    private bool IsBun(int ingredient)
    {
        return ingredient == (int)IngredientData.IngredientType.BunCircle1 ||
               ingredient == (int)IngredientData.IngredientType.BunCircle2;
    }

    private bool IsMeat(int ingredient)
    {
        return ingredient == (int)IngredientData.IngredientType.MeatCircle ||
               ingredient == (int)IngredientData.IngredientType.MeatCircle1 ||
               ingredient == (int)IngredientData.IngredientType.MeatCircle2;
    }

    public void ClearPlate()
    {
        burgerBowl.ClearIngredients();
        potatoBowl.ClearIngredients();
        juiceBowl.ClearIngredients();
    }

}
