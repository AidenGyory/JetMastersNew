using UnityEngine;

public class AsteroidScript : MonoBehaviour
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
            Debug.Log("" + other.GetComponent<SpaceshipController>().velocityRead);
            if (other.GetComponent<SpaceshipController>().velocityRead > 50)
            {
                other.GetComponent<SpaceshipController>().SpaceshipBroken();
                if (GameManager.Instance.sound)
                {
                    audio.PlayOneShot(crashSfx);
                }
            }
        }
    }
}
