using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI timeText;

    public float time = 0f;
    private bool isTimerRunning = false;

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
        time = 180f;
        isTimerRunning = true;
        UpdateUIText();
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                UpdateUIText();
            }
            else
            {
                time = 0;
                isTimerRunning = false;
                UpdateUIText();
            }
        }
    }

    private void UpdateUIText()
    {
        if (timeText != null)
        {
            timeText.text = "가게마감 : " + Mathf.FloorToInt(time).ToString() + "초";
        }
    }
}
