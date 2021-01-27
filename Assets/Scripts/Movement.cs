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
        
            //Sprinting option
            if (Input.GetKey(KeyCode.LeftShift) && !sprinting && !sprintOnCooldown && sprintTimer > 0 )
            {
                speed = speed * 2;
                sprinting = true;
                
            }
            
            if(sprinting)
            {
                sprintTimer -= 0.05f;
                trail.SetActive(true);
            }
            
            if(!sprinting)
            {
                sprintTimer += 0.01f;
                trail.SetActive(false);
            }


            if (!Input.GetKey(KeyCode.LeftShift) && sprinting)
            {
               
                speed = speed / 2;
                sprinting = false;
            }

            if (sprintTimer < 0)
                sprintTimer = 0;

            if (sprintTimer == 0)
            {
                sprintOnCooldown = true;
                sprinting = false;
                speed = speed / 2;
            }

            if (sprintTimer > 2)
            {
                sprintTimer = 2;
                sprintOnCooldown = false;
            }

        if (speed < 3)
            speed = 3;
        

        float mV = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, mV * speed);
    }
}
