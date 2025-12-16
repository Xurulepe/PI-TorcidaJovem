using UnityEngine;

public class SpirteBilbord : MonoBehaviour
{
    public GameObject _spriteVirus;
    public Transform cameraTrans;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraTrans = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        _spriteVirus.transform.LookAt(_spriteVirus.transform.localPosition + cameraTrans.localPosition);
    }
}
