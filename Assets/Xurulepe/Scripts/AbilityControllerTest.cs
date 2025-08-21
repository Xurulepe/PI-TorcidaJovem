using UnityEngine;
using UnityEngine.UI;

public class AbilityControllerTest : MonoBehaviour
{
    [Header("Habilidades")]
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
