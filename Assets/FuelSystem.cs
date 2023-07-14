using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelSystem : MonoBehaviour
{
    [SerializeField] SpaceshipController player;

    [SerializeField] Image fill;

    private void Update()
    {
        fill.fillAmount = player.currentFuel / player.maxFuel; 
    }
}
