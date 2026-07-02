using System.Collections;
using UnityEngine;

namespace System.Sound
{
    /// <summary>
    /// 사운드 실행 오브젝트 스크립트
    /// </summary>
    [Serializable]
    public class SoundObject : MonoBehaviour
    {
        // === External Reference ===
        [SerializeField] private string sourceName = null;
        private SoundManager _soundManager;

        // === Internal Components ===
        private SoundType _soundType;
        private AudioClip _clip;
        private AudioSource _audioSource;
        private float _volume;
        private float _masterVolume = 1f;

        // === State Control ===
        private bool _isInitialized = false;
        public bool IsPlaying
        {
            get { return _audioSource.isPlaying; }
        }

        private void Awake()
        {
            if (SoundDataManager.Instance != null && SoundDataManager.Instance.SoundData != null)
            {
                _masterVolume = SoundDataManager.Instance.SoundData.masterVolume;
            }
            else
            {
                _masterVolume = 1f;
            }
        }

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }
            _soundManager = SoundManager.Instance;
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                _audioSource = gameObject.AddComponent<AudioSource>();
            }
            _audioSource.playOnAwake = false;
            _audioSource.loop = false;

            if (sourceName != null && sourceName.Length != 0)
            {
                SetSoundSourceByName(sourceName);
            }

            _isInitialized = true;
        }
        
        /// <summary>
        /// 문자열 이름기반 오브젝트에 Sound 적용
        /// </summary>
        public void SetSoundSourceByName(string soundSourceName)
        {
            SoundSource soundSource = SoundManager.Instance.SoundSourceList.GetSoundSourceByName(soundSourceName);
            _soundType = soundSource.type;
            _volume = SoundManager.Instance.GetVolume(soundSource.type);
            _clip = soundSource.clip;
        }

        /// <summary>
        /// 루프 적용
        /// </summary>
        public void SetLoop(bool isLoop)
        {
            _audioSource.loop = isLoop;
        }

        /// <summary>
        /// 소리 재생
        /// </summary>
        public IEnumerator Play()
        {
            yield return new WaitUntil(() => _audioSource != null);
            _audioSource.volume = _volume * _masterVolume;
            _audioSource.clip = _clip;
            _audioSource.Play();
            yield return new WaitWhile(() => _audioSource.isPlaying);
        }
        
        /// <summary>
        /// 재생중인 소리 중단
        /// </summary>
        public void Stop()
        {
            _audioSource.Stop();
        }

        /// <summary>
        /// 사운드 타입 반환
        /// </summary>
        public SoundType GetSoundType()
        {
            return _soundType;
        }

        /// <summary>
        /// 오브젝트의 타입별 볼륨값 설정
        /// </summary>
        public void SetVolume(float volume)
        {
            this._volume = volume;
            UpdateAudioSourceVolume();
        }

        public void SetMasterVolume(float masterVolume)
        {
            this._masterVolume = masterVolume;
            UpdateAudioSourceVolume();
        }

        public void PlayWithCallback(ICallback callback)
        {
            StartCoroutine(PlayCoroutine(callback));
        }

        private IEnumerator PlayCoroutine(ICallback callback)
        {
            yield return Play();
            callback?.OnProcessCompleted();
        }
        
        private void UpdateAudioSourceVolume()
        {
            if (_audioSource != null)
            {
                _audioSource.volume = _volume * _masterVolume;
            }
        }
    }
}
