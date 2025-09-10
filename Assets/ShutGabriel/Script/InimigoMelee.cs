using UnityEngine;

public class InimigoMelee : InimigoDef
{
    public int dano = 1;
    public float IntervaloAtaque = 3f;
    private float tempo = 0f;
    private bool _playerNaArea = false;
    private GameObject _player;
    protected override void Update()
    {
        base.Update();
        if (_alvo != null)
        {

        }
       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"));
        {
            //Debug.Log("Hit");
            _playerNaArea = true;
            _player = other.gameObject;
            if (_playerNaArea = true)
            {
                Ataque();
                tempo = 0f;
            }
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

    private void Ataque()
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
