using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Localization.Settings;

public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// O painel de fade que cobre a tela para transi��es.
    /// </summary>
    [Tooltip("O painel de fade que cobre a tela para transi��es.")]
    [SerializeField] Image fadePanel;

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

    private void Start()
    {
        _menusLists = new List<List<Transform>>()
        {
            _startMenuElements,
            _configMenuElements,
            _sonsMenuElements,
            _languageMenuElements
        };


        fadePanel.gameObject.SetActive(true);

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
    public void LoadScene(string sceneName)
    {
        StartCoroutine(OpenScene(sceneName));
    }

    IEnumerator OpenScene(string sceneName)
    {
        fadePanel.color = new Color(0f, 0f, 0f, 0f);
        FadeToDark();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
    #endregion
}
