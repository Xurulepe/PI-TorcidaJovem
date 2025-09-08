using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerTest : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private int _maxHealth = 10;
    [SerializeField] private int _currentHealth;
    [SerializeField] private int _maxEnergy = 10;
    [SerializeField] private int _currentEnergy;

    [Header("Habilidades")]
    [SerializeField] private Ability _ability1;
    [SerializeField] private Ability _ability2;

    [Header("Unity Events")]
    public UnityEvent OnDamageTaken;
    public UnityEvent OnAbilityUsed;
    public UnityEvent OnHealthRecupered;
    public UnityEvent OnEnergyRecupered;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _currentEnergy = _maxEnergy;
    }

    public void UseAbility1(InputAction.CallbackContext context)
    {
        if (context.performed && _ability1.readyToUse && _currentEnergy >= _ability1.energyCost)
        {
            Debug.Log("Usou habilidade 1");
            _ability1.TryUse();
            _currentEnergy -= _ability1.energyCost;
            
            OnAbilityUsed?.Invoke();
        }
    }

    public void UseAbility2(InputAction.CallbackContext context)
    {
        if (context.performed && _ability2.readyToUse && _currentEnergy >= _ability2.energyCost)
        {
            Debug.Log("Usou habilidade 2");
            _ability2.TryUse();
            _currentEnergy -= _ability2.energyCost;
            
            OnAbilityUsed?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(2);  
        }
        else if (other.gameObject.CompareTag("HealRegen"))
        {
            RecuperateHealth(3);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("EnergyRegen"))
        {
            RecuperateEnergy(5);
            Destroy(other.gameObject);
        }
    }

    private void TakeDamage(int amount)
    {
        _currentHealth -= amount;

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
            Debug.Log("Player morreu");
        }
        OnDamageTaken?.Invoke();
    }

    private void RecuperateHealth(int amount)
    {
        _currentHealth += amount;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        OnHealthRecupered?.Invoke();
    }

    private void RecuperateEnergy(int amount)
    {
        _currentEnergy += amount;
        if (_currentEnergy > _maxEnergy)
        {
            _currentEnergy = _maxEnergy;
        }
        OnEnergyRecupered?.Invoke();
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetCurrentEnergy()
    {
        return _currentEnergy;
    }
}
