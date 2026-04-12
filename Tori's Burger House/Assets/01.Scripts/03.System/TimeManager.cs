using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 게임 플레이타임 관리 매니저
/// </summary>
public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [Header("Time Manager Setting")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private float time = 0f;

    //변수
    private float setTime = 180f;
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
        time = setTime;
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

                SceneManager.LoadScene("02.Finish");
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
