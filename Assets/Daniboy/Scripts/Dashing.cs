using UnityEngine;
using UnityEngine.InputSystem;
public class Dashing : MonoBehaviour
{
    [Header("Variaveis")]
    //public Transform _orientation;
    private Rigidbody _rb;
    private PlayerControle _playerControle;

    [Header("Dash")]
    public float _dashSpeed;
    public float _dashForce;
    public float _dashupForce;
    public float _dashduration;


    [Header("Cooldown")]
    public float _cooldowndash;
    private float _cooldowtime;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerControle = GetComponent<PlayerControle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Dash() 
    { 
    Vector3 forcetoapply = _playerControle.moveDirection * _dashForce;

        _rb.AddForce(forcetoapply, ForceMode.Impulse);

        Invoke(nameof(Dashreset), _dashduration);

    }

    public void DashInput(InputAction.CallbackContext value) 
    { 
    
    _playerControle.moveDirection = value.ReadValue<Vector3>();
    
    }



    private void Dashreset() 
    { 

    }


}
