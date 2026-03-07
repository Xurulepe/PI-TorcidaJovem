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
       
        base.Start();
        _quantiaRestante = amountToPool;
        _TimeReal = _TimeStart;
        timerIsRunning = true;
        _SPAWN = GetComponent<Transform>();
        
    }
    
    protected virtual void Update()
    {
        if (_gfc.isPaused)
        {
            timerIsRunning = false;

        }
        else
        {
            timerIsRunning = true;
            if (timerIsRunning) 
            {
                if (_TimeReal > 0)
                {
                    _TimeReal -= Time.deltaTime;
                }
                else
                {
                    if (_quantiaRestante <= 0)
                    {
                        gameObject.SetActive(false);
                    }
                    Debug.Log("Tempo esta correndo");

                    _tempInimigo = GetPooledObject();
                    if (_tempInimigo == null)
                    {
                        Debug.Log("Ta nulo");
                    }
                    else
                    {
                        InimigoON(_tempInimigo);
                    }

                    _TimeReal = _TimeStart;
                    _quantiaRestante = Mathf.Max(0, _quantiaRestante - 1);
                    if (_quantiaRestante <= 0)
                    {
                        SpawnerOff();
                    }
                }

            }
           
        }
    }
    void SpawnerOff()
    {
        timerIsRunning = false;
        gameObject.SetActive(false);
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
