using UnityEngine;

public class PoolInimigo1 : PoolInimigo
{
    public static PoolInimigo1 _poolimigos1;

    protected override void Awake()
    {
        _poolimigos1 = this;
    }
    
    
    protected override void InimigoON(GameObject bullet)
    {
        if (!_gfc.isPaused)
        {
            bullet = PoolInimigo1._poolimigos1.GetPooledObject();
            base.InimigoON(bullet);
        }
       
    }


}
