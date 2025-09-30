using UnityEngine;

public class PoolInimigo1 : PoolInimigo
{
    public static PoolInimigo1 _poolimigos1;

    protected override void Awake()
    {
        _poolimigos1 = this;
    }
    
    void Update()
    {
        if (timerIsRunning)
        {
            if (_TimeReal > 0)
            {
                _TimeReal -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Tempo esta correndo");
                InimigoON(_tempInimigo);
                _TimeReal = _TimeStart;
                //timerIsRunning=false;
            }
        }

    }
    void InimigoON(GameObject bullet)
    {
        bullet = PoolInimigo1._poolimigos1.GetPooledObject();
        if (bullet != null)
        {
            //bullet.transform.position = turret.transform.position;
            //bullet.transform.rotation = turret.transform.rotation;
            bullet.SetActive(true);
        }
    }
}
