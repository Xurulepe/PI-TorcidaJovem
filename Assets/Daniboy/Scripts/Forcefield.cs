using UnityEngine;

public class Forcefield : MonoBehaviour
{
    public Animator _anim;


    // Update is called once per frame
    void Update()
    {
        animationactive();
    }

    void animationactive()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            _anim.SetTrigger("Expand");


        }
      

    }





}
