using UnityEngine;

public class PoolInimigo : ObjectPooling
{
    protected float _TimeReal;
    [SerializeField] protected float _TimeStart = 3;
    [SerializeField] protected bool timerIsRunning = false;
    protected GameObject _tempInimigo;
    protected Transform _SPAWN;

    protected override void Start()
    {
        base.Start();
        _TimeReal = _TimeStart;
        timerIsRunning = true;
        _SPAWN = GetComponent<Transform>();
    }
    protected virtual void Update()
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
    protected virtual void InimigoON(GameObject bullet)
    {
        //bullet = PoolInimigo1._poolimigos1.GetPooledObject();
        if (bullet != null)
        {
            InimigoDef inimigoDef = bullet.GetComponent<InimigoDef>();
            inimigoDef.Vida();

            bullet.transform.SetParent(_SPAWN);
            bullet.transform.localPosition = Vector3.zero;
            bullet.SetActive(true);
        }
        
    }
}
