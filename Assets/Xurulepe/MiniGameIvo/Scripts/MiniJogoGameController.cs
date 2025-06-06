using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniJogoGameController : MonoBehaviour
{
    [SerializeField] Transform _groundBase;
    [SerializeField] float _groundH;
    [SerializeField] float _distance;

    [SerializeField] bool _checkGroundCount;

    public int _groundNumber;

    void Start()
    {
        _groundH = _groundBase.position.y;

        Invoke("GroundTime", 0.25f);
    }

    void GroundTime()
    {
        for (int i = 0; i < _groundNumber; i++)
        {
            if (i == _groundNumber - 1)
            {
                _checkGroundCount = true;
            }
            GroundStart();
        }
    }

    void GroundStart()
    {
        GameObject ground = MiniJogoGroundPool.Instance.GetPooledObject();
        if (ground != null)
        {

            ground.transform.position = new Vector2(ground.transform.position.x, _groundH + _distance);
            _groundH = ground.transform.position.y;

            ground.SetActive(true);
        }
    }

    public void ResetarCena(string cena)
    {
        SceneManager.LoadScene(cena);
    }

}
