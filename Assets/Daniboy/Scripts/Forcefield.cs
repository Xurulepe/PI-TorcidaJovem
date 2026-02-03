using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Forcefield : MonoBehaviour
{

    public static Forcefield instance;
    [SerializeField] GameObject fieldPrefab;
    private List<GameObject> objects = new List<GameObject>();
    private int amountToPool = 10;
   
 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(fieldPrefab);
            obj.SetActive(false);
            objects.Add(obj);
        }
    }


    public GameObject GetPooledObjects()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].activeInHierarchy)
            {
                return objects[i];
            }
        }
        return null;
    }


}


