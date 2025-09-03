using System.Collections;
using System.Collections.Generic;
using System.Sound;
using UnityEngine;

public class BgmPlay : MonoBehaviour
{
    public string bgmName;
    private void Start()
    {
        SoundObject _soundObject;
        _soundObject = Sound.Play(bgmName, true);
    }
}