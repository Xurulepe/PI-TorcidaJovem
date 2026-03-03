using UnityEngine;
using UnityEngine.EventSystems;

public class MenuAudio : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject currentButton;
    [SerializeField] private AudioClip buttonsNavigationAudioClip;

    private void Start()
    {
        eventSystem.SetSelectedGameObject(currentButton);
    }

    private void Update()
    {
        PlaySFXOnCurrentButtonChanged();
        UpdateSelectedButton();
    }

    private void PlaySFXOnCurrentButtonChanged()
    {
        if (eventSystem.currentSelectedGameObject != currentButton)
        {
            AudioManager.Instance.PlaySFX(buttonsNavigationAudioClip);
        }
    }

    private void UpdateSelectedButton()
    {
        if (eventSystem.currentSelectedGameObject != null)
        {
            currentButton = eventSystem.currentSelectedGameObject;
        }
        else
        {
            eventSystem.SetSelectedGameObject(currentButton);
        }
    }
}
