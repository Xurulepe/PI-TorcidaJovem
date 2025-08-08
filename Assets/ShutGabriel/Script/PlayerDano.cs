using UnityEngine;

public class PlayerDano : MonoBehaviour
{
    private MeshRenderer _meshrenderer;
    private Color corOriginal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _meshrenderer = GetComponent<MeshRenderer>();
        if(_meshrenderer != null)
        {
            corOriginal = _meshrenderer.material.color;
        }
    }
    public void LevarDano()
    {
        StartCoroutine(PiscarVermelho());
    }
    private System.Collections.IEnumerator PiscarVermelho()
    {
        if (_meshrenderer != null)
        {
            _meshrenderer.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            _meshrenderer.material.color = corOriginal;
        }

    }
}
