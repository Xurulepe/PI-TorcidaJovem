using System.Threading;
using UnityEngine;

public class Spawn : InimigoDefault
{
    protected void Spawner()
    {
        _SpawnTimer = _SpawnTimer + Time.deltaTime;
        int x = Random.Range(0, 4);
        if (x >= 1.5f)
        {
            Instantiate(inimigo1, _Spawner[x].transform.position, Quaternion.identity);
            _SpawnTimer = 0;
        }
    }


}
