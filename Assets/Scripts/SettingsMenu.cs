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
        GameManager.Instance.ToggleSound(true);
        GameManager.Instance.GetComponent<AudioSource>().Play();
    }

    public void MusicOFF()
    {
        GameManager.Instance.ToggleSound(false);
        GameManager.Instance.GetComponent<AudioSource>().Stop();

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
