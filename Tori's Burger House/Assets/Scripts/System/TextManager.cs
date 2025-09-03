using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    public static TextManager Instance { get; private set; }

    [SerializeField]
    private TextMeshPro toriStateText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetText(string text)
    {
        if (toriStateText != null)
        {
            toriStateText.text = text;
        }
    }
}
