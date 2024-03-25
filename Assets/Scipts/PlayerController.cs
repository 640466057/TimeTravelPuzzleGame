using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Rigidbody2D rb;

    [Header("Parameters")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpStrength;
    bool jumping;
    bool grounded;
    [SerializeField] float gravity;
    [SerializeField] float jumpGravity;

    [Header("Input")]
    public KeyCode timeTravel;
    public State timeTravelMode;
    public enum State
    {
        box,
        together
    }
    [SerializeField] private KeyCode changeMode;
    [SerializeField] private KeyCode reset;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name.Substring(gameObject.name.Length - 4, 4) == "Echo") //Checks for if the script belongs to an "Echo" and if so, disables it
        {
            gameObject.TryGetComponent(out PlayerController PC);
            PC.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //reset
        if (Input.GetKeyDown(reset))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        //Time Travel Mode
        if (Input.GetKeyDown(changeMode))
        {
            if (timeTravelMode == State.box)
                timeTravelMode = State.together;
            else if (timeTravelMode == State.together)
                timeTravelMode = State.box;
        }
        
        //Time Travel
        if (Input.GetKeyDown(timeTravel) && gameObject.TryGetComponent(out TimeMachine TM) && timeTravelMode == State.together)
        {
                TM.TimeTravel();
        }
        // Player Controlls
        //Downward raycast to check if the player is on the ground
        RaycastHit2D[] hit = new RaycastHit2D[1];
        rb.Cast(Vector2.down, hit, 0.015f);
        if (!jumping && hit[0]) // If passed : Player is on ground
        {
            grounded = true;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) // Starts the jumping sequence if 'W' or "Space" is pressed
            {
                jumping = true;
                grounded = false;
                rb.velocity = new Vector2 (rb.velocity.x, jumpStrength);
            }
        }
        else if (rb.velocity.y < 0 || jumping && !(Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))) // Checks if the player have let go of the jump button
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
}
