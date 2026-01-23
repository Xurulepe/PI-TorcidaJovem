using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]
public class LaserBeam : MonoBehaviour
{
    public GameObject _Player;
    public Transform _gunTransform;
    public float _gunrange;
    public float _guntime;
    public float Timefire;
    LineRenderer _lineRenderer;
    public float fireRate;
    public PlayerControle _pLayerControle;
    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        Timefire += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.E) && Timefire > fireRate) 
        {

            Timefire = 0;
            _lineRenderer.SetPosition(0, _gunTransform.position);
            Vector3 rayOrigin = _Player.transform.position;
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, _Player.transform.forward, out hit, _gunrange))
            {

                _lineRenderer.SetPosition(1, hit.point);
                Debug.Log(hit.transform.position);


            }
            else
            {


                _lineRenderer.SetPosition(1, rayOrigin + (_Player.transform.forward * _gunrange));



            }
            StartCoroutine(ShootLaser());
        }
    }

    IEnumerator ShootLaser() 
    {
        _pLayerControle._anima_Robo.SetTrigger("Laser");
        _lineRenderer.enabled = true;
        yield return new WaitForSeconds(_guntime);
        _lineRenderer.enabled = false;
        _pLayerControle._anima_Robo.SetTrigger("Fim_laser");
    }


}
