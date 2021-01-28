using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;
    private bool sprinting;
    public float sprintTimer = 2;
    public GameObject trail;
    private bool sprintOnCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        
            //Sprinting option- whenever the player holds down left shift double speed, if it is not on cooldown
            if (Input.GetKey(KeyCode.LeftShift) && !sprinting && !sprintOnCooldown && sprintTimer > 0 )
            {
                speed = speed * 2;
                sprinting = true;
                
            }
            
            //the sprint lasts about 2 seconds before it is put on cooldown, calculates the amount of time remaining
            if(sprinting)
            {
                sprintTimer -= 0.05f;
                trail.SetActive(true);
            }
            
            //refreshes the sprint constantly while the player is not actively sprinting
            if(!sprinting)
            {
                sprintTimer += 0.01f;
                trail.SetActive(false);
            }

            //resets the player speed back to normal when they are not sprinting
            if (!Input.GetKey(KeyCode.LeftShift) && sprinting)
            {
               
                speed = speed / 2;
                sprinting = false;
            }

            //doesnt let the amount of time the player can sprint go below zero
            if (sprintTimer < 0)
                sprintTimer = 0;

            //places sprint on cooldown if it ever reaches zero
            if (sprintTimer == 0)
            {
                sprintOnCooldown = true;
                sprinting = false;
                speed = speed / 2;
            }

            //when the sprint timer fills back up allows the player to sprint again
            if (sprintTimer > 2)
            {
                sprintTimer = 2;
                sprintOnCooldown = false;
            }

        //set bottom limit of speed, to average speed (3)
        if (speed < 3)
            speed = 3;
        

        float mV = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, mV * speed);
    }
}
