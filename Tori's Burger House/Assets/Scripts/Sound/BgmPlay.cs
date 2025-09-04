using System.Collections;
using System.Collections.Generic;
using System.Sound;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;

public class BgmPlay : MonoBehaviour
{
    public string bgmName;
    public bool replay = true;
    private void Start()
    {
        SoundManager.Instance.StopAllBgm();
        SoundObject _soundObject;
        _soundObject = Sound.Play(bgmName, replay);
    }
}