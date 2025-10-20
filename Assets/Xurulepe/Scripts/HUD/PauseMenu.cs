using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuPanel;

    // propriedade estática para verificar se o jogo está pausado
    // substituir por não-static em um GameManager depois
    public static bool IsPaused { get; private set; } = false;

    // teste de música
    public AudioClip music;

    private void Start()
    {
        if (music != null)
        {
            AudioManager.Instance.PlayMusic(music);
        }
    }

    #region PAUSE/RESUME/EXIT
    public void Pause()
    {
        Debug.Log("Pausando o jogo");
        IsPaused = true;
        AnimatePauseMenuIn(_pauseMenuPanel);
    }

    public void Resume()
    {
        Debug.Log("Retomando o jogo");
        IsPaused = false;
        AnimatePauseMenuOut(_pauseMenuPanel);
    }

    public void BackToMenu()
    {
        IsPaused = false;
        SceneManager.LoadScene(0);
    }
    #endregion

    #region ANIMATIONS
    public void AnimatePauseMenuIn(GameObject gameObject)
    {
        gameObject.transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        gameObject.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }

    public void AnimatePauseMenuOut(GameObject gameObject)
    {
        gameObject.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }
    #endregion
}
