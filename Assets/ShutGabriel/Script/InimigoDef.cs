using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.AI;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI;

public class InimigoDef : MonoBehaviour
{
    //movimento
    protected NavMeshAgent _agent;
    protected Transform _alvo;
    [SerializeField] protected int vida = 100;


    //status
    [SerializeField] protected int dano = 2;
    [SerializeField]  MeshRenderer[] _renderer;
    [SerializeField] protected ParticleSystem[] _part;
    [SerializeField] protected Collider[] _CL;
    [SerializeField] protected bool _checkHIT;
    [SerializeField] protected bool _checkMorte;
    [SerializeField] protected bool _isHIT;
    [SerializeField] protected Vector3 ScaleStart;
    protected float _tempo = 0f;
    bool _startV;
    public bool _StopTiro;

    [Header("SpriteVirus")]
    [SerializeField] protected GameObject _spriteVirus;
    [SerializeField] protected Transform cameraTransform;
    //[SerializeField] protected Transform shadowPosition;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected List<Sprite> Rostoimg = new List<Sprite>();
    [SerializeField] GameObject hITBOX;
    [SerializeField] Color cor;
    [SerializeField] Color cor2;
    public bool estaMorrendo = false;
    public float TempoMorrendo = 0f;



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
        _spriteVirus.transform.DOScale(Vector3.one * 0.7f, 0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
        if (!_startV)
        {
            ScaleStart = _spriteVirus.transform.localScale;
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
        if (estaMorrendo)
        {

            _spriteVirus.gameObject.SetActive(false);
            TempoMorrendo -= Time.deltaTime;
            if (TempoMorrendo <= 0f)
            {
                
                gameObject.SetActive(false);
                //_spriteVirus.gameObject.SetActive(false);
            }
        }
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

        for (int i = 0; i < _part.Length; i++)
        {
            _part[i].Play();
        }
        
        //tempo antes de deletar
        //yield return new WaitForSeconds(0.10f);

        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].enabled = false;
        }
        TempoMorrendo = 1.5f;
        estaMorrendo = true;

        
        
        

    }
    protected virtual void selecaoFace()
    {
        if (_isHIT == true)
        {
            spriteRenderer.sprite = Rostoimg[1];
            //_spriteVirus.GetComponent<SpriteRenderer>().color = cor;
            _spriteVirus.GetComponent<SpriteRenderer>().DOColor(cor,.25f);

        }
        else
        {
            spriteRenderer.sprite = Rostoimg[0];
        }
    }

    protected virtual IEnumerator HitTime()
    {
        _StopTiro = true;
        _checkMorte = true;
        _agent.isStopped = true;
        hITBOX.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].transform.DOScale(0.5f, 0.25f);
        }
        

        for (int i = 0; i < _CL.Length; i++)
        {
            _CL[i].enabled = false;

        }

        yield return new WaitForSeconds(0.1f);
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

        vida = 100;
        _tempo = 0f;
        _checkMorte = false;
        _isHIT = false;
        estaMorrendo = false;
        
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
        _spriteVirus.GetComponent<SpriteRenderer>().DOColor(cor2, .25f);
        _tempo = 0f;
        _checkMorte = false;
        _isHIT = false;
        _agent.isStopped = false;
        hITBOX.SetActive(true);
        _StopTiro = false;
        estaMorrendo = false;

        if (_startV)
        {
            //transform.localScale = ScaleStart;
            transform.DOScale(ScaleStart, 0.5f);
        }

        for (int i = 0; i < _renderer.Length; i++)
        {
            _renderer[i].enabled = false;
            _spriteVirus.gameObject.SetActive(true);
        }
    }

}
