using UnityEngine;
using UnityEngine.UI;

public class HealthBarSliderUI : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;
    [SerializeField] private PlayerHealthScript _playerHealth;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
        _playerHealth = GameObject.FindFirstObjectByType<PlayerHealthScript>();
    }

    private void Start()
    {
        // inscrever no evento de mudança de vida do jogador

        UpdateHealthBar();
    }

    // provavelmente substituir por evento do player
    private void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        // atualizar a barra de vida com base na vida atual do jogador
        _healthBar.value = _playerHealth.GetHealthNormalized();
    }
}
