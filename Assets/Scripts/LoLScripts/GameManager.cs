using UnityEngine;
using LoLSDK;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    //SAVEDATA GOES HERE
    public int currentLevel;
    
    public int level1Stars; 
    public int level2Stars; 
    public int level3Stars; 
    public int level4Stars; 
    public int level5Stars; 
    public int level6Stars; 
    public int level7Stars; 
    public int level8Stars; 
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }


    //Progression is an LoL concept, allows teachers to see how far through the game students are
    [Header("Progression")]
    int complete = 0; //Progression
    const int maxProgress = 8; //Must be atleast 8
    
    //Stars for each level 
    public int level1Stars; 
    public int level2Stars; 
    public int level3Stars; 
    public int level4Stars; 
    public int level5Stars; 
    public int level6Stars; 
    public int level7Stars; 
    public int level8Stars;

    [SerializeField] Button btnNew, btnCont;
    public bool levelReset = false;


    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;

            //This initialised the titlescreen buttons "new game" and "load game" with functionality
            Helper.StateButtonInitialize<SaveData>(btnNew, btnCont, Load);
            SetUp();
        }
        else if (Instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void SetUp()
    {
        complete = 0;
        btnNew.GetComponentInChildren<TextMeshProUGUI>().text = FindAndSpeakText("newGame", false);
        btnCont.GetComponentInChildren<TextMeshProUGUI>().text = FindAndSpeakText("loadGame", false);
    }

    //note this function is called regardless of where there's save data or not
    //The savedata is loaded from the LoL sever through the SDK through the LoLSDKHelper class
    void Load(SaveData savedata)
    {
        //If no save data is passed, start from the beginning of the game
        if (savedata == null)
        {
            //DO STUFF TO START FROM BEGINNING
            complete = 0;
            level1Stars = 0; 
            level2Stars = 0; 
            level3Stars = 0; 
            level4Stars = 0; 
            level5Stars = 0; 
            level6Stars = 0; 
            level7Stars = 0; 
            level8Stars = 0; 
            SceneManager.LoadScene("LevelSelect");
            return;
        }

        //OTHERWISE, DO STUFF TO LOAD GAME DATA
        //e.g. read level number in savedata, changescene to that level
        //Update the progression variable
        complete = savedata.currentLevel;
        level1Stars = savedata.level1Stars; 
        level2Stars = savedata.level2Stars; 
        level3Stars = savedata.level3Stars; 
        level4Stars = savedata.level4Stars; 
        level5Stars = savedata.level5Stars; 
        level6Stars = savedata.level6Stars; 
        level7Stars = savedata.level7Stars; 
        level8Stars = savedata.level8Stars; 
        SceneManager.LoadScene("LevelSelect");
    }

    public void Save()
    {
        SaveData dataToSave = new SaveData();

        //IMPLEMENT DATA YOU WANT TO SAVE
        dataToSave.currentLevel = complete;
        dataToSave.level1Stars = level1Stars; 
        dataToSave.level2Stars = level2Stars; 
        dataToSave.level3Stars = level3Stars; 
        dataToSave.level4Stars = level4Stars; 
        dataToSave.level5Stars = level5Stars; 
        dataToSave.level6Stars = level6Stars; 
        dataToSave.level7Stars = level7Stars; 
        dataToSave.level8Stars = level8Stars; 

        //Saves the data to LoL servers through the SDK
        LOLSDK.Instance.SaveState(dataToSave);
    }


    //Call this when player presses a button at the very end of the game to tell the SDK the game has been finished
    public void EndGameButton()
    {
        LOLSDK.Instance.CompleteGame();
    }

    // Read progress points to check current progress
    public int ReadProgress()
    {
        return complete; 
    }

    public void SetProgress(int progress)
    {
        complete = progress;
    }

    //Call this at approprite times to update student's progress visible to teacher
    //e.g. call at the end of a level
    //score can be 0 if the game doesn't have score
    public void SubmitProgress(int score)
    {
        LOLSDK.Instance.SubmitProgress(score, complete, maxProgress);
    }

    //Call this to display text and/or have text spoken aloud
    //By default speaktext is true, set to false when displaying text for buttons and such
    public string FindAndSpeakText(string arg, bool speaktext = true)
    {
        if (speaktext)
        {
            //Dont TTS in editor because its not supported
            //Test correct functionality using the harness on the dev portal
#if !UNITY_EDITOR
            LOLSDK.Instance.SpeakText(arg);
#endif
        }

        //Looks in Language.json and finds the key arg and returns value
        return SharedState.LanguageDefs[arg];
    }
}