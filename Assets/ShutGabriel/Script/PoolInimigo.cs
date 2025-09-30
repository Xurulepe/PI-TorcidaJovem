using UnityEngine;

public class PoolInimigo : ObjectPooling
{
    protected float _TimeReal;
    [SerializeField] protected float _TimeStart = 3;
    [SerializeField] protected bool timerIsRunning = false;
    protected GameObject _tempInimigo;

    protected override void Start()
    {
        base.Start();
        _TimeReal = _TimeStart;
        timerIsRunning = true;
    }
}
