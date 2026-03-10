using TMPro;
using UnityEngine;

public class DestroyedEnemiesUI : MonoBehaviour
{
    [SerializeField] private GameFlowController gameFlowController;
    [SerializeField] private TextMeshProUGUI destroyedEnemiesUI;

    private void OnGUI()
    {
        destroyedEnemiesUI.text = gameFlowController.enemyCount.ToString();
    }
}
