using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Sound;
using UnityEngine.UIElements;

/// <summary>
/// GameScene으로 이동합니다.
/// </summary>
public class GameStart : MonoBehaviour
{
    [Header("Button Setting")]
    [SerializeField] private string sceneName;
    [SerializeField] private string soundName;
    public void OnButtonClick()
    {
        if (soundName != null)
        {
            SoundObject _soundObject;
            _soundObject = Sound.Play(soundName, false);
            _soundObject.SetVolume(0.9f);
        }
        SceneManager.LoadScene(sceneName);
    }
}
