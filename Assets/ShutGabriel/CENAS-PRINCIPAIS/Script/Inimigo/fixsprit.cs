using UnityEngine;

public class fixsprit : MonoBehaviour
{
    private Quaternion _rotacaoFixa;
    public Transform AlvoEnemy;
    public Vector3 offset = new Vector3(0, 1.5f, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rotacaoFixa = transform.rotation;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = _rotacaoFixa;
    }
}
