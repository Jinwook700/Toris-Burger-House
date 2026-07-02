using System.Collections;
using System.Collections.Generic;
using System.Sound;
using UnityEngine;

/// <summary>
/// 배경음악 플레이어
/// </summary>
public class BgmPlayer : MonoBehaviour
{
    [Header("BgmPlay Settings")]
    [SerializeField] private string bgmName;
    [SerializeField ]private bool replay = true;
    [SerializeField] private float setVol = 0.1f;
    
    private void Start()
    {
        SoundManager.Instance.StopAllBgm();
        SoundPlayer.Play(bgmName, replay, setVol);
    }
}