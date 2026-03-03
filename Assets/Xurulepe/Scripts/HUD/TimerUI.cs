using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerTextUI;

    private GameFlowController gameFlowController;

    private float timer;
    int minutes;
    int seconds;
    private string niceTime;

    private void Awake()
    {
        gameFlowController = GetComponent<GameFlowController>();
        timer = 0f;
    }

    private void Update()
    {
        if (!gameFlowController.isPaused)
        {
            timer += Time.deltaTime;
        }
    }

    private void OnGUI()
    {
        minutes = Mathf.FloorToInt(timer / 60F);
        seconds = Mathf.FloorToInt(timer - minutes * 60);

        niceTime = string.Format("{00:00}:{1:00}", minutes, seconds);

        timerTextUI.text = niceTime;
    }

    public string GetTimer()
    {
        niceTime = string.Format("{00:00}:{1:00}", minutes, seconds);

        return niceTime;
    }
}
