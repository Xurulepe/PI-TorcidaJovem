using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Spawner : InimigoDef
{
   public GameObject inimigoM;
    public float _intervalo = 2f;
    public int _maxInimigos = 5;
    private float _tempo = 0f;

    public List<GameObject> InimigosAtivos = new List<GameObject>();

    protected override void Start()
    {
        _agent = null;
        _alvo = null;

    }
    protected override void Update()
    {
        if (vida > 0)
        {
            _tempo += Time.deltaTime;
            if (_tempo >= _intervalo &&InimigosAtivos.Count < _maxInimigos)
            {

                _tempo = 0f;
                SpawnInimigo();
                //gameObject.SetActive(true);
            }
        }
    }


    void SpawnInimigo()
    {
        GameObject novoInimigo = Instantiate(inimigoM, transform.position, Quaternion.identity);
        InimigosAtivos.Add(novoInimigo);
        InimigoDef script = novoInimigo.GetComponent<InimigoDef>();
        if (script != null)
        {
            script.spawner = this;
        }
    }
    public void RemoverInimigo(GameObject Inimigo)
    {
        if (InimigosAtivos.Contains(Inimigo))InimigosAtivos.Remove(Inimigo);
    }
    public override void Morrer()
    {
        InimigosAtivos.Clear();
        base.Morrer();
    }
}
