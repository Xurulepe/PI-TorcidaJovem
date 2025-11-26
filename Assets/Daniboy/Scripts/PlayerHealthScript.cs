using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerHealthScript : MonoBehaviour
{
    public int _maxHealth;
    public int Respawn;
    public int _currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
        Test();
        
    }

    public void Death() 
    { 
    
    if (_currentHealth <= 0) 
    {

       SceneManager.LoadScene(Respawn);
        
        
        
    }
    
    }
    public void Test() 
    { 
    
    if (Input.GetKeyUp(KeyCode.Escape)) 
        {

            _currentHealth = 0;
        
        
        
        }
    
    
    }


    public void DamagePlayer(int Hurt, Vector3 direction) 
    {

        _currentHealth -= Hurt;
    
    
    }

    public float GetHealthNormalized()
    {
        return (float)_currentHealth / _maxHealth;
    }




}
