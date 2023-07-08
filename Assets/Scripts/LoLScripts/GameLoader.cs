using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LoLSDK;
using System.IO;
using SimpleJSON;
using UnityEngine.SceneManagement;
 
namespace JetMaster
{
    public class GameLoader : MonoBehaviour
    {
        [Header("Editor Variables")]
        private string startGamePath = "startGame.json";
        private string langPath = "language.json";
        private string langCode = "en";
        private string menuScene = "MainMenu";

        private void Awake()
        {
#if UNITY_EDITOR
            ILOLSDK webGL = new LoLSDK.MockWebGL();
#elif UNITY_WEBGL
			ILOLSDK webGL = new LoLSDK.WebGL();
#endif

            //GAMENAME GOES HERE
            LOLSDK.Init(webGL, "com.catalyst-games.jet-master");

            LOLSDK.Instance.StartGameReceived += new StartGameReceivedHandler(StartGame);
            LOLSDK.Instance.GameStateChanged += new GameStateChangedHandler(gameState => Debug.Log(gameState));
            LOLSDK.Instance.QuestionsReceived += new QuestionListReceivedHandler(questionList => Debug.Log(questionList));
            LOLSDK.Instance.LanguageDefsReceived += new LanguageDefsReceivedHandler(HandleLanguage);

            // Mock the platform-to-game messages when in the Unity editor.
#if UNITY_EDITOR
            LoadMockData();
#endif

            LOLSDK.Instance.GameIsReady();
        }

        void StartGame(string json)
        {
            //load the starting scene
            SceneManager.LoadScene(menuScene, LoadSceneMode.Single);
            SharedState.StartGameData = JSON.Parse(json);
        }

        void HandleLanguage(string json)
        {
            JSONNode langDefs = JSON.Parse(json);
            SharedState.LanguageDefs = langDefs;
        }

        private void LoadMockData()
        {
            string startDataFilePath = Path.Combine(Application.streamingAssetsPath, startGamePath);

            //Load LanguageCode payload
            if (File.Exists(startDataFilePath))
            {
                string startdataAsJSON = File.ReadAllText(startDataFilePath);

                JSONNode startGamePayload = JSON.Parse(startdataAsJSON);
                langCode = startGamePayload["languageCode"];

                StartGame(startdataAsJSON);
            }

            //Load appropriate language file
            string langFilePath = Path.Combine(Application.streamingAssetsPath, langPath);
            if (File.Exists(langFilePath))
            {
                string langDataAsJson = File.ReadAllText(langFilePath);

                JSONNode langDefs = JSON.Parse(langDataAsJson);
                HandleLanguage(langDefs[langCode].ToString());
            }
        }

    }
}