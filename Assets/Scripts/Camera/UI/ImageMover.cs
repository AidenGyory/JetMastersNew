using UnityEngine;

public class ImageMover : MonoBehaviour
{
    public float moveSpeed = 2f; // Adjust the speed of movement as needed
    public float movementRange = 2f; // Adjust the range of movement as needed
    private RectTransform rectTransform;
    private Vector3 startPosition;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.localPosition;
    }

    private void Update()
    {
        // Calculate the vertical movement based on time and speed
        float verticalMovement = Mathf.Sin(Time.time * moveSpeed) * movementRange;
        Vector3 newPosition = startPosition + new Vector3(0f, verticalMovement, 0f);

        // Update the image's position
        rectTransform.localPosition = newPosition;
    }
}
