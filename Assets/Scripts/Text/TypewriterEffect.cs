using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public float typingSpeed;  // The speed at which characters are "typed"

    public string textToType;
    [SerializeField] public TMP_Text textComponent;

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

        typingCoroutine = null;  // Reset the typing coroutine reference
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