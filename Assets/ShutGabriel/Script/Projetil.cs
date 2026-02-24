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
    public float tempoExplosao = 0.1f;
    public bool Tiro = false;
    [Header("Campo de força")]
    bool _field = false;
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
        if (other.CompareTag("HitPlayer"))
        {
            Debug.Log("Bala Acertou");
            Dano();
            for (int i = 0; i < _part.Length; i++)
            {
                _part[i].Play();
            }
            Tiro = true; 
        }
        else if (other.gameObject.CompareTag("Forcefield") && !_field)
        {
            for (int i = 0; i < _renderer.Length; i++)
            {
                _field = true;
                _part[i].Play();
                Invoke(nameof(FieldResp), 1);

            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HitPlayer"))
        {
            for (int i = 0; i < _renderer.Length; i++)
            {
                _renderer[i].enabled = false;
                gameObject.SetActive(false);
            }
        }
        if (other.gameObject.CompareTag("Forcefield") && !_field)
        {
            for (int i = 0; i < _renderer.Length; i++)
            {
                _renderer[i].enabled = false;
                gameObject.SetActive(false);
            }
        }

    }
    void FieldResp()
    {

        _field = false;

    }
    void Dano()
    {
        
         PlayerHealthScript PlayerHealth = _alvo.gameObject.GetComponent<PlayerHealthScript>();
         PlayerHealth.DamagePlayer(Hurt, direction);
        
    }
}
