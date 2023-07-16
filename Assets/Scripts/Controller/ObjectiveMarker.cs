using UnityEngine;
using UnityEngine.UI;

public class ObjectiveMarker : MonoBehaviour
{
    
    // This class will be used to control a marker that points towards the objective attached to this object
    [SerializeField] Transform objective;
    // Determines how far from borders we would be
    // These are the clamp values
    [Header("Clamp Values: Buffer from Edge of Screen")]
    [SerializeField] float XmaxOffSet = 0.95f;
    [SerializeField] float XminOffset = 0.05f;
    [SerializeField] float YmaxOffSet = 0.95f;
    [SerializeField] float YminOffset = 0.05f;
    [SerializeField] float distanceFromMarker = 2;
    [SerializeField] GameObject marker;
    [SerializeField] private GameObject icon; 
    private Camera cam;
    private void Start()
    {
        cam = FindObjectOfType<Camera>(); 
    }

    private void Update()
    {
        MoveToObjective();
        CheckMarkerDistance();
    }

    // Goes as close to the objective without going off the screen
    private void MoveToObjective()
    {
        var viewPos = cam.WorldToViewportPoint(objective.position);
        viewPos.x = Mathf.Clamp(viewPos.x, XminOffset, XmaxOffSet);
        viewPos.y = Mathf.Clamp(viewPos.y, YminOffset, YmaxOffSet);
        transform.position = cam.ViewportToWorldPoint(viewPos);
        
        // Look at the objective
        marker.transform.up = Vector3.Lerp(marker.transform.up,objective.position - transform.position, 8f);
    }
    private void CheckMarkerDistance()
    {
        var dist = Vector3.Distance(transform.position, objective.position);
        ToggleMarkerUI(!(dist < distanceFromMarker));
    }
    private void ToggleMarkerUI(bool visibility)
    {
        marker.SetActive(visibility);
        icon.SetActive(visibility);
    }
}
