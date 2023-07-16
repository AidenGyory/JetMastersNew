using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class levelUnlockScript : MonoBehaviour
{
    [SerializeField] int progressRequiredToUnlock;
    [SerializeField] GameObject locked;
    [SerializeField] private GameObject[] starsUI; 
    private LevelInfo info; 

    void Start()
    {
        info = FindObjectOfType<LevelInfo>();
        info.UpdateStars();

        if (GameManager.Instance.ReadProgress() < progressRequiredToUnlock) return;
        
        locked.SetActive(false);
        for (var i = 0; i < info.levelStars[progressRequiredToUnlock]+1; i++)
        {
            starsUI[i].SetActive(true);
        }
    }
}
