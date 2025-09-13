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

    [Header("componentes")]
    [SerializeField] Animator _animator;
    [SerializeField] GameObject _virus1;
    [SerializeField] GameObject _virus2;
    [SerializeField] GameObject _virus3;

    /// <summary>
    /// O painel de fade que cobre a tela para transi��es.
    /// </summary>
    [Tooltip("O painel de fade que cobre a tela para transi��es.")]
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
    /// Os pain�is dos menus (Start, Configura��es, Sons, Idiomas).
    /// </summary>
    [Tooltip("Os pain�is dos menus (Start, Configura��es, Sons, Idiomas).")]
    [SerializeField] List<Transform> _menuPanels = new List<Transform>();

    #region MENUS ELEMENTS
    /// <summary>
    /// Os elementos que ser�o animados do menu Start.
    /// </summary>
    [Tooltip("Os elementos que ser�o animados do menu Start.")]
    [SerializeField] List<Transform> _startMenuElements = new List<Transform>();

    /// <summary>
    /// Os elementos que ser�o animados do menu de Configura��es.
    /// </summary>
    [Tooltip("Os elementos que ser�o animados do menu de Configura��es.")]
    [SerializeField] List<Transform> _configMenuElements = new List<Transform>();

    /// <summary>
    /// Os elementos que ser�o animados do menu de Cr�ditos.
    /// </summary>
    [Tooltip("Os elementos que ser�o animados do menu de Cr�ditos.")]
    [SerializeField] List<Transform> _creditMenuElements = new List<Transform>();

    /// <summary>
    /// Os elementos que ser�o animados do menu de Sons.
    /// </summary>
    [Tooltip("Os elementos que ser�o animados do menu de Sons.")]
    [SerializeField] List<Transform> _sonsMenuElements = new List<Transform>();

    /// <summary>
    /// Os elementos que ser�o animados do menu de Idiomas.
    /// </summary>
    [Tooltip("Os elementos que ser�o animados do menu de Idiomas.")]
    [SerializeField] List<Transform> _languageMenuElements = new List<Transform>();
    #endregion

    /// <summary>
    /// A lista de listas que cont�m os elementos de cada menu.
    /// </summary>
    [Tooltip("A lista de listas que cont�m os elementos de cada menu.")]
    List<List<Transform>> _menusLists = new List<List<Transform>>();

    #region controle de dotoween
    [Header("controle de anima��o do toween")]
    public float escalaMin ;   // escala m�nima do pulso
    public float escalaMax ;   // escala m�xima do pulso

    public float escalaMin1;   // escala m�nima do pulso
    public float escalaMax1;   // escala m�xima do pulso

    public float escalaMin2;   // escala m�nima do pulso
    public float escalaMax2;   // escala m�xima do pulso

    public float duracaoMin ;  // dura��o m�nima
    public float duracaoMax ;  // dura��o m�xima
    public Ease ease = Ease.InOutSine; // suaviza��o

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
        AudioManager.Instance.PlayMusic("MenuMusic");
    }

    #region CHANGE MENU
    /// <summary>
    /// Altera o menu ativo, baseado no �ndice do menu.
    /// </summary>
    /// <param name="menuIndex"> O index do menu que ser� ativado.</param>
    public void ChangeMenu(int menuIndex)
    {
        if (menuIndex < 0 || menuIndex >= _menusLists.Count || menuIndex >= _menuPanels.Count)
        {
            Debug.LogWarning($"Menu inv�lido! Index {menuIndex} n�o existe!");
            return;
        }

        HideOthersMenus(_menuPanels[menuIndex]);
        StartCoroutine(AnimateMenu(_menusLists[menuIndex]));
    }

    /// <summary>
    /// Oculta outros menus, exceto o menu passado como exce��o.
    /// </summary>
    /// <param name="menuToKeep"> O menu que ser� ativo.</param>
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
    /// Anima os elementos do menu, fazendo com que eles apare�am um de cada vez.
    /// </summary>
    /// <param name="menuButtons"> A lista de componentes do menu que ser� animado.</param>
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
    /// Oculta os elementos do menu que ser�o animados.
    /// </summary>
    /// <param name="menusElements"> A lista de elementos que ser� ocultada.</param>
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
