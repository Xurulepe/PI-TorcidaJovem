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
        bullet = PoolInimigo3._poolimigos3.GetPooledObject();
        base.InimigoON(bullet);
    }
}