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
        // Q Ű�� ������ Normal ���·� ����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetState(CharacterState.Normal);
        }
        // W Ű�� ������ Happy ���·� ����
        else if (Input.GetKeyDown(KeyCode.W))
        {
            SetState(CharacterState.Happy);
        }
        // E Ű�� ������ Angry ���·� ����
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
            TextManager.Instance.SetText(newState.ToString() + "�̴ٳ�" + "\n" + "�ĳĳ�~!");
            Debug.Log("���� ����: " + newState.ToString());
        }
    }
}
