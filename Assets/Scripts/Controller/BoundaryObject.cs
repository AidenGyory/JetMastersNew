using UnityEngine;

public class BoundaryObject : MonoBehaviour
{
    [SerializeField] private AudioClip crashSfx;
    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>(); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<SpaceshipController>().GameOver();
            if (GameManager.Instance.sound)
            {
                audio.PlayOneShot(crashSfx);
            }
        }
    }
}
