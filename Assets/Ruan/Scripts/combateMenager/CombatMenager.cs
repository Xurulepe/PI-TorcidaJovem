using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CombatMenager : MonoBehaviour
{
    public PlayerControle pl;
    public static CombatMenager instance;

    public bool canRaceiveInput;
    public bool inputReceived;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canRaceiveInput)
            {
                inputReceived = true;
                canRaceiveInput = false;
            }
            else
            {
                return;
            }

        }

    }

    public void InputManger()
    {
        if (!canRaceiveInput)
        {
            canRaceiveInput = true;
        }
        else
        {
            canRaceiveInput = false;
        }
    }
}
