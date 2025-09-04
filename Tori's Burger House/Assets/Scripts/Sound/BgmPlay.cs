using System.Collections;
using System.Collections.Generic;
using System.Sound;
using UnityEngine;
using UnityEngine.Apple.ReplayKit;

public class BgmPlay : MonoBehaviour
{
    public string bgmName;
    public bool replay = true;
    public float setVol = 0.1f;
    private void Start()
    {
        SoundManager.Instance.StopAllBgm();
        SoundObject _soundObject;
        _soundObject = Sound.Play(bgmName, replay);
        _soundObject.SetVolume(setVol);
    }
}