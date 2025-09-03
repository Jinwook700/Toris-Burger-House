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

        // 메뉴 판정은 감튀 무시 (버거+음료만 비교)
        bool matched = false;
        MenuPlate menuPlate = FindObjectOfType<MenuPlate>();
        Menu matchedMenu = null;

        foreach (Transform child in menuPlate.menuContainer)
        {
            Menu menu = child.GetComponent<Menu>();
            if (menu != null && IsSameMenu(menu.SelectedIngredientIndices, combination))
            {
                matched = true;
                matchedMenu = menu;
                break;
            }
        }

        if (matched)
        {
            Debug.Log("정답! 메뉴와 일치합니다");
            Debug.Log($"획득 점수: {score}");

            // 맞춘 메뉴 삭제
            if (matchedMenu != null)
            {
                MenuManager.Instance.RemoveMenuCombination(matchedMenu.SelectedIngredientIndices);
                Destroy(matchedMenu.gameObject);
                menuPlate.menuList.Remove(matchedMenu);
            }

            ClearPlate();
            isSubmitted = false;

            // 새 메뉴 추가
            menuPlate.SpawnNewMenu();
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
            Tori.Instance.SetState(Tori.CharacterState.Angry, "이런 메뉴는 없다냥!");
            return 0;
        }

        // 2. 햄버거 확인 (재료 5개 이상인지)
        List<int> burger = burgerBowl.GetIngredients();
        if (burger.Count < 5)
        {
            Debug.Log("햄버거가 5겹 미만 → 0점");
            Tori.Instance.SetState(Tori.CharacterState.Angry, "이런 메뉴는 없다냥!");
            return 0;
        }

        // 3. 양쪽 빵 확인
        int bunStart = burger[0];
        int bunEnd = burger[burger.Count - 1];
        if (!IsBun(bunStart) || !IsBun(bunEnd))
        {
            Debug.Log("햄버거의 시작/끝이 빵이 아님 → 0점");
            Tori.Instance.SetState(Tori.CharacterState.Angry, "이런 메뉴는 없다냥!");
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

            List<int> menuBurger = new List<int>
        {
            menuCombo[0],
            menuCombo[1],
            menuCombo[2]
        };

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

            bool bunMatch = IsBun(burger[0]) && IsBun(burger[burger.Count - 1]);
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
            Tori.Instance.SetState(Tori.CharacterState.Angry, "이런 메뉴는 없다냥!");
            return 0;
        }

        // -------------------------
        // 6. 감점 규칙 + Tori 상태
        // -------------------------
        int burntCount = 0;
        bool potatoBurnt = false;
        bool potatoMissing = false;

        // 빵 체크
        if (bunStart == (int)IngredientData.IngredientType.BunCircle2) { score -= 100; burntCount++; }
        if (bunEnd == (int)IngredientData.IngredientType.BunCircle2) { score -= 100; burntCount++; }

        // 고기 체크
        foreach (int mid in middleIngredients)
        {
            if (mid == (int)IngredientData.IngredientType.MeatCircle2)
            {
                score -= 100;
                burntCount++;
            }
        }

        // 감자 체크
        if (plateCombo.Count > 4)
        {
            int potato = plateCombo[3];
            if (potato == (int)IngredientData.IngredientType.PotatoCircle3)
            {
                Debug.Log("태운 감자튀김 → -200점");
                score -= 200;
                potatoBurnt = true;
            }
            else if (potato != (int)IngredientData.IngredientType.PotatoCircle2)
            {
                Debug.Log("감자 없음 → -400점");
                score -= 400;
                potatoMissing = true;
            }
        }
        else
        {
            Debug.Log("감자 없음 → -400점");
            score -= 400;
            potatoMissing = true;
        }

        // -------------------------
        // Tori 상태 결정
        // -------------------------
        if (score == 1000)
        {
            Tori.Instance.SetState(Tori.CharacterState.Happy, "맛있겠다냥~!");
        }
        else if (potatoMissing && burntCount == 0)
        {
            Tori.Instance.SetState(Tori.CharacterState.Angry, "감자를 무시하냥!");
        }
        else if (potatoBurnt && burntCount == 0)
        {
            Tori.Instance.SetState(Tori.CharacterState.Normal, "감자에서 탄맛난다냥..");
        }
        else if (!potatoBurnt && !potatoMissing && burntCount == 1)
        {
            Tori.Instance.SetState(Tori.CharacterState.Normal, "잘먹겠다냥!");
        }
        else if (!potatoBurnt && !potatoMissing && burntCount == 2)
        {
            Tori.Instance.SetState(Tori.CharacterState.Normal, "먹을만하다냥");
        }
        else if (!potatoBurnt && !potatoMissing && burntCount >= 3)
        {
            Tori.Instance.SetState(Tori.CharacterState.Angry, "넌 요리하지마라냥");
        }
        else if (potatoBurnt && burntCount >= 1)
        {
            Tori.Instance.SetState(Tori.CharacterState.Normal, "넌 요리하지 말라냥..");
        }
        else
        {
            Tori.Instance.SetState(Tori.CharacterState.Normal, "고생햇다냥.");
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
    private bool IsSameMenu(List<int> menuCombo, List<int> plateCombo)
    {
        if (menuCombo.Count < 5 || plateCombo.Count < 4) return false;

        // 버거 중간 재료 3개 비교 (고기/탄고기 구분 없음)
        for (int i = 0; i < 3; i++)
        {
            if (!(menuCombo[i] == plateCombo[i] ||
                  (IsMeat(menuCombo[i]) && IsMeat(plateCombo[i]))))
            {
                return false;
            }
        }

        // 음료 비교 (menuCombo[4] vs plateCombo 마지막)
        if (menuCombo[4] != plateCombo[plateCombo.Count - 1])
            return false;

        return true;
    }
}
