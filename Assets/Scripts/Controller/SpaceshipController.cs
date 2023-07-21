using UnityEngine;
using UnityEngine.UI;

public class SpaceshipController : MonoBehaviour
{
    public int tokensCollected; 
    [SerializeField] private bool useFuel;
    [SerializeField] private bool canRotate; 
    [Header("Fuel Components")]
    public float maxFuel;
    public float currentFuel;
    [Space]
    public float currentThrust;
    public float maxThrust;
    [Space]
    public float velocityRead;
    [Header("Image Components")]
    [SerializeField] private Image spaceShip;
    [SerializeField] private Sprite spaceShipBroken;
    [Header("Effects")]
    [SerializeField] private GameObject forwardThrusterEffect;
    [SerializeField] private GameObject leftThrusterEffect;
    [SerializeField] private GameObject rightThrusterEffect;
    [Header("Thruster Toggles")]
    [SerializeField] private bool thrustingForward;
    [SerializeField] private bool rotateLeft;
    [SerializeField] private bool rotateRight;
    [Header("Rotation Components")]
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float slowdownOnRotate;
    [SerializeField] private AudioClip forwardThrusterSfx;
    [SerializeField] private AudioClip sideThrusterSfx;
    private AudioSource thrusterAudio; 
    
    // Private References 
    private bool canMove;
    private Rigidbody2D rb;
    private Speedometer speedometer; 
    
    private void Start()
    {
        // Get reference to RigidBody
        rb = GetComponent<Rigidbody2D>(); 
        //Get reference to Speedometer 
        speedometer = FindObjectOfType<Speedometer>();
        // Set Fuel to maxFuel
        currentFuel = maxFuel; 
        //Reference to Audio
        thrusterAudio = GetComponent<AudioSource>(); 

    }

    private void Update()
    {
        // Run Update for Input if Spaceship Can Move
        if (!canMove) return; 
        
        // Check if the spaceship has fuel
        if (useFuel)
        {
            if (canMove && currentFuel < 1)
            {
                canMove = false;
                Invoke(nameof(GameOver), 1f); 
                return; 
            }
        }
        
        //Takes input from keyboard and UI
        HandleInput();

        //Read Velocity from rigidbody 
        velocityRead = rb.velocity.magnitude;
        speedometer.SetRotation(velocityRead);


        //Reduce thrust
        if (currentThrust > 1f && !thrustingForward)
        {
            currentThrust = 0.1f;
        }

        if (currentThrust < 0) { currentThrust = 0.1f; }
    }


    private void FixedUpdate()
    {
        //Handles forward thrust
        ThrustForward();
        
        //Check if spaceship can Rotate
        if(!canRotate) return; 
        
        //Add Torque to rotate spaceship
        if (rotateLeft)
        {
            rb.AddTorque(-rotateSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            currentThrust -= Time.deltaTime * slowdownOnRotate;
            //Reduce Fuel
            if(useFuel)
            {
                if(currentFuel > 0)
                {
                    currentFuel -= Time.deltaTime; 
                }
            }
        }
        if (rotateRight)
        {
            rb.AddTorque(rotateSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            currentThrust -= Time.deltaTime * slowdownOnRotate;
            //Reduce Fuel
            if(useFuel)
            {
                if(currentFuel > 0)
                {
                    currentFuel -= Time.deltaTime; 
                }
            }
        }
        
        // Reduce Fuel
        if(thrustingForward && useFuel)
        {
            if(currentFuel > 0)
            {
                currentFuel -= Time.deltaTime; 
            }
        }
        
        
    }

    private void HandleInput()
    {
        // Forward Thruster Toggle
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClickLaunch(true);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            ClickLaunch(false);
        }

        // Rotate Thrusters Toggles 
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            ClickLeft(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            ClickLeft(false);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            ClickRight(true);

        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            ClickRight(false);
        }

    }

    //Moves the ship in the forward direction using acceleration to push it forward with force
    //the acceleration is added to the current thrust to get the launching effect
    private void ThrustForward()
    {
        if (!canMove) return; 
        
        
        //Handle Acceleration
        currentThrust += currentThrust;

        // cap Thrust 
        if (currentThrust > maxThrust)
        {
            currentThrust = maxThrust;
        }

        rb.AddForce(transform.up * (currentThrust * Time.fixedDeltaTime), ForceMode2D.Force);
    }

    //These methods allow UI to control the ship
    public void ClickLaunch(bool clicked)
    {
        thrustingForward = clicked;
        forwardThrusterEffect.SetActive(clicked);
        
        //SFX
        if (GameManager.Instance.sound)
        {
            if (clicked)
            {
                thrusterAudio.clip = forwardThrusterSfx;
                thrusterAudio.Play();
            }
            else
            {
                thrusterAudio.Stop();
            }
        }
        
    }

    public void ClickLeft(bool clicked)
    {
        rotateRight = clicked;
        rightThrusterEffect.SetActive(clicked);
        
        if (GameManager.Instance.sound)
        {
            if (clicked)
            {
                thrusterAudio.clip = sideThrusterSfx;
                thrusterAudio.Play();
            }
            else
            {
                thrusterAudio.Stop();
            }
        }
    }

    public void ClickRight(bool clicked)
    {
        rotateLeft = clicked;
        leftThrusterEffect.SetActive(clicked);

        if (GameManager.Instance.sound)
        {
            if (clicked)
            {
                thrusterAudio.clip = sideThrusterSfx;
                thrusterAudio.Play();
            }
            else
            {
                thrusterAudio.Stop();
            }
        }
        
    }

    public void SpaceshipBroken()
    {
        canMove = false;
        ClickLaunch(false);
        ClickLeft(false);
        ClickRight(false);
        spaceShip.sprite = spaceShipBroken;
        Invoke(nameof(GameOver), 1f); 
    }

    public void ToggleActive(bool active)
    {
        canMove = active;
        if (rb != null)
        {
            rb.simulated = active;
        }
    }

    public void GameOver()
    {
        Debug.LogAssertion("Spaceship has lost power!!");
        rb.simulated = false;
        LevelManager.Instance.LoseState();
    }

    public void RechargeFuel()
    {
        currentFuel = maxFuel; 
    }
}
