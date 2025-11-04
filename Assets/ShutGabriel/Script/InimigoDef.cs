using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.AI;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;

public class InimigoDef : MonoBehaviour
{
    //movimento
    protected NavMeshAgent _agent;
    protected Transform _alvo;
    [SerializeField] protected int vida = 10;

    //status
    [SerializeField] protected int dano = 1;
    [SerializeField]  MeshRenderer[] _renderer;
    [SerializeField] protected ParticleSystem[] _part;
    [SerializeField] protected Collider[] _CL;
    [SerializeField] protected bool _checkHIT;
    [SerializeField] protected bool _checkMorte;
    [SerializeField] protected bool _isHIT;
    [SerializeField] protected Vector3 ScaleStart;
    protected float _tempo = 0f;
    bool _startV;

    [Header("SpriteVirus")]
    [SerializeField] protected GameObject _spriteVirus;
    [SerializeField] protected Transform cameraTransform;
    //[SerializeField] protected Transform shadowPosition;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected List<Sprite> Rostoimg = new List<Sprite>();



    protected virtual void Start()
    {
        //controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        
        _agent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            _alvo = player.transform;
        }

        if (!_startV)
        {
            ScaleStart = transform.localScale;
            _startV = true;
        }

    }
    protected void LateUpdate()
    {
        _spriteVirus.transform.LookAt(_spriteVirus.transform.localPosition + cameraTransform.position);
        //shadowPosition.transform.LookAt(shadowPosition.transform.localPosition + cameraTransform.position);
    }
    
    protected virtual void Update()
    {

        if (_alvo != null && _agent != null)
        {
            _agent.SetDestination(_alvo.position);
        }
        selecaoFace();
    }
    protected virtual void LevarDano(int dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Morrer();

        }
        else
        {
            RecuperarDedano();
        }
    }
    //Morte do inimigo
    protected virtual void Morrer()
    {
        
        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].enabled = false;
        }
        
        _spriteVirus.gameObject.SetActive(false);
        gameObject.SetActive(false);
        
        //yield return new WaitForSeconds(0.10f);

    }
    protected virtual void selecaoFace()
    {
        if (_isHIT == true)
        {
            spriteRenderer.sprite = Rostoimg[1];
        }
        else
        {
            spriteRenderer.sprite = Rostoimg[0];
        }
    }

    protected virtual IEnumerator HitTime()
    {
        _checkMorte = true;
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].transform.DOScale(0.5f, 0.05f);
        }

        for (int i = 0; i < _CL.Length; i++)
        {
            _CL[i].enabled = false;

        }
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].enabled = false;
        }
        yield return new WaitForSeconds(0.25f);

      

        _checkHIT = false;
        LevarDano(dano);
        
        
    }


    public virtual void Vida()
    {

        vida = 10;
        _tempo = 0f;
        _checkMorte = false;
        _isHIT = false;
        
        if (_startV)
        {
            transform.localScale = ScaleStart;
        }

        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].enabled = false;
            _spriteVirus.gameObject.SetActive(true);
        }
    }
    public virtual void RecuperarDedano()
    {

       
        _tempo = 0f;
        _checkMorte = false;
        _isHIT = false;

        if (_startV)
        {
            transform.localScale = ScaleStart;
        }

        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].enabled = false;
            _spriteVirus.gameObject.SetActive(true);
        }
    }

}
