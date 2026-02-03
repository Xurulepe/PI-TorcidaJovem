using UnityEngine;

public class FieldEnd : MonoBehaviour
{ 
    public float Fieldtime = 2f;
    [SerializeField] public GameObject fieldPool;
    public Transform _Player;
    public InimigoMelee enemyScript1;
    public InimigoShooter enemyScript2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        enemyScript1.GetComponent<InimigoMelee>();

        enemyScript2.GetComponent<InimigoShooter>();


    }

    void OnTriggerEnter(Collider other) 
    { 
    
    if (other.gameObject.layer == 6)
        {
         
            enemyScript1.ApplyKnockback(other.gameObject.transform.position);
            Debug.Log("Field");
            enemyScript2.ApplyKnockback(other.gameObject.transform.position);

        }
    
    
    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _Player.transform.position;
    }

 
    void DeactivateObj()
    {
        fieldPool.SetActive(false);
    }

    private void OnEnable()
    {
      Fieldtime =- Time.deltaTime;
        Invoke("DeactivateObj", 0.3f);
    }
}
