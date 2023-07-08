using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    //Components
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    //[SerializeField] PhysicsInformation physicInformation;
    //[SerializeField] FuelSystem fuelSystem;

    [Header("Movement Paramaters")]
    //Forward movement
    [SerializeField] float accelerationMultiplier = 5.0f;
    [SerializeField] public float maxThrust = 600.0f;
    [SerializeField] public float currentThrust = 0f;

    //Turning movement
    [SerializeField] private float turningPower;
    //what must the player be below when landing
    [SerializeField] int speedLimit;
    [SerializeField] private float fuelUsage;

    //Effects
    [SerializeField] GameObject forwardThrusterEffect;
    [SerializeField] GameObject leftThrusterEffect;
    [SerializeField] GameObject rightThrusterEffect;

    //sprites
    [SerializeField] GameObject mainSprite;
    [SerializeField] GameObject crashedSprite;

    //UI
    [SerializeField] Speedometer speedometer;

    //states
    bool levelOver = false;

    //These variables control the ship
    //Using bools so we can use both keyboard input and screen controls
    public bool thrustingForward = false;
    public bool leftTurn = false;
    public bool rightTurn = false;

    //Controls whether we want the ship to turn as some levels we may want straight movement
    [SerializeField] private bool canTurn = false;

    private void Start()
    {
        //fuelUsage = fuelUsage * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelOver) return;
        //Add thrust on up arrow
        KeyboardInput();

    }

    private void FixedUpdate()
    {
        //Move the ship
        ShipMovement();
    }

    //Moves the ship using the rigidbody
    private void ShipMovement()
    {
        //The parts below is what actually controls the spaceship
        if (thrustingForward)
        {
            ThrustForward();
        }
        else
        {
            forwardThrusterEffect.SetActive(false);
        }

        if (leftTurn && canTurn)
        {
            TurnLeft();
        }
        else
        {
            leftThrusterEffect.SetActive(false);
        }

        if (rightTurn && canTurn)
        {
            TurnRight();
        }
        else
        {
            rightThrusterEffect.SetActive(false);
        }
    }

    //Checks for keyboard input and applies it to the appropriate method
    private void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClickLaunch();
        }
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            UnclickLaunch();
        }

        //turn ship right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rightTurn = true;
        }
        else
        {
            rightTurn = false;
        }

        //turn ship left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            leftTurn = true;
        }
        else
        {
            leftTurn = false;
        }
    }

    //Turns the ship left using the rigidbody
    public void TurnLeft()
    {
        rb.AddTorque(turningPower * Time.fixedDeltaTime, ForceMode2D.Impulse);
        leftThrusterEffect.SetActive(true);

        //use fuel
        //fuelSystem?.UseFuel(fuelUsage);

        
    }

    //Turns the ship right using the rigidbody
    public void TurnRight()
    {
        rb.AddTorque(-turningPower * Time.fixedDeltaTime, ForceMode2D.Impulse);
        rightThrusterEffect.SetActive(true);

        //use fuel
        //fuelSystem?.UseFuel(fuelUsage);
    }

    //Moves the ship in the forward direction using acceleration to push it forward with force
    //the acceleration is added to the current thrust to get the launching effect
    public void ThrustForward()
    {
        currentThrust += accelerationMultiplier;
        rb.AddForce(transform.up * currentThrust * Time.fixedDeltaTime, ForceMode2D.Force);
        forwardThrusterEffect.SetActive(true);

        if(currentThrust > maxThrust) currentThrust = maxThrust;

        //use fuel
        //fuelSystem?.UseFuel(fuelUsage);

        //Set the speedometer
       if(speedometer !=null) speedometer.SetRotation(currentThrust / maxThrust);
    }


    //check asteroids for collision speed
    //If the ship is over the speed limit it crashes
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Asteroid")) //&& physicInformation.speed > speedLimit)
        {
            CrashShip();
        }
    }

    //Called when the ship crashes
    public void CrashShip()
    {
        //crash game over
        //swap the sprites
        mainSprite.SetActive(false);
        crashedSprite.SetActive(true);

        GameOver();
    }

    //Game over state
    //To DO: Move to a game manager with enum for current game state
    public void GameOver()
    {
        forwardThrusterEffect.SetActive(false);
        leftThrusterEffect.SetActive(false);
        rightThrusterEffect.SetActive(false);
        //this.enabled = false;
    }

    //The method for the UI input to launch the ship
    public void ClickLaunch()
    {
        thrustingForward = true;
    }

    //If the launch button is no longer being pressed use this
    public void UnclickLaunch()
    {
        thrustingForward = false;
    }

    
}
