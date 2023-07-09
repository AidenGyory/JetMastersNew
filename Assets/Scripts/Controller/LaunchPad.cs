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
}
