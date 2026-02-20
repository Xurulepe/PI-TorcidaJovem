using UnityEngine;

public class PoolInimigo3 : PoolInimigo
{
    public static PoolInimigo3 _poolimigos3;

    protected override void Awake()
    {
        _poolimigos3 = this;
    }


    protected override void InimigoON(GameObject bullet)
    {
        //teste
        if (!_gfc.isPaused && (_gfc.enemyCount >= 2 || _gfc.enemyCount == 0))
        {
            // _gfc.enemyCount = 1;
            bullet = PoolInimigo3._poolimigos3.GetPooledObject();
            base.InimigoON(bullet);
        }
    }
}