using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainForwardThruster : MonoBehaviour
{
    //Components
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    //Effects
    [SerializeField] GameObject forwardThrusterEffect;

    [Header("Forward Thrust")]
    //These variables control the ship
    //Using bools so we can use both keyboard input and screen controls
    public bool thrustingForward;
    [Space]
    public float currentThrust;
    [Space]
    public float maxThrust;
    public float velocityRead; 

    private void Update()
    {

        //Read Velocity from rigidbody 
        velocityRead = rb.velocity.magnitude; 

        //Forward Launch key toggles
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClickLaunch();
            
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            UnclickLaunch();
        }

        if(thrustingForward)
        {
            ThrustForward(); 
        }
        //Reduce thurst
        if(currentThrust > 1f && !thrustingForward)
        {
            currentThrust = 0.1f;
        }
    }

    //Moves the ship in the forward direction using acceleration to push it forward with force
    //the acceleration is added to the current thrust to get the launching effect
    public void ThrustForward()
    {
        //Handle Acceleration
        currentThrust += currentThrust * Time.deltaTime * 8;

        // cap Thrust 
        if (currentThrust > maxThrust)
        {
            currentThrust = maxThrust;
        }
        
        rb.AddForce(transform.up * currentThrust * Time.fixedDeltaTime, ForceMode2D.Force);

        
    }

    //The method for the UI input to launch the ship
    public void ClickLaunch()
    {
        thrustingForward = true;
        forwardThrusterEffect.SetActive(thrustingForward);
    }

    //If the launch button is no longer being pressed use this
    public void UnclickLaunch()
    {
        thrustingForward = false;
        forwardThrusterEffect.SetActive(thrustingForward);
    }

    
}
