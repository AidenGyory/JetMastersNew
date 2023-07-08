using UnityEngine;

public class ImageSpinner : MonoBehaviour
{
    public float rotationSpeed = 50f; // Adjust the speed of rotation as needed
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Rotate the image around its center based on the rotation speed
        rectTransform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}