using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField] private TMP_Text textDisplay;
    [SerializeField] private string textKeyword;
    [SerializeField] private bool speakText; 

    private void Start()
    {
        textDisplay.text = FindAndSpeakText(textKeyword, speakText); 
    }
    
    public string FindAndSpeakText(string arg, bool speaktext = true)
    {
        if (speaktext)
        {
            //Dont TTS in editor because its not supported
            //Test correct functionality using the harness on the dev portal
#if !UNITY_EDITOR
            LoLSDK.LOLSDK.Instance.SpeakText(arg);
#endif
        }

        //Looks in Language.json and finds the key arg and returns value
        return SharedState.LanguageDefs[arg];
    }
}
