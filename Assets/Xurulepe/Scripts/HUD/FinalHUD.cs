using TMPro;
using UnityEngine;

public class FinalHUD : MonoBehaviour
{
    [Header("Objetivos")]
    [SerializeField] private TextMeshProUGUI HUDObjectiveText;
    [SerializeField] private TextMeshProUGUI missionObjectiveText;

    private void OnEnable()
    {
        HUDObjectiveText.text = HUDObjectiveText.text.Replace("{0}", missionObjectiveText.text);
    }
}
