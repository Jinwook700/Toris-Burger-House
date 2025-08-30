using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    public static Gold Instance { get; private set; }

    [SerializeField]
    public TextMeshProUGUI goldText;

    public int gold;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gold = 10000;
        UpdateGoldUI();
    }

    public void AddGold(int amount)
    {
        if (amount > 0)
        {
            gold += amount;
            UpdateGoldUI();
        }
    }
    private void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = "가게매출 : " + gold.ToString() + "원";
        }
    }
}
