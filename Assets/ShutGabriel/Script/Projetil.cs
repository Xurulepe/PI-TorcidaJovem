using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Projetil : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public float lifeTime = 1f;
    int Hurt = 15;
    private Vector3 direction;
    [SerializeField] Transform _alvo;
    [SerializeField] protected ParticleSystem[] _part;
    [SerializeField] MeshRenderer[] _renderer;
    public float tempoExplosao = 1f;


    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        //Destroy(gameObject,lifeTime);
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
            for (int i = 0; i < _part.Length; i++)
            {
                _part[i].Play();
            }
            for (int i = 0; i < _renderer.Length; i++)
            {
                _renderer[i].enabled = false;
            }
           
            
        }
    }
    void Dano()
    {
        
         PlayerHealthScript PlayerHealth = _alvo.gameObject.GetComponent<PlayerHealthScript>();
         PlayerHealth.DamagePlayer(Hurt, direction);
        
    }
}
