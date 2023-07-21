using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [Serializable]
    public class NPCLine
    {
        public string npcName;
        public string keyword; 
    }

    public TypewriterEffect typewriter;

    [SerializeField] private TMP_Text npcName;

    public List<NPCLine> dialogueLines;
    private string textToWrite;
    private int index;

    public void StartDialogue()
    {
        if (dialogueLines.Count > 0)
        {
            index = 0; 
            TypeLine(index);
        }
    }

    void TypeLine(int lineIndex)
    {
        npcName.text = FindAndSpeakText(dialogueLines[lineIndex].npcName,false);
        textToWrite = FindAndSpeakText(dialogueLines[lineIndex].keyword,true);

        typewriter.StartTypewriterEffect(textToWrite);  // Start the typewriter effect with the specified text
    }
    
    
    string FindAndSpeakText(string arg, bool speaktext = true)
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

    void NextLine()
    {
        if (index < dialogueLines.Count-1)
        {
            index++;
            TypeLine(index); 
        }
        else
        {
            //finish dialogue
            FinishDialogue();
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InputContinue();
        }
    }

    public void InputContinue()
    {
        //check if the line was still typing or not
        if (!typewriter.CompleteTypeWriter())
        {
            NextLine();
        }
    }

    public bool CheckLastLine()
    {
        if (index >= dialogueLines.Count -1)
        {
            return true; 
        }

        return false; 
    }

    public void FinishDialogue()
    {
        LevelManager.Instance.FinishDialogue();
    }
}
