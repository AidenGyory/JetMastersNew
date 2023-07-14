using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class Speedometer : MonoBehaviour
{
    //This script controls the needle on the spedometer
    //Set a max and min rotation and use the set rotation method to use by giving it a normalized value
    [SerializeField] Transform needleTransform;
    [SerializeField] float maxRotation = 200.0f;
    [SerializeField] float minRotation = 0.0f;
    [SerializeField] float minVelocity = -50;
    [SerializeField] float maxVelocity = 50;
    [Range(0, 1)]
    [SerializeField] float maxFill = 0.65f;
    [SerializeField] Image fill;
    [SerializeField] TMP_Text speedNumber; 

    //Sets the z rotation of the needle depending on a normalized value between 1 and 0
    public void SetRotation(float velocity)
    {
        float t = Mathf.InverseLerp(minVelocity, maxVelocity, velocity);
        
        float rotation = Mathf.Lerp(minRotation, maxRotation, t);
        //print(maxRotation * normalizedValue);
        needleTransform.eulerAngles = new Vector3(0,0, rotation);

        fill.fillAmount = t * maxFill;

        speedNumber.text = ((int)velocity).ToString();

        Debug.Log((int)t); 
    }
}
