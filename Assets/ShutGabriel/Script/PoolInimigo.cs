using UnityEngine;
using UnityEngine.AI;

public class PoolInimigo : ObjectPooling
{
    protected float _TimeReal;
    [SerializeField] protected float _TimeStart = 2;
    [SerializeField] protected bool timerIsRunning = false;
    protected GameObject _tempInimigo;
    protected Transform _SPAWN;
    public int _quantiaRestante;
    public bool bloqueio;

    public bool posiAleatoria;
    public Vector3 posiMin;
    public Vector3 posiMax;

    
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
                        if (posiAleatoria == true)
                        {
                            transform.position = new Vector3(Random.Range(posiMin.x, posiMax.x), transform.position.y, Random.Range(posiMin.z, posiMax.z));
                        }
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
            inimigoDef.Vida(bullet);

            //bullet.transform.SetParent(_SPAWN);
            bullet.transform.localPosition = transform.localPosition;
           
            bullet.SetActive(true);

            WaveManager.Instance.AddNewEnemy(bullet);

            for (int i = 0; i < _part.Length; i++)
            {
                _part[i].Play();
            }
        }
        
    }
}
