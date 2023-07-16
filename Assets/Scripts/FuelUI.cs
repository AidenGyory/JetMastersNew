using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    private SpaceshipController player;
    [SerializeField] private Image fill;

    private void Start()
    {
        player = FindObjectOfType<SpaceshipController>(); 
    }

    private void Update()
    {
        fill.fillAmount = player.currentFuel / player.maxFuel; 
    }
}
