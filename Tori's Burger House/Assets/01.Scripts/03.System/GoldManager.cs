using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 총 수입금 관리 매니저
/// </summary>
public class GoldManager : MonoBehaviour
{
    public static GoldManager Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI goldText;
    public int gold;
    public int startGold;

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
        gold = startGold;
        UpdateGoldUI();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();

        if (gold <= 0)
        {
            gold = 0;
            SceneManager.LoadScene("Finish");
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
