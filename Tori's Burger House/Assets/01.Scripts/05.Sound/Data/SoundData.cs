using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundData
{
    public float masterVolume = 1f;

    public List<SoundVolumeData> typeVolumes = new List<SoundVolumeData>()
    {
        new SoundVolumeData(SoundType.UI, 1f),
        new SoundVolumeData(SoundType.BGM, 1f)
    };

    public float GetVolume(SoundType type)
    {
        SoundVolumeData data = typeVolumes.Find(volumeData => volumeData.type == type);
        return data != null ? data.volume : 1f;
    }

    public void SetVolume(SoundType type, float value)
    {
        SoundVolumeData data = typeVolumes.Find(volumeData => volumeData.type == type);

        if (data != null)
        {
            data.volume = value;
        }
        else
        {
            typeVolumes.Add(new SoundVolumeData(type, value));
        }
    }

    public void EnsureDefaultVolumes()
    {
        if (typeVolumes == null)
        {
            typeVolumes = new List<SoundVolumeData>();
        }

        SetVolumeIfMissing(SoundType.UI, 1f);
        SetVolumeIfMissing(SoundType.BGM, 1f);
    }

    private void SetVolumeIfMissing(SoundType type, float value)
    {
        if (typeVolumes.Exists(volumeData => volumeData.type == type))
        {
            return;
        }

        typeVolumes.Add(new SoundVolumeData(type, value));
    }
}

[System.Serializable]
public class SoundVolumeData
{
    public SoundType type;
    public float volume;

    public SoundVolumeData()
    {
    }

    public SoundVolumeData(SoundType type, float volume)
    {
        this.type = type;
        this.volume = volume;
    }
}
