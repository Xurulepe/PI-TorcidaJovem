using UnityEngine;

public class InimigoATK : MonoBehaviour
{
    public float _timeBetweenAttacks = 0.5f;
    public int _Dano = 10;
    Animator _anim;
    GameObject _Player;
    [SerializeField] bool _PlayerinRange;
    float _timer;
    //PlayerHealth playerHealth;
    private void Awake()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        //PlayerHealth = GetComponent<PlayerHealth>();
        _anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _Player)
        {
            _PlayerinRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _Player)
        {
            _PlayerinRange = false;
        }
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _timeBetweenAttacks && _PlayerinRange)
        {
            //Attack();
        }
        void Attack()
        {
            //if(_PlayerHealth)
        }
    }
}
