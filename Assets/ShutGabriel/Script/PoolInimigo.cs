using UnityEngine;

public class PoolInimigo : ObjectPooling
{
    protected float _TimeReal;
    [SerializeField] protected float _TimeStart = 2;
    [SerializeField] protected bool timerIsRunning = false;
    protected GameObject _tempInimigo;
    protected Transform _SPAWN;
    public int _quantiaRestante;
    public bool bloqueio;
    protected override void Start()
    {
        _quantiaRestante = amountToPool;
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
                _quantiaRestante--;
                SpawnerOff();
                //timerIsRunning=false;
            }
        }
    }
    void SpawnerOff()
    {
        if (_quantiaRestante <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void InimigoON(GameObject bullet)
    {
        if (bullet != null)
        {
            InimigoDef inimigoDef = bullet.GetComponent<InimigoDef>();
            inimigoDef.Vida();

            //bullet.transform.SetParent(_SPAWN);
            bullet.transform.localPosition = transform.position;
            bullet.SetActive(true);
            for (int i = 0; i < _part.Length; i++)
            {
                _part[i].Play();
            }
        }
        
    }
}
