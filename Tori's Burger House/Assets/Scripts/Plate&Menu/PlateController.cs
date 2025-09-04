using System.Collections.Generic;
using System.Sound;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    public BurgerBowl burgerBowl;
    public PotatoBowl potatoBowl;
    public JuiceBowl juiceBowl;

    private bool isSubmitted = false;

    private void Start()
    {
        burgerBowl.SetPlate(this);
        potatoBowl.SetPlate(this);
        juiceBowl.SetPlate(this);
    }

    public void OnBowlUpdated()
    {
    }

    public void OnJuiceAdded()
    {
        if (isSubmitted) return;
        isSubmitted = true;

        List<int> combination = GetPlateCombination();

        int score = CalculateScore(combination);

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
            SoundObject _soundObject;
            _soundObject = Sound.Play("GoodSubmit", false);
            _soundObject.SetVolume(0.9f);

            GoldManager.Instance.AddGold(score);

            if (matchedMenu != null)
            {
                MenuManager.Instance.RemoveMenuCombination(matchedMenu.SelectedIngredientIndices);
                Destroy(matchedMenu.gameObject);
                menuPlate.menuList.Remove(matchedMenu);
            }

            ClearPlate();
            isSubmitted = false;

            menuPlate.SpawnNewMenu();
        }
        else
        {
            SoundObject _soundObject;
            _soundObject = Sound.Play("BadSubmit", false);
            _soundObject.SetVolume(0.9f);

            ClearPlate();
            isSubmitted = false;
        }

        burgerBowl.GetComponent<Bowl>().ResetBowl();
        potatoBowl.GetComponent<Bowl>().ResetBowl();
        juiceBowl.GetComponent<Bowl>().ResetBowl();
    }

    private List<int> GetPlateCombination()
    {
        List<int> result = new List<int>();

        List<int> burger = burgerBowl.GetIngredients();
        if (burger.Count >= 5)
        {
            result.Add(burger[1]);
            result.Add(burger[2]);
            result.Add(burger[3]);
        }

        result.AddRange(potatoBowl.GetIngredients());

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
            Tori.Instance.SetState(Tori.CharacterState.Angry, "이런 메뉴는 없다냥!");
            return 0;
        }

        List<int> burger = burgerBowl.GetIngredients();
        if (burger.Count < 5)
        {
            Tori.Instance.SetState(Tori.CharacterState.Angry, "이런 메뉴는 없다냥!");
            return 0;
        }

        int bunStart = burger[0];
        int bunEnd = burger[burger.Count - 1];
        if (!IsBun(bunStart) || !IsBun(bunEnd))
        {
            Tori.Instance.SetState(Tori.CharacterState.Angry, "이런 메뉴는 없다냥!");
            return 0;
        }

        List<int> middleIngredients = new List<int>
    {
        burger[1],
        burger[2],
        burger[3]
    };

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
            Tori.Instance.SetState(Tori.CharacterState.Angry, "이런 메뉴는 없다냥!");
            return 0;
        }

        int burntCount = 0;
        bool potatoBurnt = false;
        bool potatoMissing = false;

        if (bunStart == (int)IngredientData.IngredientType.BunCircle2) { score -= 100; burntCount++; }
        if (bunEnd == (int)IngredientData.IngredientType.BunCircle2) { score -= 100; burntCount++; }

        foreach (int mid in middleIngredients)
        {
            if (mid == (int)IngredientData.IngredientType.MeatCircle2)
            {
                score -= 100;
                burntCount++;
            }
        }

        if (plateCombo.Count > 4)
        {
            int potato = plateCombo[3];
            if (potato == (int)IngredientData.IngredientType.PotatoCircle3)
            {
                score -= 200;
                potatoBurnt = true;
            }
            else if (potato != (int)IngredientData.IngredientType.PotatoCircle2)
            {
                score -= 400;
                potatoMissing = true;
            }
        }
        else
        {
            score -= 400;
            potatoMissing = true;
        }

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

        for (int i = 0; i < 3; i++)
        {
            if (!(menuCombo[i] == plateCombo[i] ||
                  (IsMeat(menuCombo[i]) && IsMeat(plateCombo[i]))))
            {
                return false;
            }
        }

        if (menuCombo[4] != plateCombo[plateCombo.Count - 1])
            return false;

        return true;
    }
}
