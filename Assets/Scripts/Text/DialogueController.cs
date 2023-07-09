using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

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

    

    private void Start()
    {
        if (dialogueLines.Count > 0)
        {
            index = 0; 
            TypeLine(index);
        }
    }

    public void TypeLine(int lineIndex)
    {
        npcName.text = FindAndSpeakText(dialogueLines[lineIndex].npcName,false);
        textToWrite = FindAndSpeakText(dialogueLines[lineIndex].keyword,true);

        typewriter.StartTypewriterEffect(textToWrite);  // Start the typewriter effect with the specified text
    }
    
    
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

    public void NextLine()
    {
        index++;
        if (index <= dialogueLines.Count)
        {
            TypeLine(index); 
        }
        else
        {
            
        }
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            typewriter.CompleteTypeWriter();
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
}
