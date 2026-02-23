using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
    //[SerializeField] private GameObject enemyMelee;
    //[SerializeField] private GameObject enemyShooter;
    [SerializeField] private List<GameObject> tutorialSpawners;
    [SerializeField] private List<GameObject> finalSpawners;
    [SerializeField] private GameObject pauseMenu;

    public List<GameObject> enemiesList = new List<GameObject>();

    public bool isPaused;
    public int enemyCount;

    public int enemyMeleeAmount = 1;
    public int enemyShooterAmount = 1;

    public bool canSpawnEnemyMelee = false;
    public bool canSpawnEnemyShooter = false;


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

    // a ser usado quando um tutorial for exibido
    public void PauseGame()
    {
        // pausar inimigos
        //PauseManager.isPaused = true;
        for (int i = 0; i < enemiesList.Count; i++) {

            enemiesList[i].GetComponent<NavMeshAgent>().isStopped = isPaused;
        }

        // pausar player
    }

    public void UnPause()
    {
        //PauseManager.isPaused = false;
    }

    #region TUTORIAL CONTROLLER
    public void ContinueTutorial()
    {
        //pauseMenu.SetActive(true);
        tutorialActive = false;
        backgroundPanel.SetActive(false);
        menuAnimation.AnimateSingleElement(tutorialHUD, Vector3.one, 0f, 0.2f, false);

        DeactiveTutorialText();

        isPaused = false;
        PauseGame();
        ControlTutorialObjects();
    }

    public void ShowTutorial()
    {
        // Pausar inimigos e player
        isPaused = true;
        PauseGame();

        tutorialActive = true;
        tutorialHUD.SetActive(true);
        backgroundPanel.SetActive(true);
        //pauseMenu.SetActive(false);

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

    private void ControlSpawners(List<GameObject> spawnerList, bool active)
    {
        foreach (GameObject spawner in spawnerList)
        {
            spawner.SetActive(active);
        }
    }

    private void ControlTutorialObjects()
    {
        switch (tutorialIndex)
        {
            case 1:
                canSpawnEnemyMelee = true;
                break;
            case 2:
                canSpawnEnemyMelee = false;
                //canSpawnEnemyShooter = true; 
                break;
            case 3:
                ControlSpawners(finalSpawners, true);
                break;
        }
    }
    #endregion
}
