using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button _button;

    [SerializeField] private AudioClip _buttonClickSound;
    [SerializeField] private string _buttonClickSoundName = "ButtonClick";

    void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(PlayButtonSound);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void PlayButtonSound()
    {
        if (_buttonClickSound != null)
        {
            AudioManager.Instance.PlaySFX(_buttonClickSound);
            return;
        }

        AudioManager.Instance.PlaySFX(_buttonClickSoundName);
    }
}
