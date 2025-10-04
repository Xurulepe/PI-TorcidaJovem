using UnityEngine;
using UnityEngine.UI;

public class ManaBarUI : MonoBehaviour
{
    [SerializeField] private Image _manaBar;

    private void Awake()
    {
        _manaBar = GetComponent<Image>();
    }

    private void Start()
    {
        // inscrever no evento de mudan�a de energia do jogador

        UpdateManaBar();
    }

    private void UpdateManaBar()
    {
        // atualizar a barra de energia com base na vida atual do jogador
    }
}
