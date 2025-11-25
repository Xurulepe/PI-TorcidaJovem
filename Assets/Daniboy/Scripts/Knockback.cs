using UnityEngine;

public class Knockback : MonoBehaviour
{
    [Header("Knockback Settings")]
    public float knockbackForce = 100f;
    public float knockbackDuration = 0.3f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 knockbackVelocity;
    private float knockbackTimer;
    bool EnemyHitBox;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Apply gravity
        if (controller.isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f; // Keeps grounded
        else
            playerVelocity.y += gravity * Time.deltaTime;

        // Decrease knockback over time
        if (knockbackTimer > 0)
        {
            controller.Move(knockbackVelocity * Time.deltaTime);
            knockbackTimer -= Time.deltaTime;
        }

      
    }


    public void ApplyKnockback(Vector3 direction, float force = -1)
    {
        if (force < 0) force = knockbackForce;

        knockbackVelocity = direction * force;
        knockbackTimer = knockbackDuration;
    }

 
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHitbox"))
        {
            Debug.Log(" inim "+other.gameObject.name);
            Vector3 knockDir = (transform.position - other.transform.position).normalized;
            ApplyKnockback(knockDir);
        }
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("EnemyHitbox"))
        {
            Vector3 knockDir = (transform.position - hit.point).normalized;
            ApplyKnockback(knockDir);
        }
    }
}