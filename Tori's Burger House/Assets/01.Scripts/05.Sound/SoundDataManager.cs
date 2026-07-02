using System.IO;
using UnityEngine;

/// <summary>
/// SoundData 관리 스크립트
/// </summary>
public class SoundDataManager : MonoBehaviour
{
    public static SoundDataManager Instance { get; private set; }

    private const string FileName = "soundSettings.json";
    private string SavePath => Path.Combine(Application.persistentDataPath, FileName);

    public SoundData SoundData { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadData();
    }

    public void LoadData()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            SoundData = JsonUtility.FromJson<SoundData>(json);
        }
        else
        {
            SoundData = new SoundData();
            SaveData();
        }

        if (SoundData == null)
        {
            SoundData = new SoundData();
        }

        SoundData.EnsureDefaultVolumes();
    }

    public void SaveData()
    {
        string directoryPath = Path.GetDirectoryName(SavePath);
        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string json = JsonUtility.ToJson(SoundData, true);
        File.WriteAllText(SavePath, json);
    }

    public void SetMasterVolume(float volume)
    {
        SoundData.masterVolume = volume;
        SaveData();

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetMasterVolume(volume);
        }
    }

    public void SetVolume(SoundType type, float volume)
    {
        SoundData.SetVolume(type, volume);
        SaveData();

        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetVolume(type, volume);
        }
    }

    public float GetMasterVolume()
    {
        return SoundData.masterVolume;
    }

    public float GetVolume(SoundType type)
    {
        return SoundData.GetVolume(type);
    }
}
