using UnityEngine;

public class FieldEnd : MonoBehaviour
{
    public float Fieldtime = 2f;
     
    [SerializeField] public PlayerControle _playerControle;
    public Transform _Player;
    public InimigoMelee enemyScript1;
    public InimigoShooter enemyScript2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //_playerControle = GetComponent<PlayerControle>();
       // enemyScript1.GetComponent<InimigoMelee>();

      //  enemyScript2.GetComponent<InimigoShooter>();


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
        _playerControle._Field.SetActive(false);

    }

    private void OnEnable()
    {
        Fieldtime = -Time.deltaTime;
        Invoke("DeactivateObj", 3f);
    }
}
