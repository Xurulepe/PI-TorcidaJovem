using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowController : MonoBehaviour
{
    [Header("HUDs")]
    [SerializeField] private GameObject winHUD;
    [SerializeField] private GameObject loseHUD;
    [SerializeField] private GameObject tutorialHUD;

    [Header("Menus a serem animados")]
    [SerializeField] private Menu winMenu;
    [SerializeField] private Menu loseMenu;

    // componentes
    private MenuAnimation menuAnimation;

    private void Awake()
    {
        menuAnimation = GetComponent<MenuAnimation>();

        winHUD.SetActive(false);
        loseHUD.SetActive(false);
    }

    public void ShowWinHUD()
    {
        winHUD.SetActive(true);

        menuAnimation.AnimateSingleElement(winHUD);
        menuAnimation.AnimateMenu(winMenu, MenuAnimation.AnimationMode.OneAtATime);
    }

    public void ShowLoseHUD()
    {
        loseHUD.SetActive(true);

        menuAnimation.AnimateSingleElement(loseHUD);
        menuAnimation.AnimateMenu(loseMenu, MenuAnimation.AnimationMode.OneAtATime);
    }

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

    public void ContinueTutorial()
    {
        tutorialHUD.SetActive(false);
    }
}
