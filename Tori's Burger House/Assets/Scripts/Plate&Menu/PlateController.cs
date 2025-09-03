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

        int score = CalculateScore(combination);

        // �޴� ������ ��Ƣ ���� (����+���Ḹ ��)
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
            Debug.Log("����! �޴��� ��ġ�մϴ�");
            Debug.Log($"ȹ�� ����: {score}");

            // ���� �޴� ����
            if (matchedMenu != null)
            {
                MenuManager.Instance.RemoveMenuCombination(matchedMenu.SelectedIngredientIndices);
                Destroy(matchedMenu.gameObject);
                menuPlate.menuList.Remove(matchedMenu);
            }

            ClearPlate();
            isSubmitted = false;

            // �� �޴� �߰�
            menuPlate.SpawnNewMenu();
        }
        else
        {
            Debug.Log("����! �޴��� �ٸ��ϴ�");
            ClearPlate();
            isSubmitted = false;
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

    private int CalculateScore(List<int> plateCombo)
    {
        int score = 1000;

        // 1. ���� Ȯ�� (�޴��ǿ� �ش� ���ᰡ �־�� ��)
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
            Debug.Log("�ش� ���ᰡ ���Ե� �޴� ���� �� 0��");
            Tori.Instance.SetState(Tori.CharacterState.Angry, "�̷� �޴��� ���ٳ�!");
            return 0;
        }

        // 2. �ܹ��� Ȯ�� (��� 5�� �̻�����)
        List<int> burger = burgerBowl.GetIngredients();
        if (burger.Count < 5)
        {
            Debug.Log("�ܹ��Ű� 5�� �̸� �� 0��");
            Tori.Instance.SetState(Tori.CharacterState.Angry, "�̷� �޴��� ���ٳ�!");
            return 0;
        }

        // 3. ���� �� Ȯ��
        int bunStart = burger[0];
        int bunEnd = burger[burger.Count - 1];
        if (!IsBun(bunStart) || !IsBun(bunEnd))
        {
            Debug.Log("�ܹ����� ����/���� ���� �ƴ� �� 0��");
            Tori.Instance.SetState(Tori.CharacterState.Angry, "�̷� �޴��� ���ٳ�!");
            return 0;
        }

        // 4. �߰� ��� Ȯ�� (3����)
        List<int> middleIngredients = new List<int>
    {
        burger[1],
        burger[2],
        burger[3]
    };

        // 5. �޴��� ��ġ ���� Ȯ��
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
            Debug.Log("��ġ�ϴ� �޴� ���� �� 0��");
            Tori.Instance.SetState(Tori.CharacterState.Angry, "�̷� �޴��� ���ٳ�!");
            return 0;
        }

        // -------------------------
        // 6. ���� ��Ģ + Tori ����
        // -------------------------
        int burntCount = 0;
        bool potatoBurnt = false;
        bool potatoMissing = false;

        // �� üũ
        if (bunStart == (int)IngredientData.IngredientType.BunCircle2) { score -= 100; burntCount++; }
        if (bunEnd == (int)IngredientData.IngredientType.BunCircle2) { score -= 100; burntCount++; }

        // ��� üũ
        foreach (int mid in middleIngredients)
        {
            if (mid == (int)IngredientData.IngredientType.MeatCircle2)
            {
                score -= 100;
                burntCount++;
            }
        }

        // ���� üũ
        if (plateCombo.Count > 4)
        {
            int potato = plateCombo[3];
            if (potato == (int)IngredientData.IngredientType.PotatoCircle3)
            {
                Debug.Log("�¿� ����Ƣ�� �� -200��");
                score -= 200;
                potatoBurnt = true;
            }
            else if (potato != (int)IngredientData.IngredientType.PotatoCircle2)
            {
                Debug.Log("���� ���� �� -400��");
                score -= 400;
                potatoMissing = true;
            }
        }
        else
        {
            Debug.Log("���� ���� �� -400��");
            score -= 400;
            potatoMissing = true;
        }

        // -------------------------
        // Tori ���� ����
        // -------------------------
        if (score == 1000)
        {
            Tori.Instance.SetState(Tori.CharacterState.Happy, "���ְڴٳ�~!");
        }
        else if (potatoMissing && burntCount == 0)
        {
            Tori.Instance.SetState(Tori.CharacterState.Angry, "���ڸ� �����ϳ�!");
        }
        else if (potatoBurnt && burntCount == 0)
        {
            Tori.Instance.SetState(Tori.CharacterState.Normal, "���ڿ��� ź�����ٳ�..");
        }
        else if (!potatoBurnt && !potatoMissing && burntCount == 1)
        {
            Tori.Instance.SetState(Tori.CharacterState.Normal, "�߸԰ڴٳ�!");
        }
        else if (!potatoBurnt && !potatoMissing && burntCount == 2)
        {
            Tori.Instance.SetState(Tori.CharacterState.Normal, "�������ϴٳ�");
        }
        else if (!potatoBurnt && !potatoMissing && burntCount >= 3)
        {
            Tori.Instance.SetState(Tori.CharacterState.Angry, "�� �丮���������");
        }
        else if (potatoBurnt && burntCount >= 1)
        {
            Tori.Instance.SetState(Tori.CharacterState.Normal, "�� �丮���� �����..");
        }
        else
        {
            Tori.Instance.SetState(Tori.CharacterState.Normal, "����޴ٳ�.");
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

        // ���� �߰� ��� 3�� �� (���/ź��� ���� ����)
        for (int i = 0; i < 3; i++)
        {
            if (!(menuCombo[i] == plateCombo[i] ||
                  (IsMeat(menuCombo[i]) && IsMeat(plateCombo[i]))))
            {
                return false;
            }
        }

        // ���� �� (menuCombo[4] vs plateCombo ������)
        if (menuCombo[4] != plateCombo[plateCombo.Count - 1])
            return false;

        return true;
    }
}
