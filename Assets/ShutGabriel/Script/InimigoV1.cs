using UnityEngine;

public class InimigoV1 : InimigoDefault
{
    protected override void Update()
    {
        float distancia = Vector3.Distance(transform.position, _player.position);
        if (distancia > _distanciaATK)
        {
            _Agent.isStopped = false;
            _Agent.SetDestination(_player.position);
        }
        else
        {
            _Agent.isStopped = true;
            Vector3 direcao = (_player.position.normalized - transform.position).normalized;
            if (Time.time >= _TempoEntreATKS)
            {
                Atacar();
                _TempoEntreATKS = Time.time + _TempoEntreATKS;
            }
        }
    }

}
