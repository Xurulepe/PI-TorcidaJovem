using UnityEngine;
using UnityEngine.InputSystem;

public class minijogoMP : MonoBehaviour
{
    [Header("física")]
    [SerializeField] Rigidbody2D _rb;

    [Header("movimento")]
    [SerializeField] Vector2 _move;
    [SerializeField] float _speed;

    [Header("force pulo")]
    [SerializeField] float _forceJump;

    [SerializeField] bool _checkGround;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        _rb.linearVelocity = new Vector2(_move.x * _speed, _rb.linearVelocity.y);
    }


    public void SetMove(InputAction.CallbackContext value)
    {
        _move = value.ReadValue<Vector2>();
    }

    #region função de pulo
    void Jump()
    {
        _rb.linearVelocityY = 0;
        _rb.AddForceY(_forceJump);
    }
    #endregion


    #region função checagem 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("jumpGround"))
        {
            Jump();
        }
    }
    #endregion



}
