using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipController : MonoBehaviour
{
    bool canMove = false;

    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    [Header("Effects")]
    [SerializeField] GameObject forwardThrusterEffect;
    [SerializeField] GameObject leftThrusterEffect;
    [SerializeField] GameObject rightThrusterEffect;
    [SerializeField] Image spaceShip;
    [SerializeField] Sprite spaceShipBroken;
    [Header("Forward Thrust")]
    //These variables control the ship
    //Using bools so we can use both keyboard input and screen controls
    public bool thrustingForward;
    [Space]
    public float currentThrust;
    [Space]
    public float maxThrust;
    public float velocityRead;
    [Header("Rotation")]
    [SerializeField] float rotateSpeed;


    bool rotateLeft;
    bool rotateRight;

    private void Update()
    {
        GetComponent<Speedometer>().SetRotation(currentThrust);
        //Takes input from keyboard and UI
        HandleInput();

        //Read Velocity from rigidbody 
        velocityRead = rb.velocity.magnitude;

        
        //Reduce thurst
        if (currentThrust > 1f && !thrustingForward)
        {
            currentThrust = 0.1f;
        }

        //Add Torque to rotate spaceship
        if (rotateLeft)
        {
            rb.AddTorque(-rotateSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        if (rotateRight)
        {
            rb.AddTorque(rotateSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }

    private void FixedUpdate()
    {
        //Handles forward thrust
        ThrustForward();
    }

    private void HandleInput()
    {
        if (!canMove)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClickLaunch(true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            ClickLaunch(false);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ClickRight(true);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            ClickRight(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ClickLeft(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            ClickLeft(false);
        }

    }

    //Moves the ship in the forward direction using acceleration to push it forward with force
    //the acceleration is added to the current thrust to get the launching effect
    public void ThrustForward()
    {
        //Handle Acceleration
        currentThrust += currentThrust;

        // cap Thrust 
        if (currentThrust > maxThrust)
        {
            currentThrust = maxThrust;
        }

        rb.AddForce(transform.up * currentThrust * Time.fixedDeltaTime, ForceMode2D.Force);
    }

    //These methods allow UI to control the ship
    public void ClickLaunch(bool clicked)
    {
        thrustingForward = clicked;
        forwardThrusterEffect.SetActive(clicked);
    }

    public void ClickLeft(bool clicked)
    {
        rotateRight = clicked;
        rightThrusterEffect.SetActive(clicked);
    }

    public void ClickRight(bool clicked)
    {
        rotateLeft = clicked;
        leftThrusterEffect.SetActive(clicked);
    }

    public void SpaceshipBroken()
    {
        canMove = false;
        spaceShip.sprite = spaceShipBroken;
        //turn rb off
        rb.simulated = false;
    }

    public void ToggleActive(bool active)
    {
        canMove = active;
        rb.simulated = active;
    }
}
