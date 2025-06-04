using UnityEngine;

public class minijogoGP : minijogoOP
{
    public static minijogoGP _groundPool;

    public override void Awake()
    {
        base.Awake();
        _groundPool = this;

    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
