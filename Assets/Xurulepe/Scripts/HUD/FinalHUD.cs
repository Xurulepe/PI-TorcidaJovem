using TMPro;
using UnityEngine;

public class FinalHUD : MonoBehaviour
{
    [Header("Objetivos")]
    [SerializeField] private TextMeshProUGUI HUDObjectiveText;
    [SerializeField] private TextMeshProUGUI missionObjectiveText;

    [Header("Temporizador")]
    [SerializeField] private TimerUI timer;
    [SerializeField] private TextMeshProUGUI HUDTimerText;

    private bool isHUDEnabled = false;
    private bool isHUDUpdated = false;

    private void OnEnable()
    {
        isHUDEnabled = true;
    }

    private void Update()
    {
        if (isHUDEnabled && !isHUDUpdated)
        {
            HUDObjectiveText.text = HUDObjectiveText.text.Replace("{0}", missionObjectiveText.text);
            HUDTimerText.text = timer.GetTimer();
            isHUDUpdated = true;
        }
    }
}
