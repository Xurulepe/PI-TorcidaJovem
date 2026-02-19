using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameFlowController gameFlowController;

    private void OnTriggerEnter(Collider other)
    {
        bool enemyLayer = other.gameObject.layer == 6 || other.gameObject.layer == 10;

        if (gameFlowController.CanShowTutorial() && enemyLayer)
        {
            gameFlowController.ShowTutorial();
        }
    }
}
