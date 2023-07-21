using UnityEngine;
using UnityEngine.Events;

public class CollectToken : MonoBehaviour
{
    [SerializeField] private UnityEvent onTrigger;
    [SerializeField] private AudioClip getToken;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.sound)
            {
                GameManager.Instance.GetComponent<AudioSource>().PlayOneShot(GameManager.Instance.coinCollectSfx); 
            }
            other.GetComponent<SpaceshipController>().tokensCollected++;
            if (GameManager.Instance.sound)
            {
                audio.PlayOneShot(getToken);
            }
            onTrigger?.Invoke(); 
            LevelManager.Instance.CheckStarCritera();
            Destroy(gameObject);
            
            
        }
    }

}
