using TMPro;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro resultGoldText;

    private void Start()
    {
        if (resultGoldText != null && GoldManager.Instance != null)
        {
            resultGoldText.text = GoldManager.Instance.gold.ToString() + "¿ø";
        }
    }
}
