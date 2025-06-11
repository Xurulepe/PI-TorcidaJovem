using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] List<Transform> menuButtons = new List<Transform>();

    private void Start()
    {
        foreach(var button in menuButtons)
        {
            button.localScale = Vector3.zero;
        }

        StartCoroutine(MenuON());
    }

    private IEnumerator MenuON()
    {
        foreach (var button in menuButtons)
        {
            button.DOScale(1.5f, .25f);
            yield return new WaitForSeconds(.25f);
            button.DOScale(1f, .25f);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
