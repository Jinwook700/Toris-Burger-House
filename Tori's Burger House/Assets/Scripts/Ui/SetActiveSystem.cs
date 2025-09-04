using System.Collections;
using System.Collections.Generic;
using System.Sound;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SetActiveSystem : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    public string soundName;
    void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    public void OnButtonClick()
    {
        if (panel != null)
        {
            if (soundName != null)
            {
                SoundObject _soundObject;
                _soundObject = Sound.Play(soundName, false);
                _soundObject.SetVolume(1.1f);
            }
            panel.SetActive(!panel.activeSelf);
        }
    }
}
