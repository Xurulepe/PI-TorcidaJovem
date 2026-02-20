using UnityEngine;

public class PoolInimigo2 : PoolInimigo
{
    public static PoolInimigo2 _poolimigos2;

    protected override void Awake()
    {
        _poolimigos2 = this;
    }


    protected override void InimigoON(GameObject bullet)
    {
        //teste
        if (!_gfc.isPaused && _gfc.enemyCount >= 1)
        {
            //_gfc.enemyCount = 2;
            bullet = PoolInimigo2._poolimigos2.GetPooledObject();
            base.InimigoON(bullet);
        }
    }
}
