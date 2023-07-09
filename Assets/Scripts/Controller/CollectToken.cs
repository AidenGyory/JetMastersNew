using UnityEngine;
using UnityEngine.Events;

public class CollectToken : MonoBehaviour
{
    [SerializeField] UnityEvent OnTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnTrigger?.Invoke();
        }
    }

}
