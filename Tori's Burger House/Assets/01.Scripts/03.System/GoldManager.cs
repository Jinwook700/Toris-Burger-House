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

    [Header("Gold Manager Setting")]
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private int gold;
    [SerializeField] private int startGold;

    //프로퍼티
    public int Gold => gold;

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

    /// <summary>
    /// 골드 수량 더하기
    /// </summary>
    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();

        if (gold <= 0)
        {
            gold = 0;
            SceneManager.LoadScene("02.Finish");
        }
    }

    /// <summary>
    /// 골드 Text 최신화
    /// </summary>
    private void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = "가게매출 : " + gold.ToString() + "원";
        }
    }
}
