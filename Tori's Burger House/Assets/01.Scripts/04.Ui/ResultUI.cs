using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 게임 결과 UI 표시
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
            int finalGold = GoldManager.Instance.Gold;

            resultGoldText.text = finalGold.ToString() + "��";

            if (finalGold >= 18000)
            {
                resultText.text = "���� Ŭ����!";
                toriText.text = "�����̴ٳ�~!";
                toriRenderer.sprite = spriteList[0];
            }
            else
            {
                resultText.text = "���� ����...";
                toriText.text = "��� ������!!";
                toriRenderer.sprite = spriteList[1];
            }
        }
    }
}
