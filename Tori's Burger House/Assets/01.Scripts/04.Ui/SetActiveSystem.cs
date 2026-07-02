using System.Collections;
using System.Collections.Generic;
using System.Sound;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 패널 열기와 닫기 처리
/// </summary>
public class SetActiveSystem : MonoBehaviour
{
    [Header("패널 설정")]
    [SerializeField] private GameObject panel;
    [SerializeField] private string soundName;

    public void OnButtonClick()
    {
        if (panel != null)
        {
            if (soundName != null)
            {
                SoundObject _soundObject;
                _soundObject = SoundPlayer.Play(soundName, false);
                _soundObject.SetVolume(1.1f);
            }
            panel.SetActive(!panel.activeSelf);
        }
    }
}
