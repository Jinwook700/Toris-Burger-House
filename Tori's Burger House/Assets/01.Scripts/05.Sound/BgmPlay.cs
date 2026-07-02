using System.Collections;
using System.Collections.Generic;
using System.Sound;
using UnityEngine;

/// <summary>
/// 배경음악 플레이어
/// </summary>
public class BgmPlay : MonoBehaviour
{
    [Header("BgmPlay Settings")]
    [SerializeField] private string bgmName;
    private bool replay = true;
    private float setVol = 0.1f;
    
    private void Start()
    {
        SoundManager.Instance.StopAllBgm();
        SoundObject _soundObject;
        _soundObject = Sound.Play(bgmName, replay);
        _soundObject.SetVolume(setVol);
    }
}