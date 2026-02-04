using UnityEngine;
using System.Collections.Generic;
public class Espadao : MonoBehaviour
{        
    public GameObject ParticulaHit;
    public int poolSize = 20;

    public List<GameObject> Particulas = new List<GameObject>();

    private void Awake()
    {
        for(int i = 0; i < poolSize; i++)
        {
            GameObject b = Instantiate(ParticulaHit);
            b.SetActive(false);
            Particulas.Add(b);
        }
    }

    public GameObject GetParticula(Vector3 posiContato)
    {
        foreach (GameObject b in Particulas)
        {
            if (!b.activeInHierarchy)
            {
                b.transform.position = posiContato;
                return b;
            }
        }

        return null; // pool acabou
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyMelee"))
        {            

            GameObject p = GetParticula(other.transform.position);

            if (p != null)
            {
                p.SetActive(true);
            }
        }
    }

}
