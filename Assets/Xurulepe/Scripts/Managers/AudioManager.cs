using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer _audioMixer;

    [Header("Audio Source")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Header("AudioClips")]
    [SerializeField] private Sound[] _musicSounds;
    [SerializeField] private Sound[] _sfxSounds;

    #region SINGLETON
    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region PLAY SOUNDS
    public void PlayMusic(string name)
    {
        Sound sound = System.Array.Find(_musicSounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogWarning("Música não encontrada!");
            return;
        }

        _musicSource.clip = sound.audioClip;
        _musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        Sound sound = System.Array.Find(_sfxSounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogWarning("SFX não encontrado!");
            return;
        }

        _sfxSource.PlayOneShot(sound.audioClip);
    }
    #endregion

    #region SET VOLUMES
    public void SetMasterVolume(float volume)
    {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
    #endregion
}
