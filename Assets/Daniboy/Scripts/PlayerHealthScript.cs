using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthScript : MonoBehaviour
{
    public int _maxHealth;
    public int Respawn;
    public int _currentHealth;
    public Animator _Anim;
    public float _deathTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _Anim = GetComponent<Animator>();
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
           


            StartCoroutine(DeathTime());

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


    IEnumerator DeathTime()
    {

        float startTime = Time.time;

        while (Time.time < startTime + _deathTime)
        {
            _Anim.SetTrigger("Death");
          

            yield return new WaitForSeconds(_deathTime);

            SceneManager.LoadScene(Respawn);
        }


    }

        public float GetHealthNormalized() 
    {

        return (float)_currentHealth / _maxHealth;
    }
    
      
    }





