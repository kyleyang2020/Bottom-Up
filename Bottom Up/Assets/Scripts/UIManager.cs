using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void EnterGame()
    {
        Debug.Log("enter");
    }
    public void Settings()
    {
        Debug.Log("settings");
    }
    public void Tutorial()
    {
        Debug.Log("tutorial");
    }
    public void Credits()
    {
        Debug.Log("credits");
    }
    public void ExitGame()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}
