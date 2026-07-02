using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public struct SoundSource
{
    public string name;
    public SoundType type;
    public AudioClip clip;
}

public enum SoundType
{
    UI,
    BGM
}

/// <summary>
/// 사운드 파일 저장 Scriptable Object
/// </summary>
[CreateAssetMenu(fileName = "SoundSourceList", menuName = "ScriptableObject/New SoundSourceList")]
public class SoundSourceList : ScriptableObject
{
    [SerializeField] private List<SoundSource> soundSources = new List<SoundSource>();

    /// <summary>
    /// string에 대응되는 sound source return
    /// </summary>
    public SoundSource GetSoundSourceByName(string name)
    {
        foreach (SoundSource source in soundSources)
        {
            if (source.name.Equals(name))
                return source;
        }

        throw new Exception("SoundSource is not found");
    }
}
