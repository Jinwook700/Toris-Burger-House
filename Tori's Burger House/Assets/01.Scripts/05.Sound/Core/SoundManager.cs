using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Sound;

/// <summary>
/// 게임 전체 사운드 관리 시스템
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    // === External Reference ===
    [SerializeField] private SoundSourceList soundSourceList;
    [SerializeField] private List<SoundObject> soundObjects;
    private SoundObject bgmSoundObject;
    
    // === Internal Components ===
    private Dictionary<SoundType, float> volumes = new Dictionary<SoundType, float>();
    
    // === Settings & Visuals ===
    private const float DEFAULT_VOLUME = 1;
    
    // === Property ===
    public SoundSourceList SoundSourceList => soundSourceList;

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
            return;
        }

        InitializeVolumes();
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    private void Start()
    {
        GetSoundObjects();
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetSoundObjects();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// 타입별 볼륨 크기 설정
    /// </summary>
    public void SetVolume(SoundType type, float volume)
    {
        volumes[type] = volume;

        foreach (SoundObject soundObject in soundObjects)
        {
            if (soundObject.GetSoundType() == type)
            {
                soundObject.SetVolume(volume);
            }
        }
    }

    /// <summary>
    /// 전체 마스터 볼륨 설정
    /// </summary>
    public void SetMasterVolume(float masterVolume)
    {
        foreach (SoundObject soundObject in soundObjects)
        {
            soundObject.SetMasterVolume(masterVolume);
        }
    }

    /// <summary>
    /// 볼륨 읽어오기
    /// </summary>
    public float GetVolume(SoundType type)
    {
        try
        {
            return volumes[type];
        }
        catch (KeyNotFoundException)
        {
            InitializeVolumes();
            return volumes[type];
        }
    }

    public void StopAllBgm()
    {
        foreach (SoundObject soundObject in soundObjects)
        {
            if (soundObject.GetSoundType() == SoundType.BGM && soundObject.IsPlaying)
            {
                soundObject.Stop();
            }
        }
    }
    
    /// <summary>
    /// 최초 실행시 Volume DEFAULT_VOLUME으로 초기화
    /// </summary>
    private void InitializeVolumes()
    {
        foreach (SoundType type in Enum.GetValues(typeof(SoundType)))
        {
            volumes[type] = DEFAULT_VOLUME;
        }
    }
    
    private void GetSoundObjects()
    {
        soundObjects = new List<SoundObject>(FindObjectsOfType<SoundObject>());
    }
}
