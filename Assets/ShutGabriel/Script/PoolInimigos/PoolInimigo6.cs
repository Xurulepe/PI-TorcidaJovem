using UnityEngine;

public class PoolInimigo6 : PoolInimigo
{
    public static PoolInimigo6 _poolimigos6;
    protected override void Awake()
    {
        _poolimigos6 = this;
    }
    protected override void InimigoON(GameObject bullet)
    {
        if (!_gfc.isPaused && (_gfc.enemyCount >= 2 || _gfc.enemyCount == 0))
        {
            bullet = PoolInimigo6._poolimigos6.GetPooledObject();
            base.InimigoON(bullet);
        }
    }
}
