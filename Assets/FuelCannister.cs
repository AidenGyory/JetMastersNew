using UnityEngine;
using UnityEngine.Events;

public class FuelCannister : MonoBehaviour
{
    [SerializeField] private UnityEvent onTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onTrigger?.Invoke(); 
            Destroy(gameObject);
        }
    }
}
