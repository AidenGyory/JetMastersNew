using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{
    //This script controls the needle on the spedometer
    //Set a max and min rotation and use the set rotation method to use by giving it a normalized value
    [SerializeField] Transform needleTransform;
    [SerializeField] float maxRotation = 200.0f;
    [SerializeField] float minRotation = 0.0f;

    //Sets the z rotation of the needle depending on a normalized value between 1 and 0
    public void SetRotation(float normalizedValue)
    {
        print(maxRotation * normalizedValue);
        needleTransform.eulerAngles = new Vector3(0,0, -(maxRotation * normalizedValue));
    }
}
