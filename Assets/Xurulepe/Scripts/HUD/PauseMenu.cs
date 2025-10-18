using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuPanel;


    // teste de música
    public AudioClip music;

    private void Start()
    {
        if (music != null)
        {
            AudioManager.Instance.PlayMusic(music);
        }
    }

    public void Pause()
    {
        Debug.Log("Pausando o jogo");
        _pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;  // Pausa o jogo
    }

    public void Resume()
    {
        Debug.Log("Retomando o jogo");
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;  // Retoma o jogo
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;  
        SceneManager.LoadScene(0);
    }
}
