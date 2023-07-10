using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.LoseState();
        }
    }
}
