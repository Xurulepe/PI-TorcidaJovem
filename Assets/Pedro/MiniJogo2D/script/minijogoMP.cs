using UnityEngine;
using UnityEngine.InputSystem;

public class minijogoMP : MonoBehaviour
{
    [Header("física")]
    [SerializeField] Rigidbody2D _rb;

    [Header("animations")]
    [SerializeField] Animator _anim;

    [Header("movimento")]
    [SerializeField] Vector2 _move;
    [SerializeField] float _speed;

    [Header("force pulo")]
    [SerializeField] float _forceJump;

    [SerializeField] bool _isGrounded;
    [SerializeField] bool _isWalking;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        _rb.linearVelocity = new Vector2(_move.x * _speed, _rb.linearVelocity.y);

        Flip();
        _anim.SetBool("isWalking", _isWalking);
        _anim.SetBool("isGrounded", _isGrounded);
    }


    public void SetMove(InputAction.CallbackContext value)
    {
        _move = value.ReadValue<Vector2>();
        _isWalking = value.performed;
    }

    #region função de pulo
    void Jump()
    {
        _isGrounded = false;
        _rb.linearVelocityY = 0;
        _rb.AddForceY(_forceJump);
        _anim.SetTrigger("jump");
    }
    #endregion


    #region função checagem 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("jumpGround"))
        {
            Jump();
        }
        else if (collision.CompareTag("ground"))
        {
            _isGrounded = true;
        }
    }
    #endregion

    #region flip
    private void Flip()
    {
        if (_move.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (_move.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    #endregion

}
