using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private GameFlowController gameFlowController;
    private bool checkEnemyMelee;
    private bool checkEnemyShooter;

    private void OnTriggerEnter(Collider other)
    {
        bool enemyMelee = other.gameObject.layer == 6;
        bool enemyShooter = other.gameObject.layer == 10;

        if (gameFlowController.CanShowTutorial() && enemyMelee && gameFlowController.enemyCount == 0 && !checkEnemyMelee)
        {
            checkEnemyMelee = true;
            gameFlowController.ShowTutorial();
        }
        else if (enemyShooter && gameFlowController.enemyCount >= 1 && !checkEnemyShooter)
        {
            checkEnemyShooter = true;
            gameFlowController.ShowTutorial();
        }
    }
}
