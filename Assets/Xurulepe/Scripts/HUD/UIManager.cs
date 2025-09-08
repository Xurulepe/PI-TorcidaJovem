using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _energySlider;

    private PlayerTest _player;

    private void Start()
    {
        _player = GMTest.Instance.Player;

        _player.OnAbilityUsed.AddListener(SetEnergySliderValue);
        _player.OnEnergyRecupered.AddListener(SetEnergySliderValue);

        _player.OnDamageTaken.AddListener(SetHealthSliderValue);
        _player.OnHealthRecupered.AddListener(SetHealthSliderValue);
    }

    private void SetHealthSliderValue()
    {
        _healthSlider.DOValue(_player.GetCurrentHealth(), 0.5f);
    }

    private void SetEnergySliderValue()
    {
        _energySlider.DOValue(_player.GetCurrentEnergy(), 0.5f);
    }
}
