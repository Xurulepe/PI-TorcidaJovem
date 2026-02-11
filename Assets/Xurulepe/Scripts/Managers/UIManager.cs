using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameFlowController _gameFlowController;

    #region SINGLETON
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        

        
    }

    public void ShowGameOver()
    {
        _gameFlowController.ShowLoseHUD();
    }

    public void ShowGameCompleted()
    {
        _gameFlowController.ShowWinHUD();
    }

    public void TogglePauseMenu()
    {
        
    }
}
