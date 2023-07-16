using UnityEngine;

public class LaunchPadWin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.CheckWinState();
        }
    }
}
