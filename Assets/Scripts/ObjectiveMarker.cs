using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveMarker : MonoBehaviour
{
    //This class will be used to control a marker that points towards the objective attached to this object
    [SerializeField] Transform objective;
    //Determines how far from borders we would be
    //These are the clamp values
    [Header("Clamp Values: Buffer from Edge of Screen")]
    [SerializeField] float maxOffSet = 0.85f;
    [SerializeField] float minOffset = 0.15f;

    private void Update()
    {
        MoveToObjective();
    }

    //Goes as close to the objective without going off the screen
    public void MoveToObjective()
    {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(objective.position);
        viewPos.x = Mathf.Clamp(viewPos.x, minOffset, maxOffSet);
        viewPos.y = Mathf.Clamp(viewPos.y, minOffset, maxOffSet);
        transform.position = Camera.main.ViewportToWorldPoint(viewPos);

        //Look at the objective
        transform.up = objective.position - transform.position;
    }



}
