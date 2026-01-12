using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Localization.Settings;
using JetBrains.Annotations;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private AudioClip _mainMenuMusic;

    [Header("componentes")]
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _virus1;
    [SerializeField] GameObject _virus2;
    [SerializeField] GameObject _virus3;

    /// <summary>
    /// O painel de fade que cobre a tela para transições.
    /// </summary>
    [Tooltip("O painel de fade que cobre a tela para transições.")]
    [SerializeField] Image fadePanel;

    /// <summary>
    /// A tela de loading que aparece ao carregar uma nova cena.
    /// </summary>
    [Tooltip("A tela de loading que aparece ao carregar uma nova cena.")]
    [SerializeField] GameObject loadingScreen;

    /// <summary>
    /// A barra de progresso de carregamento.
    /// </summary>
    [Tooltip("A barra de progresso de carregamento.")]
    [SerializeField] Image progressBar;

    [Space]
    /// <summary>
    /// Os painéis dos menus (Start, Configurações, Sons, Idiomas).
    /// </summary>
    [Tooltip("Os painéis dos menus (Start, Configurações, Sons, Idiomas).")]
    [SerializeField] List<Transform> _menuPanels = new List<Transform>();

    #region MENUS ELEMENTS
    /// <summary>
    /// Os elementos que serão animados do menu Start.
    /// </summary>
    [Tooltip("Os elementos que serão animados do menu Start.")]
    [SerializeField] List<Transform> _startMenuElements = new List<Transform>();

    /// <summary>
    /// Os elementos que serão animados do menu de Configurações.
    /// </summary>
    [Tooltip("Os elementos que serão animados do menu de Configurações.")]
    [SerializeField] List<Transform> _configMenuElements = new List<Transform>();

    /// <summary>
    /// Os elementos que serão animados do menu de Créditos.
    /// </summary>
    [Tooltip("Os elementos que serão animados do menu de Créditos.")]
    [SerializeField] List<Transform> _creditMenuElements = new List<Transform>();

    /// <summary>
    /// Os elementos que serão animados do menu de Sons.
    /// </summary>
    [Tooltip("Os elementos que serão animados do menu de Sons.")]
    [SerializeField] List<Transform> _sonsMenuElements = new List<Transform>();

    /// <summary>
    /// Os elementos que serão animados do menu de Idiomas.
    /// </summary>
    [Tooltip("Os elementos que serão animados do menu de Idiomas.")]
    [SerializeField] List<Transform> _languageMenuElements = new List<Transform>();
    #endregion

    /// <summary>
    /// A lista de listas que contém os elementos de cada menu.
    /// </summary>
    [Tooltip("A lista de listas que contém os elementos de cada menu.")]
    List<List<Transform>> _menusLists = new List<List<Transform>>();

    #region controle de dotoween
    [Header("controle de animação do toween")]
    public float escalaMin ;   // escala mínima do pulso
    public float escalaMax ;   // escala máxima do pulso

    public float escalaMin1;   // escala mínima do pulso
    public float escalaMax1;   // escala máxima do pulso

    public float escalaMin2;   // escala mínima do pulso
    public float escalaMax2;   // escala máxima do pulso

    public float duracaoMin ;  // duração mínima
    public float duracaoMax ;  // duração máxima
    public Ease ease = Ease.InOutSine; // suavização

    private Tween tween1, tween2, tween3;

    #endregion
    private void Start()
    {
        _menusLists = new List<List<Transform>>()
        {
            _startMenuElements,
            _configMenuElements,
            _creditMenuElements,
            _sonsMenuElements,
            _languageMenuElements
        };


        fadePanel.gameObject.SetActive(true);
        IniciarPulso1(_virus1.transform);
        IniciarPulso2(_virus2.transform);
        IniciarPulso3(_virus3.transform);

        HideMenuElements(_startMenuElements);
        FadeToLight();
        StartCoroutine(AnimateMenu(_startMenuElements));
        AudioManager.Instance.PlayMusic(_mainMenuMusic);
    }

    #region CHANGE MENU
    /// <summary>
    /// Altera o menu ativo, baseado no índice do menu.
    /// </summary>
    /// <param name="menuIndex"> O index do menu que será ativado.</param>
    public void ChangeMenu(int menuIndex)
    {
        if (menuIndex < 0 || menuIndex >= _menusLists.Count || menuIndex >= _menuPanels.Count)
        {
            Debug.LogWarning($"Menu inválido! Index {menuIndex} não existe!");
            return;
        }

        HideOthersMenus(_menuPanels[menuIndex]);
        StartCoroutine(AnimateMenu(_menusLists[menuIndex]));
    }

    /// <summary>
    /// Oculta outros menus, exceto o menu passado como exceção.
    /// </summary>
    /// <param name="menuToKeep"> O menu que será ativo.</param>
    private void HideOthersMenus(Transform menuToKeep)
    {
        foreach (var menu in _menuPanels)
        {
            menu.gameObject.SetActive(false);
        }

        menuToKeep.gameObject.SetActive(true);
    }
    #endregion

    #region ANIMATE MENU
    /// <summary>
    /// Anima os elementos do menu, fazendo com que eles apareçam um de cada vez.
    /// </summary>
    /// <param name="menuButtons"> A lista de componentes do menu que será animado.</param>
    private IEnumerator AnimateMenu(List<Transform> menuButtons)
    {
        HideMenuElements(menuButtons);

        foreach (var button in menuButtons)
        {
            button.DOScaleY(1f, .25f);
            yield return new WaitForSeconds(.25f);
        }
    }

    /// <summary>
    /// Oculta os elementos do menu que serão animados.
    /// </summary>
    /// <param name="menusElements"> A lista de elementos que será ocultada.</param>
    private void HideMenuElements(List<Transform> menusElements)
    {
        foreach (var element in menusElements)
        {
            element.localScale = new Vector3(element.localScale.x, 0f, element.localScale.z);
        }
    }

    public void AnimaBackGroundTrue()
    {
        _animator.SetBool("Move", true);
    }

    public void AnimaBackGroundFalse()
    {
        _animator.SetBool("Move", false);
    }

    void IniciarPulso1(Transform transobj)
    {
        float escalaAlvo = Random.Range(escalaMin, escalaMax);
        float duracao = Random.Range(duracaoMin, duracaoMax);

        tween1?.Kill();
        tween1 = transobj.DOScale(escalaAlvo, duracao)
            .SetEase(ease)
            .OnComplete(() => IniciarPulso1(transobj));
    }

    void IniciarPulso2(Transform transobj)
    {
        float escalaAlvo = Random.Range(escalaMin1, escalaMax1);
        float duracao = Random.Range(duracaoMin, duracaoMax);

        tween2?.Kill();
        tween2 = transobj.DOScale(escalaAlvo, duracao)
            .SetEase(ease)
            .OnComplete(() => IniciarPulso2(transobj));
    }

    void IniciarPulso3(Transform transobj)
    {
        float escalaAlvo = Random.Range(escalaMin2, escalaMax2);
        float duracao = Random.Range(duracaoMin, duracaoMax);

        tween3?.Kill();
        tween3 = transobj.DOScale(escalaAlvo, duracao)
            .SetEase(ease)
            .OnComplete(() => IniciarPulso3(transobj));
    }

    #endregion

    #region FADE PANEL CONTROLLER
    private void FadeToLight()
    {
        fadePanel.DOFade(0f, 1f);
    }

    private void FadeToDark()
    {
        fadePanel.DOFade(1f, 1f);
    }
    #endregion

    public void ChangeLanguage(int languageIndex)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
    }

    #region CHANGE SCENE AND QUIT GAME
    public void LoadScene(int scene_id)
    {
        StartCoroutine(OpenScene(scene_id));
    }

    IEnumerator OpenScene(int scene_id)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene_id);

        loadingScreen.SetActive(true);

        while (!asyncLoad.isDone)
        {
            progressBar.fillAmount = asyncLoad.progress;
            yield return null;
        }

        progressBar.fillAmount = 1f;
        loadingScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    #endregion
}
