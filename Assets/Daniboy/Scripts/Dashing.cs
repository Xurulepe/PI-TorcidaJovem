using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Dashing : MonoBehaviour
{
    public PlayerControle _moveScript;
    public bool _IsDashing;
    public float _dashSpeed;
    public float _dashTime;
    public PlayerControle _playerScript;
    [SerializeField] private ParticleSystem _damagePlayer;
    private ParticleSystem instanceDamage;

    // Start is called before the first frame update
    void Start()
    {
        _playerScript = GetComponent<PlayerControle>();
        _moveScript = GetComponent<PlayerControle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerScript._lockMove == false) 
        {
            
            DashMove();

        }
         
    }

    public void DashMove()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {  
            StartCoroutine(Impulse());
        }
    }

     IEnumerator Impulse()
    {
        float startTime = Time.time;

        while (Time.time < startTime + _dashTime)
        {
            _moveScript._anima_Robo.SetTrigger("Dash");
            _moveScript.Controller.Move(_moveScript.moveInput * _dashSpeed * Time.deltaTime);
            SpawnDamageParticle();
            yield return null;

        }
    }

    private void SpawnDamageParticle() 
    { 
        instanceDamage = Instantiate(_damagePlayer, transform.position, Quaternion.identity);  
    }


}
