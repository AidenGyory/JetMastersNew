using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public void PauseGame()
    {
        Time.timeScale = 0; 
    }
    
    public void UnpauseGame()
    {
        Time.timeScale = 1; 
    }

    public void MusicON()
    {
        Debug.Log("Sound ON");
        //TODO: Add Audio Manager 
    }

    public void MusicOFF()
    {
        Debug.Log("Sound OFF");
    }
    
    public void ResetLevel()
    {
        GameManager.Instance.levelReset = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
}
