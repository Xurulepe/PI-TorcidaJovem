using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    //public static ObjectPooling SharedInstance;
    [SerializeField] protected List<GameObject> pooledObjects;
    [SerializeField] protected GameObject objectToPool;
    [SerializeField] protected int amountToPool;
    

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
