using UnityEngine;

public class FieldEnd : MonoBehaviour
{ 
    public float Fieldtime = 2f;
    [SerializeField] public GameObject fieldPool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
