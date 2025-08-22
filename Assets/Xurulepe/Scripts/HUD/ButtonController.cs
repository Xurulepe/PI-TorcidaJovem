using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    private Button _button;

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
        AudioManager.Instance.PlaySFXByName("ButtonClick");
    }
}
