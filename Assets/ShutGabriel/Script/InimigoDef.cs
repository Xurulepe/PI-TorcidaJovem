using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.AI;
using System.Collections;
using DG.Tweening;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI;
using DG.Tweening.Core.Easing;
using System.Drawing;

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
    [SerializeField] protected ParticleSystem[] _part2;
    [SerializeField] protected Collider[] _CL;
    [SerializeField] protected bool _checkHIT;
  
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
    [SerializeField] protected SpriteRenderer spriteRendererVirus;
    [SerializeField] protected List<Sprite> Rostoimg = new List<Sprite>();
    [SerializeField] GameObject hITBOX;
    public bool estaMorrendo = false;
    [SerializeField] protected float knockbackDuration = 0.3f;
    [SerializeField] protected float knockbackTimer;
    public float TempoMorrendo = 0f;

    [Header("ruan conde")]
    public Material mat;
    public Coroutine flashRoutine;
    public GameObject particule;
    public float tempoPiscar;
    public bool executado;
    //--------------------PARAR INIMIGO----------------------
    protected static bool isPaused = false;
    public GameFlowController _gfc;
    public bool morreu;
    protected float _knockbackForce = 20f;
    protected Vector3 knockbackVelocity;
    protected bool _OnHit;

    void Awake()
    {     
       
        mat = new Material(spriteRendererVirus.sharedMaterial);
        spriteRendererVirus.material = mat;
        knockbackTimer = knockbackDuration;

        mat.SetFloat("_FlashAmount", 0f);
    }


    void OnEnable()
    {
        morreu = false;

        // importante pra pooling
        if (mat != null)
            mat.SetFloat("_FlashAmount", 0f);
    }

    protected virtual void Start()
    {

        //controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        _gfc =GameObject.FindWithTag("GameController").GetComponent<GameFlowController>();
        _gfc.AddEnemy(gameObject);

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
        // para inimigos se o jogo estiver pausada
       // _agent.isStopped = _gfc.isPaused;
      

        if (_alvo != null && _agent != null)

        {
            if (_agent.enabled == true)
            {

                _agent.isStopped = _gfc.isPaused;
                _agent.SetDestination(_alvo.position);
            }
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
   

    public void Flash(float duration = 0.1f)
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashRoutine(duration));
    }

    IEnumerator FlashRoutine(float duration)
    {
        mat.SetFloat("_FlashAmount", 1f);
        yield return new WaitForSeconds(duration);
        mat.SetFloat("_FlashAmount", 0f);
    }

    //Morte do inimigo
    protected virtual void Morrer()
    {
        _gfc.enemyCount++;
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
            //_spriteVirus.GetComponent<SpriteRenderer>().DOColor(cor,.25f);
            if (executado == false)
            {
                Flash(tempoPiscar);

                particule.SetActive(true);
                
                executado = true;
            }

        }
        else
        {
            spriteRenderer.sprite = Rostoimg[0];
            executado = false;

        }
    }

    protected virtual IEnumerator HitTime()
    {
        _StopTiro = true;
     
        vida -= dano;
        if (_agent.enabled)
        {
            _agent.isStopped = true;
            _agent.enabled = false;
        }
        _OnHit = true;
        hITBOX.SetActive(false);

        for (int i = 0; i < _CL.Length; i++)
        {
            _CL[i].enabled = false;

        }

        if (vida <= 0)
        {

           Morrer();
        }
        else
        {
          
            for (int i = 0; i < _renderer.Length; i++)
            {
                _renderer[i].transform.DOScale(0.5f, 0.25f);
            }
            yield return new WaitForSeconds(0.25f);
            for (int i = 0; i < _renderer.Length; i++)
            {
                _renderer[i].enabled = false;
            }
            yield return new WaitForSeconds(0.25f);
            //_agent.enabled = true
            _OnHit = false;
           // RecuperarDedano();
        }   
       
    }


    public virtual void Vida()
    {

        vida = 100;
        _tempo = 0f;

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
        for (int i = 0; i < _part2.Length; i++)
        {
            _part2[i].Play();
        }
        //_spriteVirus.GetComponent<SpriteRenderer>().DOColor(cor2, .25f);
        _tempo = 0f;
 
        _isHIT = false;
        _agent.isStopped = false;
        knockbackTimer = knockbackDuration;
        hITBOX.SetActive(true);
        _StopTiro = false;
        _agent.enabled = true;
        Debug.Log("Move");
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

    private void OnDisable()
    {
        morreu = true;
    }

    public virtual void ApplyKnockback(Vector3 direction, float force = -1)
    {
        if (force < 0)
            force = _knockbackForce;

        direction.y = 0f;
        knockbackVelocity = direction.normalized * force;

        //_agent.enabled = true;
    }

}