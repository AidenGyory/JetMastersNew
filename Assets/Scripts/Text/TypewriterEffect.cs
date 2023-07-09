using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private DialogueController controller;
    [Space]
    [SerializeField] private float typingSpeed;  // The speed at which characters are "typed"
    
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private UnityEvent whenLineIsFinished;
    [SerializeField] private UnityEvent whenDialogueFinishes; 

    private string textToType;
    private Coroutine typingCoroutine;

    public void StartTypewriterEffect(string text)
    {
        // Stop any existing typewriter coroutine
        if (typingCoroutine != null)
        {
            
            StopCoroutine(typingCoroutine);
        }

        textToType = text;
        textComponent.text = string.Empty;  // Clear the text component

        // Start the typewriter coroutine
        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (char character in textToType)
        {
            textComponent.text += character;  // Add the character to the text component

            // Wait for the specified typing speed
            yield return new WaitForSeconds(typingSpeed);
        }

        if (controller.CheckLastLine())
        {
            whenDialogueFinishes?.Invoke();
        }
        else
        {
            whenLineIsFinished?.Invoke();
        }
        typingCoroutine = null; 
        // Reset the typing coroutine reference
    }
    
    public void CompleteTypeWriter()
    {
        // If the spacebar is pressed, complete the current typewriter effect immediately
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            textComponent.text = textToType;  // Set the full text immediately
            typingCoroutine = null;
        }
    }
}