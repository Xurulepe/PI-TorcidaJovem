using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

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

    [Header("UI Sliders")]
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _sFXVolumeSlider;

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
    /// <summary>
    /// Procura uma música pelo nome e a reproduz.
    /// </summary>
    /// <param name="name"> Nome da música a ser procurada no AudioManager.</param>
    public void PlayMusic(string name)
    {
        Sound sound = System.Array.Find(_musicSounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogWarning("Música " + name + " não encontrada!");
            return;
        }

        _musicSource.clip = sound.audioClip;
        _musicSource.Play();
    }

    /// <summary>
    /// Reproduz uma música a partir de um AudioClip.
    /// </summary>
    /// <param name="clip"> Clip da música a ser reproduzida.</param>
    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Clip de música é nulo!");
            return;
        }

        _musicSource.clip = clip;
        _musicSource.Play();
    }

    /// <summary>
    /// Procura um efeito sonoro pelo nome e o reproduz.
    /// </summary>
    /// <param name="name"> Nome do efeito sonoro a ser procurado no AudioManager. </param>
    public void PlaySFX(string name)
    {
        Sound sound = System.Array.Find(_sfxSounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogWarning("SFX " + name + " não encontrado!");
            return;
        }

        _sfxSource.PlayOneShot(sound.audioClip);
    }

    /// <summary>
    /// Reproduz um efeito sonoro a partir de um AudioClip.
    /// </summary>
    /// <param name="clip"> Clip do efeito sonoro a ser reproduzido.</param>
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Clip de SFX é nulo!");
            return;
        }

        _sfxSource.PlayOneShot(clip);
    }
    #endregion

    #region SET VOLUMES
    // Define os volumes de áudio pelos sliders

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

    /// <summary>
    /// Seta os sliders de volume a partir do HUDController de cada cena.
    /// </summary>
    /// <param name="hud">O HUDControler presente na cena que contém os sliders.</param>
    public void GetSliders(HUDController hud)
    {
        _masterVolumeSlider = hud.MasterSlider;
        _musicVolumeSlider = hud.MusicSlider;
        _sFXVolumeSlider = hud.SFXSlider;
    }


}
