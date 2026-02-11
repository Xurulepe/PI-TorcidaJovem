using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthScript : MonoBehaviour
{
    public int _maxHealth;
    public int Respawn;
    public int _currentHealth;
    public Animator _Anim;
    public Animator _anima_Robo;
    public float _deathTime;
    public PlayerControle _playerControle;

    public bool morteExecutada;

    [SerializeField] private GameFlowController _gameFlowController;
    private bool _jogoTerminou;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerControle = GetComponent<PlayerControle>();
        _Anim = GetComponent<Animator>();
        _anima_Robo = _playerControle._anima_Robo;
        _currentHealth = _maxHealth;
        _jogoTerminou = false;
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
            if (morteExecutada == false)
            {
                //_Anim.SetBool("Death", true);
                _anima_Robo.SetTrigger("Death");
                _playerControle._Death = true;
                morteExecutada = true;
                StartCoroutine(DeathTime());
            }
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

            yield return new WaitForSeconds(_deathTime);
            morteExecutada = false;
            //SceneManager.LoadScene(Respawn); 
            
            if (!_jogoTerminou)
            {
                _gameFlowController.ShowLoseHUD();
                _jogoTerminou = true;
            }
        }


    }

    public float GetHealthNormalized()
    {
        return (float)_currentHealth / _maxHealth;
    }
}





