using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance { get; private set; }

    [Header("UI Scrollbars")]
    public Scrollbar masterVolumeSlider;
    public Scrollbar uiVolumeSlider;
    public Scrollbar bgmVolumeSlider;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InitialSetting();
    }

    public void InitialSetting()
    {
        SoundData soundData = SoundDataManager.Instance.SoundData;

        masterVolumeSlider.value = soundData.masterVolume;
        uiVolumeSlider.value = soundData.GetVolume(SoundType.UI);
        bgmVolumeSlider.value = soundData.GetVolume(SoundType.BGM);

        SoundManager.Instance.SetMasterVolume(soundData.masterVolume);
        foreach (SoundVolumeData volumeData in soundData.typeVolumes)
        {
            SoundManager.Instance.SetVolume(volumeData.type, volumeData.volume);
        }

        masterVolumeSlider.onValueChanged.AddListener(UpdateMasterVolume);
        uiVolumeSlider.onValueChanged.AddListener(UpdateUIVolume);
        bgmVolumeSlider.onValueChanged.AddListener(UpdateBgmVolume);
    }

    private void UpdateMasterVolume(float value)
    {
        SoundDataManager.Instance.SetMasterVolume(value);
    }

    private void UpdateUIVolume(float value)
    {
        SoundDataManager.Instance.SetVolume(SoundType.UI, value);
    }

    private void UpdateBgmVolume(float value)
    {
        SoundDataManager.Instance.SetVolume(SoundType.BGM, value);
    }
}
