using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowController : MonoBehaviour
{
    [SerializeField] private GameObject backgroundPanel;

    [Header("HUDs")]
    [SerializeField] private GameObject winHUD;
    [SerializeField] private GameObject loseHUD;
    [SerializeField] private GameObject tutorialHUD;

    [Header("Menus a serem animados")]
    [SerializeField] private Menu winMenu;
    [SerializeField] private Menu loseMenu;
    [SerializeField] private Menu tutorialMenu;

    [Header("Tutorial controller")]
    [SerializeField] private List<GameObject> tutorialList;
    [SerializeField] private int tutorialIndex;
    [SerializeField] private bool tutorialActive;

    [Header("Controlados pelo tutorial")]
    [SerializeField] private GameObject enemyMelee;
    [SerializeField] private GameObject enemyShooter;
    [SerializeField] List<GameObject> spawners;

    // componentes
    private MenuAnimation menuAnimation;

    private void Awake()
    {
        menuAnimation = GetComponent<MenuAnimation>();

        winHUD.SetActive(false);
        loseHUD.SetActive(false);
        tutorialHUD.SetActive(false);
        backgroundPanel.SetActive(false);

        tutorialIndex = 0;

        ShowTutorial();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L) && tutorialIndex <= tutorialList.Count - 1 && !tutorialActive)
        //{
        //    ShowTutorial();
        //}
    }

    #region HUD DE FINAL DE JOGO
    public void ShowWinHUD()
    {
        winHUD.SetActive(true);

        backgroundPanel.SetActive(true);
        menuAnimation.AnimateSingleElement(winHUD, Vector3.zero, 1f, 0.25f);
        menuAnimation.AnimateMenu(winMenu, MenuAnimation.AnimationMode.OneAtATime);
    }

    public void ShowLoseHUD()
    {
        loseHUD.SetActive(true);

        backgroundPanel.SetActive(true);
        menuAnimation.AnimateSingleElement(loseHUD, Vector3.zero, 1f, 0.25f);
        menuAnimation.AnimateMenu(loseMenu, MenuAnimation.AnimationMode.OneAtATime);
    }
    #endregion

    #region HUD BUTTONS FUNCTIONS
    public void BackToMenu()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(0);
    }

    public void RetryLevel()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    #endregion


    public void PauseGame()
    {

    }

    #region TUTORIAL CONTROLLER
    public void ContinueTutorial()
    {
        tutorialActive = false;
        backgroundPanel.SetActive(false);
        menuAnimation.AnimateSingleElement(tutorialHUD, Vector3.one, 0f, 0.2f, false);

        DeactiveTutorialText();
        //ControlTutorialObjects();
    }

    public void ShowTutorial()
    {
        // Pausar inimigos e player

        tutorialActive = true;
        tutorialHUD.SetActive(true);
        backgroundPanel.SetActive(true);

        tutorialList[tutorialIndex].SetActive(true);

        menuAnimation.AnimateSingleElement(tutorialHUD, Vector3.zero, 1f, 0.5f);
        menuAnimation.AnimateMenu(tutorialMenu, MenuAnimation.AnimationMode.OneAtATime);

        tutorialIndex++;
    }

    public bool CanShowTutorial()
    {
        return (tutorialIndex <= tutorialList.Count - 1) && !tutorialActive;
    }

    private void DeactiveTutorialText()
    {
        foreach (var tutorial in tutorialList)
        {
            tutorial.SetActive(false);
        }
    }

    private void ControlTutorialObjects()
    {
        switch (tutorialIndex)
        {
            case 4:

                break;
        }
    }
    #endregion
}
