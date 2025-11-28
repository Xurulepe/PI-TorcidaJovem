using UnityEngine;
using UnityEngine.UI;

public class AudioHelper : MonoBehaviour
{
    [Header("Audio Sliders")]
    [Tooltip("Slider que controla o volume master")]
    public Slider MasterSlider;

    [Tooltip("Slider que controla o volume da música")]
    public Slider MusicSlider;

    [Tooltip("Slider que controla o volume dos efeitos sonoros")]
    public Slider SFXSlider;

    private void Start()
    {
        AudioManager.Instance.GetSliders(this);
        AudioManager.Instance.LoadVolume();
    }

    private void OnEnable()
    {
        MasterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
        MusicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        SFXSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }

    private void OnDisable()
    {
        MasterSlider.onValueChanged.RemoveAllListeners();
        MusicSlider.onValueChanged.RemoveAllListeners();
        SFXSlider.onValueChanged.RemoveAllListeners();
    }
}
