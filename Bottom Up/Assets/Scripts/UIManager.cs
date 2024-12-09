using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject StartCanvas;
    public GameObject SettingCanvas;
    public GameObject TutorialCanvas;
    public GameObject CreditsCanvas;
    public GameObject GameOverCanvas;

    private GameObject currentCanvas;
    private GameObject previousCanvas;

    public void ChangeScene(string sceneName)
    {
        if (!(string.IsNullOrEmpty(sceneName)))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    public void NotImplemented()
    {
        Debug.Log("NotImplementedNotImplementedNotImplementedNotImplemented");
    }

    public void GoBack()
    {
        Debug.Log("return to previous canvas");
        currentCanvas.SetActive(false);
        previousCanvas.SetActive(true);
    }

    public void EnterGame()
    {
        Debug.Log("enter");
        ChangeScene("Game");
    }
    public void Settings()
    {
        Debug.Log("settings");
        currentCanvas = SettingCanvas;
        previousCanvas = StartCanvas;

        previousCanvas.SetActive(false);
        currentCanvas.SetActive(true);
    }
    public void Tutorial()
    {
        Debug.Log("tutorial");
        currentCanvas = TutorialCanvas;
        previousCanvas = StartCanvas;

        previousCanvas.SetActive(false);
        currentCanvas.SetActive(true);
    }
    public void Credits()
    {
        Debug.Log("credits");
        currentCanvas = CreditsCanvas;
        previousCanvas = StartCanvas;

        previousCanvas.SetActive(false);
        currentCanvas.SetActive(true);
    }
    public void ExitGame()
    {
        Debug.Log("exit");
        Application.Quit();
    }

    public void GameOver()
    {
        ChangeScene("Start");
        StartCanvas.SetActive(false);
        GameOverCanvas.SetActive(true);

        currentCanvas = GameOverCanvas;
        previousCanvas = StartCanvas;
    }
}
