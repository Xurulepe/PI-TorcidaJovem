using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Dashing : MonoBehaviour
{
    public PlayerControle _moveScript;

    public float _dashSpeed;
    public float _dashTime;
    public PlayerControle _playerScript;


    // Start is called before the first frame update
    void Start()
    {
        _playerScript = GetComponent<PlayerControle>();
        _moveScript = GetComponent<PlayerControle>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerScript._lockMove == false) 
        {

            DashMove();

        }
         
    }

    public void DashMove()
    {
       

            if (Input.GetKeyDown(KeyCode.Space))
            {

                StartCoroutine(Impulse());

            }


       

       
    }

     IEnumerator Impulse()
    {

        float startTime = Time.time;

        while (Time.time < startTime + _dashTime)
        {

            _moveScript.Controller.Move(_moveScript.moveInput * _dashSpeed * Time.deltaTime);

            yield return null;

        }


    }



}
