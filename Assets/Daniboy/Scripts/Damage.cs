using UnityEngine;

public class Damage : MonoBehaviour
{
    public int Hurt = 1;
    public PlayerHealthScript PlayerHealthScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerHealthScript = GetComponent<PlayerHealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HitPlayer") 
        { 
        
        Vector3 damageDirection = other.transform.position - transform.forward;
           damageDirection = damageDirection.normalized;

           FindFirstObjectByType<PlayerHealthScript>().DamagePlayer(Hurt, damageDirection);
        
        
        }
    }
}
