using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI timeText;

    public float time = 0f;
    public float setTime = 180f;
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

                int currentGold = GoldManager.Instance.gold;
                if (currentGold >= 20000)
                {
                    SceneManager.LoadScene("Finish");
                }
                else
                {
                    SceneManager.LoadScene("NotFinish");
                }
            }
        }
    }

    private void UpdateUIText()
    {
        if (timeText != null)
        {
            timeText.text = "���Ը��� : " + Mathf.FloorToInt(time).ToString() + "��";
        }
    }
}
