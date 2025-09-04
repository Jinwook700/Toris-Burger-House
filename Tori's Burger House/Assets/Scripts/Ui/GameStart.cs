using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Sound;
using UnityEngine.UIElements;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    string sceneName;
    public string soundName;
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
