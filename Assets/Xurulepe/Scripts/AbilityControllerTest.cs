using UnityEngine;
using UnityEngine.UI;

public class AbilityControllerTest : MonoBehaviour
{
    [SerializeField] Ability ability1;
    [SerializeField] Ability ability2;

    void Update()
    {
        ability1.Update();
        ability2.Update();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            ability1.TryUse();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            ability2.TryUse();
    }
}

[System.Serializable]
public class Ability  // classe auxiliar 
{
    public Image timerImage;      
    public float cooldown;        

    public void TryUse()
    {
        if (timerImage.fillAmount <= 0f)
        {
            timerImage.fillAmount = 1f;
        }
    }

    public void Update()
    {
        if (timerImage.fillAmount > 0f)
        {
            timerImage.fillAmount -= Time.deltaTime / cooldown;
            if (timerImage.fillAmount < 0f)
                timerImage.fillAmount = 0f;
        }
    }
}
