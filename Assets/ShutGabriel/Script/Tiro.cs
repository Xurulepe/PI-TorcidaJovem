using UnityEngine;

public class Tiro : InimigoDefault
{
    protected float lifeTime = 5f;
    void start()
    {
        Destroy(gameObject, 7f);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerDano>()?.LevarDano();
            Debug.Log("-1 HP");
            Destroy(gameObject);
        }
    }
}
