using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpStrength;
    [SerializeField] bool jumping;
    [SerializeField] bool grounded;
    [SerializeField] float gravity;
    [SerializeField] float jumpGravity;

    [SerializeField] GameObject echo;
    [SerializeField] KeyCode timeTravel;
    [SerializeField] Vector3 previusPos;
    [SerializeField] Vector3 previusVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Time Travel
        echo.transform.position = previusPos;
        if (Input.GetKeyDown(timeTravel))
        {
            TimeTravel();
        }
        StartCoroutine(LoadTimeTravel(10));

        // Player Controlls
        //Downward raycast to check if the player is on the ground
        RaycastHit2D[] hit = new RaycastHit2D[1];
        rb.Cast(Vector2.down, hit, 0.015f);
        if (!jumping && hit[0]) // If passed : Player is on ground
        {
            grounded = true;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) // Starts the jumping sequence if 'W' or "Space" is pressed
            {
                jumping = true;
                grounded = false;
                rb.velocity = new Vector2 (rb.velocity.x, jumpStrength);
            }
        }
        else if (jumping && !(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))) // Checks if the player have let go of the jump button
        {
            jumping = false;
        }

        //gravity scale
        if (!grounded)
        {
            if (jumping)
                rb.gravityScale = jumpGravity;
            else
                rb.gravityScale = gravity;
        }
        
        if (Input.GetAxisRaw("Horizontal") != 0)
            rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
    }

    IEnumerator LoadTimeTravel(int time)
    {
        Vector3 pos = transform.position;
        Vector2 velocity = rb.velocity;

        yield return new WaitForSeconds(time);

        previusPos = pos;
        previusVelocity = velocity;
    }

    private void TimeTravel()
    {
        transform.position = previusPos;
        rb.velocity = previusVelocity;
    }
}
