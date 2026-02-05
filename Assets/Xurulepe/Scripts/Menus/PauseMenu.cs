using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseButtonUI;
    [SerializeField] private Image _backgroundPanel;
    [SerializeField] private GameObject _backgroundImage;

    [Header("Componentes do menu de pausa")]
    [SerializeField] private Menu _pauseMenu;
    [SerializeField] private Menu _settingsMenu;
    [SerializeField] private Menu _soundsMenu;

    [Header("Listas de menus")]
    [SerializeField] private List<Menu> _menus = new List<Menu>();

    [Header("Controle de botões")]
    [SerializeField] private Button _firstButtonPauseMenu;

    // propriedade para verificar se o jogo está pausado
    // substituir em um GameManager depois
    public bool IsPaused { get; private set; } = false;

    [Space(20f)]
    // teste de música
    public AudioClip music;

    private void Start()
    {
        _menus = new List<Menu>()
        {
            _pauseMenu,
            _settingsMenu,
            _soundsMenu
        };

        if (music != null && AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayMusic(music);
        }

        _backgroundPanel.color = new Color(0f, 0f, 0f, 0f);
    }

    public void ChangeMenu(int index)
    {
        if (index < 0 || index >= _menus.Count)
        {
            Debug.LogWarning($"Menu inválido! Index {index} não existe!");
            return;
        }

        foreach (var menu in _menus)
        {
            menu.gameObject.SetActive(false);
        }

        AnimateMenuElements(_menus[index]);
    }

    #region INPUT
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (IsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    #endregion

    #region PAUSE/RESUME/EXIT
    public void Pause()
    {
        _pauseButtonUI.SetActive(false);
        _firstButtonPauseMenu.Select();
        _backgroundImage.SetActive(true);

        Debug.Log("Pausando o jogo");
        IsPaused = true;

        AnimateMenuElements(_pauseMenu);
        FadeBackgroundPanel(0.7f, 0.5f, true);
    }

    public void Resume()
    {
        _pauseButtonUI.SetActive(true);
        _backgroundImage.SetActive(false);

        Debug.Log("Retomando o jogo");
        IsPaused = false;

        foreach (var menu in _menus)
        {
            if (menu.gameObject.activeSelf)
            {
                AnimatePauseMenuOut(menu);
            }
        }

        FadeBackgroundPanel(0f, 0.25f, false);
    }

    public void BackToMenu()
    {
        IsPaused = false;

        DOTween.KillAll();
        SceneManager.LoadScene(0);
    }
    #endregion

    #region ANIMATIONS
    private void AnimateMenuElements(Menu menu)
    {
        menu.gameObject.SetActive(true);
        menu.transform.localScale = Vector3.one;

        HideMenuElements(menu);

        foreach (Transform menuElement in menu.animatedElements)
        {
            menuElement.DOScale(1f, 0.25f);
        }
    }

    private void HideMenuElements(Menu menu)
    {
        foreach (var element in menu.animatedElements)
        {
            element.localScale = new Vector3(element.localScale.x, 0f, element.localScale.z);
        }
    }

    private void AnimatePauseMenuOut(Menu menu)
    {

        foreach (Transform menuElement in menu.animatedElements)
        {
            menuElement.DOScale(0f, 0.25f).OnComplete(() =>
            {
                menu.gameObject.SetActive(false);
            });
        }
    }

    private void FadeBackgroundPanel(float alphaValue, float duration, bool activeOnComplete = true)
    {
        _backgroundPanel.DOKill();

        _backgroundPanel.gameObject.SetActive(true);

        _backgroundPanel.DOFade(alphaValue, duration).OnComplete(() =>
        {
            _backgroundPanel.gameObject.SetActive(activeOnComplete);
        });
    }
    #endregion
}
