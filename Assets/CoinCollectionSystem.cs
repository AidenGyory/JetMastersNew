using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCollectionSystem : MonoBehaviour
{
    public int totalCoinsInLevel;
    public int coinsCollected;

    [SerializeField] TMP_Text coinUI; 

    void Update()
    {
        coinUI.text = $"{coinsCollected}/{totalCoinsInLevel}"; 
    }

    public void AddCoins(int amount)
    {
        coinsCollected += amount;
    }
}
