using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseButtonUI;
    [SerializeField] private Image _backgroundPanel;

    [Header("Componentes do menu de pausa")]
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _soundsMenu;

    [Header("Listas de menus")]
    [SerializeField] private List<GameObject> _menus = new List<GameObject>();

    [Header("Controle de botões")]
    [SerializeField] private Button _firstButtonPauseMenu;

    // propriedade estática para verificar se o jogo está pausado
    // substituir por não-static em um GameManager depois
    public static bool IsPaused { get; private set; } = false;

    [Space(20f)]
    // teste de música
    public AudioClip music;

    private void Start()
    {
        _menus = new List<GameObject>()
        {
            _pauseMenuPanel,
            _settingsMenu,
            _soundsMenu
        };

        if (music != null)
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
            menu.SetActive(false);
        }

        AnimatePauseMenuIn(_menus[index]);
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

        Debug.Log("Pausando o jogo");
        IsPaused = true;

        AnimatePauseMenuIn(_pauseMenuPanel);
        FadeBackgroundPanel(0.7f, 0.5f, true);
    }

    public void Resume()
    {
        _pauseButtonUI.SetActive(true);

        Debug.Log("Retomando o jogo");
        IsPaused = false;

        foreach (var menu in _menus)
        {
            if (menu.activeSelf)
            {
                AnimatePauseMenuOut(menu);
            }
        }

        FadeBackgroundPanel(0f, 0.25f, false);
    }

    public void BackToMenu()
    {
        IsPaused = false;
        SceneManager.LoadScene(0);
    }
    #endregion

    #region ANIMATIONS
    private void AnimatePauseMenuIn(GameObject gameObject)
    {
        gameObject.transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        gameObject.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    private void AnimatePauseMenuOut(GameObject gameObject)
    {
        gameObject.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
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
