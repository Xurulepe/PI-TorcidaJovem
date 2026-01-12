using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    private Button _button;

    [SerializeField] private AudioClip _buttonClickSound;

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
        _button.onClick.RemoveListener(PlayButtonSound);
    }

    private void PlayButtonSound()
    {
        if (_buttonClickSound != null)
        {
            AudioManager.Instance.PlaySFX(_buttonClickSound);
        }
    }
}
