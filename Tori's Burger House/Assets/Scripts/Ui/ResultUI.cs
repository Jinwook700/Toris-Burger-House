using TMPro;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro resultGoldText;

    private void Start()
    {
        if (resultGoldText != null && GameManager.Instance != null)
        {
            resultGoldText.text = GameManager.Instance.totalGold.ToString() + "¿ø";
        }
    }
}
