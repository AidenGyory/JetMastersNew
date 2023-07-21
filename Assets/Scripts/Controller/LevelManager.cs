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
    [SerializeField] SpaceshipController player;
    [Header("Win/Loss")]
    [SerializeField] GameObject loseUI;
    [SerializeField] GameObject winUI;
    [SerializeField] int levelNumber; //game needs to know what level you're on
    [Range(0,3)]
    public int starsCollected;
    [Space]
    [SerializeField] private int tokensNeedForStar1; 
    [SerializeField] private int tokensNeedForStar2; 
    [SerializeField] private int tokensNeedForStar3;

     

    private void Start()
    {
        if (!GameManager.Instance.levelReset)
        {
            //Start level normally
            player.ToggleActive(false);
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
        player.ToggleActive(true);
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
            player.ToggleActive(false);
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

        var currentLevelStars = GetLevelStars();
        
        if (starsCollected > currentLevelStars)
        {
            SaveLevel(levelNumber); 
            GameManager.Instance.Save();
        }
        //go back to level select
        GoToLevelSelect();
    }

    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }
    private void SaveLevel(int level)
    {
        switch (level)
        {
            case 1: GameManager.Instance.level1Stars = starsCollected; break;
            case 2: GameManager.Instance.level2Stars = starsCollected; break;
            case 3: GameManager.Instance.level3Stars = starsCollected; break;
            case 4: GameManager.Instance.level4Stars = starsCollected; break;
            case 5: GameManager.Instance.level5Stars = starsCollected; break;
            case 6: GameManager.Instance.level6Stars = starsCollected; break;
            case 7: GameManager.Instance.level7Stars = starsCollected; break;
            case 8: GameManager.Instance.level8Stars = starsCollected; break;
        }
    }

    private int GetLevelStars()
    {
        return levelNumber switch
        {
            1 => GameManager.Instance.level1Stars,
            2 => GameManager.Instance.level2Stars,
            3 => GameManager.Instance.level3Stars,
            4 => GameManager.Instance.level4Stars,
            5 => GameManager.Instance.level5Stars,
            6 => GameManager.Instance.level6Stars,
            7 => GameManager.Instance.level7Stars,
            8 => GameManager.Instance.level8Stars,
            _ => 0
        };
    }
    
    public void LoseState()
    {
        loseUI.SetActive(true);
        player.SpaceshipBroken();
    }

    //reloads the current scene and sets a reset flag
    public void ResetLevel()
    {
        GameManager.Instance.levelReset = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int CheckStarCritera()
    {
        if (player.tokensCollected >= tokensNeedForStar1)
        {
            starsCollected = 1; 
        }
        if (player.tokensCollected >= tokensNeedForStar2)
        {
            starsCollected = 2; 
            
        }
        if (player.tokensCollected >= tokensNeedForStar3)
        {
            starsCollected = 3; 
        }

        return starsCollected; 
    }
}
