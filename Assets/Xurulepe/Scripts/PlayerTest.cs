using UnityEngine;
using UnityEngine.UI;

public class PlayerTest : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _energySlider;

    public void TakeDamage()
    {
        _healthSlider.value -= 1;
    }

    public void UseAbility()
    {
        _energySlider.value -= 1;
    }

    public void Heal()
    {
        _healthSlider.value += 1;
    }

    public void RechargeEnergy()
    {
        _energySlider.value += 1;
    }

}
