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
    [SerializeField] private GameObject waveHUD;

    [Header("Menus a serem animados")]
    [SerializeField] private Menu winMenu;
    [SerializeField] private Menu loseMenu;
    [SerializeField] private Menu tutorialMenu;

    [Header("Tutorial controller")]
    [SerializeField] private List<GameObject> tutorialList;
    [SerializeField] private int tutorialIndex;
    [SerializeField] private bool tutorialActive;
    [SerializeField] private bool waveStarted = false;

    [Header("Controlados pelo tutorial")]
    [SerializeField] private InimigoMelee enemyMelee;
    [SerializeField] private InimigoShooter enemyShooter;
    [SerializeField] private List<GameObject> tutorialSpawners;
    [SerializeField] private List<GameObject> wave1Spawners;
    [SerializeField] private List<GameObject> wave2Spawners;
    [SerializeField] private List<GameObject> wave3Spawners;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private PlayerControle playerControl;

    [Space]
    public List<GameObject> enemiesList = new List<GameObject>();

    public bool isPaused;
    public int enemyCount;
    public bool tutorialFinished;

    // componentes
    private MenuAnimation menuAnimation;
    private TimerUI timerUI;

    private void Awake()
    {
        menuAnimation = GetComponent<MenuAnimation>();
        timerUI = GetComponent<TimerUI>();

        winHUD.SetActive(false);
        loseHUD.SetActive(false);
        tutorialHUD.SetActive(false);
        backgroundPanel.SetActive(false);

        tutorialIndex = 0;

        ShowTutorial();
    }

    private void Update()
    {
        if (!waveStarted)
        {
            ControlEnemies();
        }

    }

    #region HUD DE FINAL DE JOGO
    public void ShowWinHUD()
    {
        PauseGame();
        winHUD.SetActive(true);

        backgroundPanel.SetActive(true);
        menuAnimation.AnimateSingleElement(winHUD, Vector3.zero, 1f, 0.25f);
        menuAnimation.AnimateMenu(winMenu, MenuAnimation.AnimationMode.OneAtATime);
    }

    public void ShowLoseHUD()
    {
        PauseGame();
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
        isPaused = true;
        playerControl.LockFunction();
    }

    public void UnPause()
    {
        isPaused = false;
        playerControl.Unlock();
    }

    public void AddEnemy(GameObject enemy)
    {
        enemiesList.Add(enemy);
    }

    #region TUTORIAL CONTROLLER
    public void ContinueTutorial()
    {
        pauseButton.SetActive(true);
        tutorialActive = false;
        backgroundPanel.SetActive(false);
        menuAnimation.AnimateSingleElement(tutorialHUD, Vector3.one, 0f, 0.2f, false);

        DeactiveTutorialText();

        UnPause();
        ControlTutorialObjects();

        if (tutorialIndex == 1)
        {
            ShowTutorial();
        }
    }

    public void ShowTutorial()
    {
        // Pausar inimigos e player
        PauseGame();

        tutorialActive = true;
        tutorialHUD.SetActive(true);
        backgroundPanel.SetActive(true);
        pauseButton.SetActive(false);

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
            case 1:  // terminou o tutorial de mover

                break;

            case 2:  // terminou o tutorial de dash

                break;

            case 3:  // terminou o tutorial de ataque mellee

                break;

            case 4:  // terminou o tutorial de ataque disparo
                ControlSpawners(tutorialSpawners, false);
                tutorialFinished = true;

                break;
        }
    }

    private void ControlEnemies()
    {
        if (enemyMelee != null && enemyMelee.morreu && CanShowTutorial())
        {
            tutorialSpawners[1].SetActive(true);  //
        }
        else if (enemyShooter != null && enemyShooter.morreu)
        {
            //ShowTutorial();
            ControlSpawners(wave1Spawners, true);
            waveHUD.SetActive(true);
            waveStarted = true;
        }
    }

    public void GetEnemyMelee()
    {
        enemyMelee = enemiesList[0].GetComponent<InimigoMelee>();
    }

    public void GetEnemyShooter()
    {
        enemyShooter = enemiesList[1].GetComponent<InimigoShooter>();
    }
    #endregion

    public void StartNewWave()
    {
        waveHUD.SetActive(true);
    }
}
