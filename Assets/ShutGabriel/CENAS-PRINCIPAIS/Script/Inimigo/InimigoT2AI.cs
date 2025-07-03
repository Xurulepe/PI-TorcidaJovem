using UnityEngine;
using UnityEngine.AI;
public class InimigoT2AI : MonoBehaviour
{
    //GETCOMPONEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEENT PARA CHAMAR PLAYER E RESOLVER NOSSOS PROBLEMAS
    public GameObject _spriteVirus;
    public Transform cameraTrans;
    public Transform shadowPosition;
    public Transform player;
    public float stoppingDistance = 10f;
    
    public GameObject projectilePrefab;
    public Transform ShootPoint;
    
    public float shootInterval = 2f;
    public float projectileSpeed = 10f;

    private NavMeshAgent agent;
    private float shootTimer;

    void Start()
    {
        //agent = GetComponent<>
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;
        shootTimer = shootInterval;
        
    }
    
    // Update is called once per frame
    void Update()
    {

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if(distance > stoppingDistance)
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
            }
            else
            {
                agent.isStopped = true;
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
                shoot();
            }
        }
    }
   
    void shoot()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0f)
        {
            GameObject projectile = Instantiate(projectilePrefab, ShootPoint.position,ShootPoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.linearVelocity = ShootPoint.forward * projectileSpeed;
            }
            shootTimer = shootInterval;
        }
    }
    void LateUpdate()
    {
        _spriteVirus.transform.LookAt(_spriteVirus.transform.localPosition + cameraTrans.position);
        shadowPosition.transform.LookAt(shadowPosition.transform.localPosition + cameraTrans.position);
    }
}
