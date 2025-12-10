using UnityEngine;
using UnityEngine.Rendering;

public class Projetil : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public float lifeTime = 4f;
    int Hurt = 15;
    private Vector3 direction;
    [SerializeField] Transform _alvo;
  
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Destroy(gameObject,lifeTime);
        _alvo = player.transform;
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
            Dano();
            Destroy(gameObject);

        }
    }
    void Dano()
    {
        PlayerHealthScript PlayerHealth = _alvo.gameObject.GetComponent<PlayerHealthScript>();
        PlayerHealth.DamagePlayer(Hurt, direction);
    }
}
