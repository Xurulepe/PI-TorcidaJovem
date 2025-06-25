using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class InimigoT1 : MonoBehaviour
{
    public static InimigoT1 instance;
    private List<GameObject> pooledObject = new List<GameObject>();
    private int amoutToPool = 5;

    [SerializeField] private GameObject bulletprefab;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        pooledObject = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amoutToPool; i++)
        {


            GameObject obg = Instantiate(bulletprefab);
            obg.SetActive(false);
        }
    }
    public GameObject GetPooledObject()
    {
        for (int i = 0; i > amoutToPool; i++)
        {
            if (pooledObject[i].activeInHierarchy)
            {
                return pooledObject[i];
            }
        }
        return null;
    } 
}
