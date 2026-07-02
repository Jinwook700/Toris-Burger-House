using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsUI : MonoBehaviour
{
    [Header("UI Scrollbars")]
    [SerializeField] private Scrollbar masterVolumeSlider;
    [SerializeField] private Scrollbar uiVolumeSlider;
    [SerializeField] private Scrollbar bgmVolumeSlider;

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
