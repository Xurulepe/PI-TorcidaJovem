using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Collections;

public class ObjectPooling : MonoBehaviour
{
    //public static ObjectPooling SharedInstance;
    [SerializeField] protected List<GameObject> pooledObjects;
    [SerializeField] protected GameObject objectToPool;
    [SerializeField] protected int amountToPool;
    [SerializeField] protected float Vida;



    [Header("ConfigSpawnerCaps")]
    [SerializeField] protected float radius = 0.5f;
    [SerializeField] protected float height = 2f;
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected Color gizmoColor = Color.cyan;

    [SerializeField] protected bool _isHitado;
    [SerializeField] protected bool _checkHitado;
    [SerializeField] protected bool _checkMortado;
    [SerializeField] protected Vector3 ScaleStart;

    [SerializeField] MeshRenderer[] _renderer;
    [SerializeField] ParticleSystem[] _part;
    [SerializeField] Collider[] _cl;
    protected int dano;
    bool _startV;
    protected virtual void Awake()
    {
        // SharedInstance = this;
    }

    protected virtual void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
        if (!_startV)
        {
            ScaleStart = transform.localScale;
            _startV = true;
        }
    }

    public virtual GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;

    }


    //HITSPAWNER
    protected virtual void LevarDano(int dano)
    {
        Vida -= dano;
        if (Vida <= 0)
        {
            Morrer();
        }
        else
        {
            RecuperarDedano();
        }
    }

    //Morte do inimigo
    protected virtual void Morrer()
    {
        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].enabled = false;
        }
        gameObject.SetActive(false);



    }

    protected virtual void OnDrawGizmos()
    {
        // Pega pontos da cápsula
        Vector3 point1, point2;
        GetCapsulePoints(out point1, out point2);

        // Cor muda se estiver colidindo
        Gizmos.color = _isHitado ? Color.red : gizmoColor;
        // Desenha esferas nas extremidades
        Gizmos.DrawWireSphere(point1, radius);
        Gizmos.DrawWireSphere(point2, radius);

        // Conectar extremidades (visual)
        Gizmos.DrawLine(point1 + transform.right * radius, point2 + transform.right * radius);


        Gizmos.DrawLine(point1 - transform.right * radius, point2 - transform.right * radius);


        Gizmos.DrawLine(point1 + transform.forward * radius, point2 + transform.forward * radius);


        Gizmos.DrawLine(point1 - transform.forward * radius, point2 - transform.forward * radius);
    }

    protected virtual void GetCapsulePoints(out Vector3 point1, out Vector3 point2)
    {
        Vector3 center = transform.position;

        float halfHeight = Mathf.Max(height * 0.5f - radius, 0f);

        Vector3 up = transform.up * halfHeight;

        point1 = center + up;    // Topo
        point2 = center - up;    // Base
    }
    protected virtual IEnumerator HitTime()
    {
        _checkMortado = true;
        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].transform.DOScale(2, .25f);
        }

        for (int i = 0; i < _cl.Length; i++)
        {
            _cl[i].enabled = false;
        }


        yield return new WaitForSeconds(0.25f);

        for (int i = 0; i < _part.Length; i++)
        {
            _part[i].Play();
        }

        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].enabled = false;
        }


        yield return new WaitForSeconds(0.25f);

        yield return new WaitForSeconds(0.25f);

        _checkHitado = false;
        LevarDano(dano);

    }
    protected virtual void RecuperarDedano()
    {


        
        _checkMortado = false;
        _isHitado = false;
        if (_startV)
        {
            transform.localScale = ScaleStart;
        }
        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].enabled = false;
            
        }

    }
}
