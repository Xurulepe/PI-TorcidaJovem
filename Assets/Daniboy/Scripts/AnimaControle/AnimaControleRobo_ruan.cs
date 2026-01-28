using UnityEngine;

public class AnimaControleRobo_ruan : MonoBehaviour
{
   public PlayerControle playerControle;

    public int quantAtk;

   public void Shoot()
    {
        playerControle.Shoot();
    }

    public void finalShoot()
    {
        playerControle.shootExec = false;
    }

    public void finalAnimaEspada1()
    {
        if (quantAtk ==1)
        {
            quantAtk = 0;
        }
    }

    public void finalAnimaEspada2()
    {
        if (quantAtk >= 1)
        {
            quantAtk = 0;
        }
    }
}
