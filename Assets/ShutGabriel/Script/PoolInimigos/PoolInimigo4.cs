using UnityEngine;

public class PoolInimigo4 : PoolInimigo
{
    public static PoolInimigo4 _poolimigos4;

    protected override void Awake()
    {
        _poolimigos4 = this;
    }


    protected override void InimigoON(GameObject bullet)
    {
        
        if (!_gfc.isPaused && (_gfc.enemyCount >= 2 || _gfc.enemyCount == 0))
        {
            bullet = PoolInimigo4._poolimigos4.GetPooledObject();
            base.InimigoON(bullet);
        }
    }
}
