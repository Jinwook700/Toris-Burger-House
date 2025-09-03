using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance { get; private set; }

    [SerializeField]
    public TextMeshProUGUI goldText;

    public int gold;

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

    private void Start()
    {
        gold = 10000;
        UpdateGoldUI();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();

        int currentGold = GoldManager.Instance.gold;
        if (gold <= 0)
        {
            SceneManager.LoadScene("NotFinish");
            GameManager.Instance.totalGold = currentGold;
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
