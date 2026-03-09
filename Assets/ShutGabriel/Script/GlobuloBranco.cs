using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GlobuloBranco : MonoBehaviour
{
    [Header("VIDA")]
    public float vidaMaxima = 100f;
    public float vidaAtual;

    [Header("Movimentação do Glóbulo:")]
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] Transform _currentTarget;
    [SerializeField] List<Transform> enemies = new List<Transform>();
    public float _updateRate = 0.5f;
    private GameObject _enemy;

    [Header("Alvo:")]
    bool enemyNaArea;
    public float IntervaloAtaque = 1f;

    [Header("Dano")]
    public float dano = 20;
    private float proximoAtaque = 0f;
    void Start()
    {

        if (_agent == null)
            _agent = GetComponent<NavMeshAgent>();
        vidaAtual = vidaMaxima;
        InvokeRepeating(nameof(UpdateTarget), 0f, _updateRate);

    }
    void UpdateTarget()
    {
        UpdateEnemyList();
        _currentTarget = FindClosestEnemy();

        if (_currentTarget != null)
        {
            _agent.SetDestination(_currentTarget.position);
        }
    }
    void UpdateEnemyList()
    {
        enemies.Clear();

        GameObject[] meleeEnemies = GameObject.FindGameObjectsWithTag("EnemyMelee");
        GameObject[] shooterEnemies = GameObject.FindGameObjectsWithTag("EnemyShooter");

        foreach (GameObject enemy in meleeEnemies)
        {
            enemies.Add(enemy.transform);
        }

        foreach (GameObject enemy in shooterEnemies)
        {
            enemies.Add(enemy.transform);
        }
    }
    Transform FindClosestEnemy()
    {
        if (enemies.Count == 0)
            return null;

        Transform closest = null;
        float minDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Transform enemy in enemies)
        {
            float distance = (enemy.position - currentPosition).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = enemy;
            }
        }

        return closest;
    }
    public void Invocar()
    {
        Debug.Log("InvocandoAliados");
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyMelee") || other.gameObject.CompareTag("EnemyShooter"));
        {
            InimigoDef inimigo = other.GetComponent<InimigoDef>();

            if (inimigo != null)
            {         
                ReceberDano(10);

            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform == _currentTarget)
        {
            InimigoDef inimigo = other.GetComponent<InimigoDef>();

            if (inimigo != null && Time.time >= proximoAtaque)
            {
           
                proximoAtaque = Time.time + 1f; 
            }
        }
    }
    public void ReceberDano(float quantidade)
    {
        vidaAtual -= quantidade;

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }
    void Morrer()
    {
        Debug.Log("O Glóbulo morreu!");
        gameObject.SetActive(false);
    }
    public virtual void Vida()
    {

        vidaAtual = 100;
       
    }
}
