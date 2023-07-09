using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;

[System.Serializable]
public class SaveData
{
    //SAVEDATA GOES HERE
    public int currentLevel;
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }


    //Progression is an LoL concept, allows teachers to see how far through the game students are
    [Header("Progression")]
    int complete = 0; //Progression
    const int maxProgress = 8; //Must be atleast 8

    [SerializeField] Button btnNew, btnCont;


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
        //start from the beginning of the game
        if (savedata == null)
        {
            //DO STUFF TO START FROM BEGINNING
            return;
        }

        //OTHERWISE, DO STUFF TO LOAD GAME DATA
        //e.g. read level number in savedata, changescene to that level
        //Update the progression variable

    }

    public void Save()
    {
        SaveData dataToSave = new SaveData();

        //IMPLEMENT DATA YOU WANT TO SAVE
        //e.g dataToSave.currentLevel = (current level variable)

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