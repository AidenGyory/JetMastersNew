using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private DialogueController controller;
    [Space]
    [SerializeField] private float typingSpeed;  // The speed at which characters are "typed"
    
    [SerializeField] private TMP_Text textComponent;

    [SerializeField] private AudioSource typeSFX; 

    private int characterCount;
    private Coroutine typingCoroutine;
    public float timer; 
    [SerializeField] Button continueBtn; 

    public void StartTypewriterEffect(string text)
    {
        timer = 0f;
        continueBtn.interactable = false; 
        // Stop any existing typewriter coroutine
        if (typingCoroutine != null)
        {
            
            StopCoroutine(typingCoroutine);
        }
        
        textComponent.text = text;  // Clear the text component
        characterCount = textComponent.GetTextInfo(text).characterCount;

        // Start the typewriter coroutine
        typingCoroutine = StartCoroutine(TypeText());
    }

    private void Update()
    {
        if (timer < 5f)
        {
            timer += Time.deltaTime; 
        }
        else
        {
            continueBtn.interactable = true; 
        }
    }

    private IEnumerator TypeText()
    {
        
        for (int i = 1; i <= characterCount; i++)
        {
            textComponent.maxVisibleCharacters = i;
            if (GameManager.Instance.sound)
            {
                typeSFX.Play();
            }
            
            yield return new WaitForSeconds(typingSpeed);
        }

        if (controller.CheckLastLine())
        {
            //controller.FinishDialogue();
        }
        else
        {
            //whenLineIsFinished?.Invoke();
        }
        typingCoroutine = null; 
        // Reset the typing coroutine reference
    }
    
    public bool CompleteTypeWriter()
    {
        // If the spacebar is pressed, complete the current typewriter effect immediately
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            textComponent.maxVisibleCharacters = characterCount;  // Set the full text immediately
            typingCoroutine = null;
            return true;
        }
        //typewriter already completed
        return false;
    }
}