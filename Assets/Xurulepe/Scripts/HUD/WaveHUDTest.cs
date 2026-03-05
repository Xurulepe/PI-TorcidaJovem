using UnityEngine;

public class WaveHUDTest : MonoBehaviour
{
    public WaveHUD waveHUD;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            waveHUD.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            waveHUD.gameObject.SetActive(false);
        }
    }
}
