using UnityEngine;

public class Tiro : MonoBehaviour
{
    public float lifeTime = 5f;
    void start()
    {
        Destroy(gameObject, 7f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerDano>()?.LevarDano();
            Debug.Log("-1 HP");
            Destroy(gameObject);
        }
    }
}
