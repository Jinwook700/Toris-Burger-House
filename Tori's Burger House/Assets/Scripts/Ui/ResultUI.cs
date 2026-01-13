using TMPro;
using UnityEngine;

/// <summary>
/// 게임 결과 시 Gold 표시
/// </summary>
public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro resultGoldText;

    private void Start()
    {
        if (resultGoldText != null && GameManager.Instance != null)
        {
            resultGoldText.text = GameManager.Instance.totalGold.ToString() + "원";
        }
    }
}
