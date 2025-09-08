using UnityEngine;

public class BaseEnemyTest : MonoBehaviour
{
    [SerializeField] protected int _maxHealth = 5;
    [SerializeField] protected int _currentHealth;

    [SerializeField] protected GameObject _spawnOnDeath;

    protected virtual void Start()
    {
        _currentHealth = _maxHealth;
    }

    protected virtual void TakeDamage()
    {
        _currentHealth--;

        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Instantiate(_spawnOnDeath, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bullet(Clone)")
        {
            TakeDamage();
        }
    }

}
