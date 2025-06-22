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
            //talvez consiga colocar efeitos aqui? perguntar em sala
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
