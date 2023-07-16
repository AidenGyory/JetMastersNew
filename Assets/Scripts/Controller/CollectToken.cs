using UnityEngine;
using UnityEngine.Events;

public class CollectToken : MonoBehaviour
{
    [SerializeField] private UnityEvent onTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<SpaceshipController>().tokensCollected++; 
            onTrigger?.Invoke(); 
            Destroy(gameObject);
            LevelManager.Instance.CheckStarCritera();
        }
    }

}
