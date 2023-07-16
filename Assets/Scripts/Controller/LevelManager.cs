using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //instancing so its easier to find the LevelManager
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }
    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    [Header("Controllers")]
    [SerializeField] DialogueController dialogueController;
    [SerializeField] SpaceshipController spaceshipController;
    [Header("Win/Loss")]
    [SerializeField] GameObject loseUI;
    [SerializeField] GameObject winUI;
    [SerializeField] int levelNumber; //game needs to know what level you're on
    [SerializeField] float losingVelocity = 20f;

    private void Start()
    {
        if (!GameManager.Instance.levelReset)
        {
            //Start level normally
            spaceshipController.ToggleActive(false);
            dialogueController.StartDialogue();
        }
        else
        {
            //Start level skipping dialogue
            GameManager.Instance.levelReset = false;
            FinishDialogue();
        }
    }

    public void FinishDialogue()
    {
        dialogueController.gameObject.SetActive(false);
        spaceshipController.ToggleActive(true);
    }

    public void CheckWinState()
    {
        //check if the player is going too fast
        /*
        if (other.GetComponentInParent<MainForwardThruster>().velocityRead > losingVelocity)
            Debug.Log("You lose!");
        else
            winEvent?.Invoke();
        */
            spaceshipController.ToggleActive(false);
        winUI.SetActive(true);
    }

    public void WinEndLevel()
    {
        //only increase progress if you haven't already finished the level
        if (GameManager.Instance != null && GameManager.Instance.ReadProgress() <= levelNumber - 1)
        {
            GameManager.Instance.SetProgress(GameManager.Instance.ReadProgress() + 1);
            GameManager.Instance.Save();
        }
        
        //go back to level select
        SceneManager.LoadScene("LevelSelect");
    }

    public void LoseState()
    {
        loseUI.SetActive(true);
        spaceshipController.SpaceshipBroken();
    }

    //reloads the current scene and sets a reset flag
    public void ResetLevel()
    {
        GameManager.Instance.levelReset = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
