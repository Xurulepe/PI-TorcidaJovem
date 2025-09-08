using UnityEngine;

public class GMTest : MonoBehaviour
{
    public PlayerTest Player;

    #region Singleton
    public static GMTest Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion


}
