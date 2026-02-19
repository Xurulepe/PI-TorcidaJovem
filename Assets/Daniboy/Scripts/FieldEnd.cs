using Unity.VisualScripting;
using UnityEngine;

public class FieldEnd : MonoBehaviour
{
    public float Fieldtime = 2f;
     
    [SerializeField] public PlayerControle _playerControle;
    public Animator forceFildAnimator;
    public Transform _Player;
    public InimigoMelee enemyScript1;
    public InimigoShooter enemyScript2;
    private float tempoAtivo;
    public float tempoAtivoMax;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Update()
    {
        tempoAtivo -= Time.deltaTime;

        if (tempoAtivo <= 0)
        {
            forceFildAnimator.SetTrigger("Desativar");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            enemyScript1.ApplyKnockback(other.gameObject.transform.position);
            Debug.Log("Field");
            enemyScript2.ApplyKnockback(other.gameObject.transform.position);            
        }
    }

    public void DeactivateObj()
    {
        _playerControle._Field.SetActive(false);
    }

    private void OnEnable()
    {
       tempoAtivo = tempoAtivoMax;
    }
}
