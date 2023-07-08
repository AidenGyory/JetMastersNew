using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    [SerializeField] GameObject landingPad;
    [SerializeField] bool isLandingPad;

    private void Start()
    {
        //Set up landing pad from start 
        landingPad.SetActive(isLandingPad);
    }

    
    public void TurnOnLandingPad()
    {
        landingPad.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            //Add code for finishing level
            Debug.Log("landed!! "); 
        }
    }
}
