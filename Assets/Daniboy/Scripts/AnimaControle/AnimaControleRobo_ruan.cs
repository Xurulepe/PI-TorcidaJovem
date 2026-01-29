using UnityEngine;
using UnityEngine.InputSystem;

public class AnimaControleRobo_ruan : MonoBehaviour
{
   public PlayerControle playerControle;

    public int quantAtk;

   public void Shoot()
    {
        playerControle.Shoot();
        quantAtk = 0;
        playerControle._finalAction = false;

    }

    public void finalAnimaEspada1()
    {
        if (quantAtk ==1)
        {
            quantAtk = 0;
            playerControle._finalAction = false;
        }
    }

    public void finalAnimaEspada2()
    {
        if (quantAtk >= 1)
        {
            quantAtk = 0;
            playerControle._finalAction = false;

        }
    }
}
