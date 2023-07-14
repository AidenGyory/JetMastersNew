using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Update()
    {
        MoveToObjective();
        //ObjectiveVisible();
    }

    // Goes as close to the objective without going off the screen
    public void MoveToObjective()
    {
        float dist = Vector3.Distance(transform.position, objective.position);
        //Debug.Log(dist);


        Vector3 viewPos = Camera.main.WorldToViewportPoint(objective.position);
        viewPos.x = Mathf.Clamp(viewPos.x, XminOffset, XmaxOffSet);
        viewPos.y = Mathf.Clamp(viewPos.y, YminOffset, YmaxOffSet);
        transform.position = Camera.main.ViewportToWorldPoint(viewPos);

        if (dist > distanceFromMarker)
        {
            
        }

        // Look at the objective
        transform.up = objective.position - transform.position;
    }

    void ObjectiveVisible()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(objective.position);

        if (viewPos.x > 0 && viewPos.x < 1 && viewPos.y > 0 && viewPos.y < 1 && viewPos.z > 0)
        {
            marker.SetActive(false); // Objective is visible, turn off the marker game object
            
        }
        else
        {
            marker.SetActive(true); // Objective is outside the screen view, turn on the marker game object
            
        }
    }
}
