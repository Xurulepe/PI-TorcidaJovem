using System.Collections;
using TMPro;
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
                 
         
    }

    public void DashMove(InputAction.CallbackContext value)
    {
        if (_playerScript._finalAction == false)
        {
            if (value.started)
            {
                if (_playerScript._lockMove == false)
                {
                    _moveScript._anima_Robo.SetTrigger("Dash");
                    StartCoroutine(Impulse());
                    _playerScript._finalAction = true;
                }
            }
        }                
    }

    IEnumerator Impulse()
    {
        float startTime = Time.time;

        while (Time.time < startTime + _dashTime)
        {
            _moveScript.Controller.Move(_moveScript.moveInput * _dashSpeed * Time.deltaTime);
            _playerScript.anima_robo_ruan.quantAtk = 0;
            SpawnDamageParticle();
            yield return null;

        }
    }

    private void SpawnDamageParticle() 
    { 
        instanceDamage = Instantiate(_damagePlayer, transform.position, Quaternion.identity);  
    }


}
