using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private PlayerHealthScript _playerHealth;

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
        if (_playerHealth != null)
        {
            _healthBar.fillAmount = _playerHealth.GetHealthNormalized();
        }
    }
}
