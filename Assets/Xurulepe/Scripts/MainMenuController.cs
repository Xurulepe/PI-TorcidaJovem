using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] List<Transform> menuButtons = new List<Transform>();
    [SerializeField] Image fadePanel;
    [SerializeField] List<Transform> menuPanels = new List<Transform>();

    private void Start()
    {
        fadePanel.gameObject.SetActive(true);

        foreach(var button in menuButtons)
        {
            //button.localScale = Vector3.zero;
            button.localScale = new Vector3(button.localScale.x, 0f, button.localScale.z);
        }

        FadeToLight();
        StartCoroutine(MenuON());
    }

    private IEnumerator MenuON()
    {
        yield return new WaitForSeconds(1.25f);

        foreach (var button in menuButtons)
        {
            //button.DOScale(1.5f, .25f);
            //yield return new WaitForSeconds(.25f);
            //button.DOScale(1f, .25f);
            button.DOScaleY(1f, .25f);
            yield return new WaitForSeconds(.25f);
        }
    }

    private void FadeToLight()
    {
        //fadePanel.color = new Color(0f, 0f, 0f, 255f);
        fadePanel.DOFade(0f, 2.5f).OnComplete(HideFadePanel);
    }

    private void FadeToDark()
    {
        //fadePanel.color = new Color(0f, 0f, 0f, 0f);

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

    public void LoadScene(string sceneName)
    {
        //SceneManager.LoadScene(sceneName);
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
