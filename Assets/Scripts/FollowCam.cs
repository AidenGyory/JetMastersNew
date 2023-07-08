using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followDamp;
    [SerializeField] bool onEarth;
    
    private void Update()
    {
        if (onEarth)
        {
            float x = 0;
            float y = Mathf.Lerp(transform.position.y, target.position.y, followDamp);
            float z = 0;
            transform.position = new Vector3(x, y, z);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, target.position, followDamp);
        }
        
    }
}
