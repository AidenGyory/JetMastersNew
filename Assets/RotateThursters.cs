using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateThursters : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float rotateSpeed; 
    //Effects
    [SerializeField] GameObject leftThrusterEffect; 
    [SerializeField] GameObject rightThrusterEffect;

    bool rotateLeft; 
    bool rotateRight;
    void Update()
    {
        //Add Torque to rotate spaceship
        if(rotateLeft)
        {
            rb.AddTorque(-rotateSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        if (rotateRight)
        {
            rb.AddTorque(rotateSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }


        //Key toggles 
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ClickLeft(); 
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            UnclickLeft(); 
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            ClickRight();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            UnclickRight(); 
        }

        leftThrusterEffect.SetActive(rotateLeft);
        rightThrusterEffect.SetActive(rotateRight); 
    }

    //Button toggles 
    public void ClickLeft()
    {
        rotateLeft = true; 
    }

    public void UnclickLeft()
    {
        rotateLeft = false;
    }

    public void ClickRight()
    {
        rotateRight = true; 
    }

    public void UnclickRight()
    {
        rotateRight = false;
    }
}
