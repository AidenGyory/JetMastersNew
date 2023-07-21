using TMPro;
using UnityEngine;

public class TokenUIDisplay : MonoBehaviour
{
    public int totalTokens;
    private SpaceshipController player; 
    [SerializeField] private TMP_Text tokenUI;

    private void Start()
    {
        player = FindObjectOfType<SpaceshipController>(); 
        // Find all Tokens in Scene to establish MAX coins; 
        if (totalTokens < 1)
        {
            var tokens = FindObjectsOfType<CollectToken>();
            totalTokens = tokens.Length;
        }
         
    }

    private void Update()
    {
        // Display the amount of tokens the player has 
        tokenUI.text = $"{player.tokensCollected}/{totalTokens}"; 
    }
}
