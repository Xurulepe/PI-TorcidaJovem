using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Localization.Settings;

public class MainMenuController : MonoBehaviour
{
    [Header("Menu Music")]
    /// <summary>
    /// A música que será reproduzida assim que entrar na cena do menu.
    /// </summary>
    [Tooltip("A música que será reproduzida assim que entrar na cena do menu.")]
    [SerializeField] private AudioClip _mainMenuMusic;

    [Header("3D Virus Animation")]
    /// <summary>
    /// O animador dos vírus 3D do menu.
    /// </summary>
    [Tooltip("O animador dos vírus 3D do menu.")]
    [SerializeField] private MainMenuVirusAnimation _virusAnimation;

    #region LOADING SETTINGS
    [Header("Loading Settings")]
    /// <summary>
    /// O painel de fade que cobre a tela para transições.
    /// </summary>
    [Tooltip("O painel de fade que cobre a tela para transições.")]
    [SerializeField] private Image _fadePanel;

    /// <summary>
    /// A tela de loading que aparece ao carregar uma nova cena.
    /// </summary>
    [Tooltip("A tela de loading que aparece ao carregar uma nova cena.")]
    [SerializeField] private GameObject _loadingScreen;

    /// <summary>
    /// A barra de progresso de carregamento.
    /// </summary>
    [Tooltip("A barra de progresso de carregamento.")]
    [SerializeField] private Image _progressBar;
    #endregion

    #region MENUS
    [Header("Menus")]
    /// <summary>
    /// A lista que contém os painéis dos menus (Start, Configurações, Sons, Idiomas, Créditos).
    /// </summary>
    [Tooltip("A lista que contém os painéis dos menus (Start, Configurações, Sons, Idiomas, Créditos).")]
    [SerializeField] private List<Menu> _menuList;

    /// <summary>
    /// Os elementos que serão animados do menu Start.
    /// </summary>
    [Tooltip("Os elementos que serão animados do menu Start.")]
    [SerializeField] private Menu _startMenu;
    #endregion

    private void Start()
    {
        _fadePanel.gameObject.SetActive(true);

        HideMenuElements(_startMenu);
        FadeToLight();

        StartCoroutine(AnimateMenu(_startMenu));

        AudioManager.Instance.PlayMusic(_mainMenuMusic);
    }

    #region CHANGE MENU
    /// <summary>
    /// Altera o menu ativo, baseado no índice do menu.
    /// </summary>
    /// <param name="menuIndex"> O index do menu que será ativado.</param>
    public void ChangeMenu(int menuIndex)
    {
        if (menuIndex < 0 || menuIndex >= _menuList.Count || menuIndex >= _menuList.Count)
        {
            Debug.LogWarning($"Menu inválido! Index {menuIndex} não existe!");
            return;
        }

        HideOthersMenus(_menuList[menuIndex]);
        StartCoroutine(AnimateMenu(_menuList[menuIndex]));
    }

    /// <summary>
    /// Oculta outros menus, exceto o menu passado como exceção.
    /// </summary>
    /// <param name="menuToKeep"> O menu que será ativo.</param>
    private void HideOthersMenus(Menu menuToKeep)
    {
        foreach (var menu in _menuList)
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
    /// <param name="menu"> O menu que contém a lista de componentes que serão animados.</param>
    private IEnumerator AnimateMenu(Menu menu)
    {
        HideMenuElements(menu);

        foreach (var button in menu.animatedElements)
        {
            button.DOScaleY(1f, .25f);
            yield return new WaitForSeconds(.25f);
        }
    }

    /// <summary>
    /// Oculta os elementos do menu que serão animados.
    /// </summary>
    /// <param name="menu"> O menu que contém a lista de elementos que será ocultada.</param>
    private void HideMenuElements(Menu menu)
    {
        foreach (var element in menu.animatedElements)
        {
            element.localScale = new Vector3(element.localScale.x, 0f, element.localScale.z);
        }
    }
    #endregion

    #region FADE PANEL CONTROLLER
    private void FadeToLight()
    {
        _fadePanel.DOFade(0f, 1f);
    }

    private void FadeToDark()
    {
        _fadePanel.DOFade(1f, 1f);
    }
    #endregion

    public void ChangeLanguage(int languageIndex)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
    }

    #region CHANGE SCENE AND QUIT GAME
    public void LoadScene(int scene_id)
    {
        _virusAnimation.KillTweens();
        StartCoroutine(OpenScene(scene_id));
    }

    private IEnumerator OpenScene(int scene_id)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene_id);

        _loadingScreen.SetActive(true);

        while (!asyncLoad.isDone)
        {
            _progressBar.fillAmount = asyncLoad.progress;
            yield return null;

            if (asyncLoad.progress == 1f)
            {
                break;
            }
        }

        _progressBar.fillAmount = 1f;
        _loadingScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    #endregion
}
