using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   /*
   //public GameObject inimigoM;
    public float _intervalo = 2f;
    public int _maxInimigos = 5;
    public float _tempo = 0f;

    public List<GameObject> InimigosAtivos = new List<GameObject>();

    public Vector3 StartPosition;
    
  void Start()
   {
        // _agent = null;
        // _alvo = null;
        StartPosition=transform.position;

   }//
  
    void Update()
   {
     //  if (vida > 0)
      // {
           _tempo += Time.deltaTime;
           if (_tempo >= _intervalo && InimigosAtivos.Count < _maxInimigos)
           {

               _tempo = 0f;
               InimigoON();
               Debug.Log("edsds");
               //gameObject.SetActive(true);
           }
       //}
   }
   

    public void InimigoON()
    {
        GameObject bullet = ObjectPooling.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            //bullet.transform.position = turret.transform.position;
            // bullet.transform.rotation = turret.transform.rotation;
            InimigoDef inimigoDef= bullet.GetComponent<InimigoDef>();

            inimigoDef.Vida();
            //bullet.transform.SetParent(transform);
            bullet.transform.position = StartPosition;

            bullet.SetActive(true);
        }
    }


    */
}
