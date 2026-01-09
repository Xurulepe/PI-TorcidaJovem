using UnityEditor.U2D;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //public float lifeTime = 3f; // Tempo antes de ser destruído
    public GameObject bulletPool;
    public Transform shootPoint;
    public Rigidbody _rb;
    public float speedProjectile = 15f;
  
    [SerializeField] public Transform _trailpoint; 
    private void Awake()
    {
      
        shootPoint = GameObject.FindWithTag("ShootPoint").transform;
        
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        bulletPool.SetActive(false);
    }

    void DeactivateObj() 
    {
        bulletPool.SetActive(false);
    }

    private void OnEnable()
    {
        _rb.linearVelocity = shootPoint.forward * speedProjectile * Time.deltaTime;
        Invoke("DeactivateObj", 3); 
    }
}
