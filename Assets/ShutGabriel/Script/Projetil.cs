using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifeTime = 4f;

    void Start()
    {
        Destroy(gameObject,lifeTime);
        
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Bala Acertou");
            Destroy(gameObject);
        }
    }
}
