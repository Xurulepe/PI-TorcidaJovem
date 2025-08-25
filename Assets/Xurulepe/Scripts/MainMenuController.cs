using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Localization.Settings;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Image fadePanel;
    [SerializeField] List<Transform> _menuPanels = new List<Transform>();
    [SerializeField] List<Transform> _startMenuButtons = new List<Transform>();
    [SerializeField] List<Transform> _configMenuButtons = new List<Transform>();
    [SerializeField] List<Transform> _sonsMenuButtons = new List<Transform>();
    [SerializeField] List<Transform> _languageMenuButtons = new List<Transform>();

    private void Start()
    {
        fadePanel.gameObject.SetActive(true);

        HideButtons(_startMenuButtons);

        FadeToLight();
        StartCoroutine(MenuAnimations(_startMenuButtons));

        AudioManager.Instance.PlayMusic("MenuMusic");
    }

    void HideButtons(List<Transform> buttonsList)
    {
        foreach (var button in buttonsList)
        {
            button.localScale = new Vector3(button.localScale.x, 0f, button.localScale.z);
        }
    }

    private IEnumerator MenuAnimations(List<Transform> menuButtons)
    {
        //yield return new WaitForSeconds(1.25f);
        HideButtons(menuButtons);

        foreach (var button in menuButtons)
        {
            button.DOScaleY(1f, .25f);
            yield return new WaitForSeconds(.25f);
        }
    }

    void HideOthersMenus(Transform menuException)
    {
        foreach (var menu in _menuPanels)
        {
            menu.gameObject.SetActive(false);
        }

        menuException.gameObject.SetActive(true);
    }

    public void ChangeMenu(int menuIndex)
    {
        switch (menuIndex)
        {
            case 0:
                HideOthersMenus(_menuPanels[0]);
                StartCoroutine(MenuAnimations(_startMenuButtons));
                break;
            case 1:
                HideOthersMenus(_menuPanels[1]);
                StartCoroutine(MenuAnimations(_configMenuButtons));
                break;
            case 2:
                HideOthersMenus(_menuPanels[2]);
                StartCoroutine(MenuAnimations(_sonsMenuButtons));
                break;
            case 3:
                HideOthersMenus(_menuPanels[3]);
                StartCoroutine(MenuAnimations(_languageMenuButtons));
                break;
            default:
                Debug.LogWarning("Menu inválido! Index " + menuIndex + " não existe!");
                break;
        }
    }

    #region FADE PANEL CONTROLLER
    private void FadeToLight()
    {
        fadePanel.DOFade(0f, 1f).OnComplete(HideFadePanel);
    }

    private void FadeToDark()
    {
        fadePanel.DOFade(1f, 1f);
    }

    void ShowFadePanel()
    {
        fadePanel.transform.localScale = Vector3.one;
    }

    void HideFadePanel()
    {
        fadePanel.transform.localScale = Vector3.zero;
    }
    #endregion

    public void ChangeLanguage(int languageIndex)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(OpenScene(sceneName));
    }

    IEnumerator OpenScene(string sceneName)
    {
        ShowFadePanel();
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
}
