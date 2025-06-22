using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniMenuIVO : MonoBehaviour
{
    public void TrocarCena()
    {
        SceneManager.LoadScene("MGAME");
    }
    public void VoltarMenu()
    {
        SceneManager.LoadScene("MGMENU");
    }
}
