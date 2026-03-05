using UnityEngine;

public class PoolInimigo5 : PoolInimigo
{
    public static PoolInimigo5 _poolimigos5;

    protected override void Awake()
    {
        _poolimigos5 = this;
    }


    protected override void InimigoON(GameObject bullet)
    {

        if (!_gfc.isPaused && (_gfc.enemyCount >= 2 || _gfc.enemyCount == 0))
        {
            bullet = PoolInimigo5._poolimigos5.GetPooledObject();
            base.InimigoON(bullet);
        }
    }
}
