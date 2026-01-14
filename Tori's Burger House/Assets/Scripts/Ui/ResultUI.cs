using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 게임 결과 시 UI 표시
/// </summary>
public class ResultUI : MonoBehaviour
{
    [Header("UI Table")]
    [SerializeField] private TextMeshPro resultText;
    [SerializeField] private TextMeshPro resultGoldText;

    [Header("Tori UI")]
    [SerializeField] private TextMeshPro toriText;
    [SerializeField] private SpriteRenderer toriRenderer;
    [SerializeField] private List<Sprite> spriteList;

    private void Start()
    {
        if (resultGoldText != null && GoldManager.Instance != null)
        {
            int finalGold = GoldManager.Instance.gold;

            resultGoldText.text = finalGold.ToString() + "원";

            if (finalGold >= 18000)
            {
                resultText.text = "게임 클리어!";
                toriText.text = "성공이다냥~!";
                toriRenderer.sprite = spriteList[0];
            }
            else
            {
                resultText.text = "게임 실패...";
                toriText.text = "장사 접어라냥!!";
                toriRenderer.sprite = spriteList[1];
            }
        }
    }
}
