using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public List<Transform> animatedElements;
    public Button autoSelectButton;

    private void OnEnable()
    {
        if (autoSelectButton != null)
        {
            autoSelectButton.Select();
        }
    }
}
