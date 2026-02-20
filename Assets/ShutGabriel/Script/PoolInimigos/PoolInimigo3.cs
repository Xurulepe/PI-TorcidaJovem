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
        if (!_gfc.isPaused)
        {
            bullet = PoolInimigo3._poolimigos3.GetPooledObject();
            base.InimigoON(bullet);
        }
    }
}