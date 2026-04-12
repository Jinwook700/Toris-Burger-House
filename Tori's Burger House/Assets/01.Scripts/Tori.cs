using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum CharacterState
{
    Normal = 0,
    Happy = 1,
    Angry = 2
}

/// <summary>
/// 토리 캐릭터 상태 관리 클래스
/// </summary>
public class Tori : MonoBehaviour
{
    public static Tori Instance { get; private set; }

    [Header("토리 세팅")]
    [SerializeField] private TextMeshPro toriStateText;

    //내부 컴포넌트
    private Animator animator;

    /*private bool isNormal = true;
    private bool isHappy = false;
    private bool isAngry = false;*/

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetState(CharacterState.Normal, "햄버거를\n만들어보자냥");
    }

    /// <summary>
    /// 토리의 상태 변경, 메세지 변경
    /// </summary>
    public void SetState(CharacterState newState, string textMessage)
    {
        if (animator != null)
        {
            animator.SetFloat("state", (int)newState);
            toriStateText.text = textMessage;
        }
    }
}
