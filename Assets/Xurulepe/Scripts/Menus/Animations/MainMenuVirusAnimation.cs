using DG.Tweening;
using UnityEngine;

public class MainMenuVirusAnimation : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _virus1;
    [SerializeField] private GameObject _virus2;
    [SerializeField] private GameObject _virus3;

    #region CONTROLE DOTWEEN
    [Header("Controle de animação dotween")]
    [SerializeField] private float escalaMin1;   // escala mínima do pulso
    [SerializeField] private float escalaMax1;   // escala máxima do pulso

    [SerializeField] private float escalaMin2;   // escala mínima do pulso
    [SerializeField] private float escalaMax2;   // escala máxima do pulso

    [SerializeField] private float escalaMin3;   // escala mínima do pulso
    [SerializeField] private float escalaMax3;   // escala máxima do pulso

    [SerializeField] private float duracaoMin;  // duração mínima
    [SerializeField] private float duracaoMax;  // duração máxima

    [SerializeField] private Ease ease = Ease.InOutSine;  // suavização

    private Tween tween1, tween2, tween3;
    #endregion

    private void Start()
    {
        IniciarPulso1(_virus1.transform);
        IniciarPulso2(_virus2.transform);
        IniciarPulso3(_virus3.transform);
    }

    public void AnimaBackGroundTrue()
    {
        _animator.SetBool("Move", true);
    }

    public void AnimaBackGroundFalse()
    {
        _animator.SetBool("Move", false);
    }

    void IniciarPulso1(Transform transobj)
    {
        float escalaAlvo = Random.Range(escalaMin1, escalaMax1);
        float duracao = Random.Range(duracaoMin, duracaoMax);

        tween1?.Kill();
        tween1 = transobj.DOScale(escalaAlvo, duracao)
            .SetEase(ease)
            .OnComplete(() => IniciarPulso1(transobj));
    }

    void IniciarPulso2(Transform transobj)
    {
        float escalaAlvo = Random.Range(escalaMin2, escalaMax2);
        float duracao = Random.Range(duracaoMin, duracaoMax);

        tween2?.Kill();
        tween2 = transobj.DOScale(escalaAlvo, duracao)
            .SetEase(ease)
            .OnComplete(() => IniciarPulso2(transobj));
    }

    void IniciarPulso3(Transform transobj)
    {
        float escalaAlvo = Random.Range(escalaMin3, escalaMax3);
        float duracao = Random.Range(duracaoMin, duracaoMax);

        tween3?.Kill();
        tween3 = transobj.DOScale(escalaAlvo, duracao)
            .SetEase(ease)
            .OnComplete(() => IniciarPulso3(transobj));
    }

    public void KillTweens()
    {
        tween1?.Kill();
        tween2?.Kill();
        tween3?.Kill();
    }
}
