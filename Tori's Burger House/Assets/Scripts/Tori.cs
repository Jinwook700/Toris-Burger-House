using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tori : MonoBehaviour
{
    public static Tori Instance { get; private set; }

    private Animator animator;

    public bool isNormal = true;
    public bool isHappy = false;
    public bool isAngry = false;

    public enum CharacterState
    {
        Normal = 0,
        Happy = 1,
        Angry = 2
    }

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
        animator = GetComponent<Animator>();

        SetState(CharacterState.Normal);
    }

    private void Update()
    {
        // Q 키를 누르면 Normal 상태로 변경
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetState(CharacterState.Normal);
        }
        // W 키를 누르면 Happy 상태로 변경
        else if (Input.GetKeyDown(KeyCode.W))
        {
            SetState(CharacterState.Happy);
        }
        // E 키를 누르면 Angry 상태로 변경
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SetState(CharacterState.Angry);
        }
    }

    public void SetState(CharacterState newState)
    {
        if (animator != null)
        {
            animator.SetFloat("state", (int)newState);
            TextManager.Instance.SetText(newState.ToString() + "이다냥" + "\n" + "냐냐냥~!");
            Debug.Log("현재 상태: " + newState.ToString());
        }
    }
}
