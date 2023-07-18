using UnityEngine;
using UnityEngine.Events;

public class CollectToken : MonoBehaviour
{
    [SerializeField] private UnityEvent onTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.sound)
            {
                GameManager.Instance.GetComponent<AudioSource>().PlayOneShot(GameManager.Instance.coinCollectSfx); 
            }
            other.GetComponent<SpaceshipController>().tokensCollected++; 
            onTrigger?.Invoke(); 
            LevelManager.Instance.CheckStarCritera();
            Destroy(gameObject);
            
            
        }
    }

}
