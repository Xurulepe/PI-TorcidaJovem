using UnityEngine;

public class AnimaControleRobo_ruan : MonoBehaviour
{
   public PlayerControle playerControle;

   public void Shoot()
    {
        playerControle.Shoot();
    }

    public void finalShoot()
    {
        playerControle.shootExec = false;
    }

}
