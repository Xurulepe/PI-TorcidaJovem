using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    public int _maxHealth;
    public int _currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int Hurt, Vector3 direction) 
    {

        _currentHealth -= Hurt;
    
    
    }
}
