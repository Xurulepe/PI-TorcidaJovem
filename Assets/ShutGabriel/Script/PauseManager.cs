using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused = false;

    void Update()
    {
        //if (isPaused != !isPaused)
        //{
        //    isPaused = !isPaused;
        //}
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
