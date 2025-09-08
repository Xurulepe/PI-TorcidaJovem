using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Habilidade que pode ser usada pelo jogador.
/// </summary>
[System.Serializable]
public class Ability : MonoBehaviour
{
    [Tooltip("A imagem que representa o tempo de recarga da habilidade.")]
    public Image timerImage;

    [Tooltip("O tempo de recarga da habilidade em segundos.")]
    public float cooldown;

    [Tooltip("O custo de energia para usar a habilidade.")]
    public int energyCost;

    [Tooltip("Se a habilidade est� pronta para ser usada.")]
    public bool readyToUse = true;

    public void TryUse()
    {
        if (timerImage.fillAmount <= 0f)
        {
            timerImage.fillAmount = 1f;
            readyToUse = false;
            //player.UseAbility(energyCost);
        }
    }

    public void Update()
    {
        if (timerImage.fillAmount > 0f)
        {
            timerImage.fillAmount -= Time.deltaTime / cooldown;
            if (timerImage.fillAmount <= 0f)
            {
                timerImage.fillAmount = 0f;
                readyToUse = true;
            }
        }
    }
}
