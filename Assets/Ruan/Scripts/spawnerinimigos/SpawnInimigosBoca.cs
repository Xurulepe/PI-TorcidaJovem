using System.Collections.Generic;
using UnityEngine;

public class SpawnInimigosBoca : MonoBehaviour
{
    [SerializeField] public List<GameObject> pooledObjects;
    [SerializeField] protected GameObject objectToPool;
    [SerializeField] protected int amountToPool;

    public float tempoAtivar;
    public float quantAtivar;

    void Start()
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

    // Update is called once per frame
    void Update()
    {
        ativarInimigos();
    }

    public void ativarInimigos()
    {
        if (tempoAtivar < 4)
        {
            tempoAtivar += Time.deltaTime;
        }

        if (tempoAtivar > 3)
        {
            for (int i = 0; i < quantAtivar; i++)
            {
                GameObject go = GetPooledObject();
                go.transform.position = this.transform.position;
                go.SetActive(true);
            }
        }
    }

    public GameObject GetPooledObject()
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
