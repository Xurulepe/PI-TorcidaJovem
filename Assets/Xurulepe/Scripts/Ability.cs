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

    public void TryUse()
    {
        if (timerImage.fillAmount <= 0f)
        {
            timerImage.fillAmount = 1f;
        }
    }

    public void Update()
    {
        if (timerImage.fillAmount > 0f)
        {
            timerImage.fillAmount -= Time.deltaTime / cooldown;
            if (timerImage.fillAmount < 0f)
                timerImage.fillAmount = 0f;
        }
    }
}
