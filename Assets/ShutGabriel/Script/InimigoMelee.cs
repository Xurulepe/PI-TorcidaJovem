using UnityEngine;

public class InimigoMelee : InimigoDef
{
    public int dano = 1;
    public float IntervaloAtaque = 3f;
    private float tempo = 0f;
    private bool _playerNaArea;
    private GameObject _player;
    protected override void Update()
    {
        base.Update();
        if (_alvo != null)
        {

        }
        if (_playerNaArea && tempo >= IntervaloAtaque)
        {
            Ataque(_player);
            tempo = 0f;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"));
        {
            _playerNaArea = true;
            _player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _playerNaArea = false;
            _player = null;
        }
    }

    private void Ataque(GameObject player)
    {
        Debug.Log("Atacou o player");
    }

    protected override void Start()
    {
        base.Start();
        _agent.speed = 3.5f;
    }
    
    public void LevarDano(int dano)
    {
        vida -= dano;
        if (vida >= 0)
        {
            Morrer();
        }
    }
}
