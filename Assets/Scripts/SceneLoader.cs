using LoLSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void GotoLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoToNewScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }
    
    public void EndGameButton()
    {
        LOLSDK.Instance.CompleteGame();
    }
}
