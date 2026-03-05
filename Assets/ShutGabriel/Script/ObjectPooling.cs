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
    [SerializeField] public int amountToPool;
    [SerializeField] protected int Vida = 100;


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
    [SerializeField] protected ParticleSystem[] _part;
    [SerializeField] Collider[] _cl;
    protected int dano = 10;
    bool _startV;
    public GameFlowController _gfc;
    protected virtual void Awake()
    {
        // SharedInstance = this;
    }

    protected virtual void Start()
    {
        _gfc = GameObject.FindWithTag("GameController").GetComponent<GameFlowController>();
        
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

    
}