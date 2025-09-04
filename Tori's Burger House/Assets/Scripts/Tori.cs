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
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        SetState(CharacterState.Normal, "�ܹ��Ÿ�\n�����ڳ�");
    }
    public void SetState(CharacterState newState, string textMessage)
    {
        if (animator != null)
        {
            animator.SetFloat("state", (int)newState);
            if (TextManager.Instance != null)
            {
                TextManager.Instance.SetText(textMessage);
            }
            
            Debug.Log("���� ����: " + newState.ToString());
        }
    }
}
