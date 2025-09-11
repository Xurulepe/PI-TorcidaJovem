using UnityEngine;

public class Projectille : MonoBehaviour
{

    public float _speed = 15f;
    public int _dano = 1;
    public float _tempoVida = 4f;

    private Vector3 direcao = Vector3.forward;

    public void SetDirecao(Vector3 dir)
    {
        direcao = dir.normalized;
        Destroy(gameObject, _tempoVida);
    }

    void Update()
    {
        transform.position += direcao * _speed * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"player sofreu {_dano} de dano");
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
