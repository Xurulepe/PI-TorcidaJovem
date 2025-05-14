using UnityEngine;

public class Projectile : MonoBehaviour
{
    //public float lifeTime = 3f; // Tempo antes de ser destruído
    public GameObject bulletPool;
    public Transform shootPoint;
    public Rigidbody _rb;
    void Start()
    {
      _rb = GetComponent<Rigidbody>();
        Invoke("DeactivateObj", 3);

    }





    void DeactivateObj() 
    {

        bulletPool.SetActive(false);


    }










}
