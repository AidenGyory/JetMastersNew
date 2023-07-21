using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset; 
    [SerializeField] float followDamp;
    
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, followDamp); 
    }
}
