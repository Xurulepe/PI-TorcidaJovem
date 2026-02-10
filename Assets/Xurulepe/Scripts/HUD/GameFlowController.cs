using UnityEngine;

public class GameFlowController : MonoBehaviour
{
    [Header("HUDs de vitória e derrota")]
    [SerializeField] private GameObject winHUD;
    [SerializeField] private GameObject loseHUD;

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
}
