using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelUnlockScript : MonoBehaviour
{
    [SerializeField] int progressRequiredToUnlock;
    [SerializeField] GameObject locked; 

    void Start()
    {
        if(GameManager.Instance.ReadProgress() >= progressRequiredToUnlock)
        {
            locked.SetActive(false); 
        }
    }
}
