using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseButtonUI;

    [Header("Componentes do menu de pausa")]
    [SerializeField] private GameObject _pauseMenuPanel;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _soundsMenu;

    [Header("Listas de menus")]
    [SerializeField] private List<GameObject> _menus = new List<GameObject>();

    [Header("Controle de bot�es")]
    [SerializeField] private Button _firstButtonPauseMenu;

    // propriedade est�tica para verificar se o jogo est� pausado
    // substituir por n�o-static em um GameManager depois
    public static bool IsPaused { get; private set; } = false;

    [Space(20f)]
    // teste de m�sica
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
    }

    public void ChangeMenu(int index)
    {
        if (index < 0 || index >= _menus.Count)
        {
            Debug.LogWarning($"Menu inv�lido! Index {index} n�o existe!");
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
    #endregion
}
