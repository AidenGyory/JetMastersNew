using UnityEngine;

public class BoundaryObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<SpaceshipController>().GameOver();
        }
    }
}
