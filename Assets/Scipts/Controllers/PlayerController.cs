using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector2 debug;
    
    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;

    [Header("Parameters")]
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpStrength;
    bool jumping;
    bool grounded;
    [SerializeField] float gravity;
    [SerializeField] float jumpGravity;

    [Header("Time Travel")]
    public State timeTravelMode;
    public enum State
    {
        box,
        together
    }

    [Header("Stats")]
    public int KeysObtained;

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
        if (Input.GetKeyDown(Settings.keys[2]))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        //Time Travel Mode
        if (Input.GetKeyDown(Settings.keys[0]))
        {
            if (timeTravelMode == State.box)
            {
                anim.SetFloat("Pink", -1);
                timeTravelMode = State.together;
            }
            else if (timeTravelMode == State.together)
            {
                anim.SetFloat("Pink", 1);
                timeTravelMode = State.box;
            }
        }
        
        //Time Travel
        if (Input.GetKeyDown(Settings.keys[1]) && gameObject.TryGetComponent(out TimeMachine TM) && timeTravelMode == State.together && TM.echo.transform.localScale != Vector3.zero) // Checks if pressed the correct key as set in the "settings" script, if it's in the right timetravel mode to travel or if it's just the box, if the "Echo" exists or if the game has not been running for long enough
                TM.TimeTravel();

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
            rb.drag = 1;
        } else
        {
            rb.drag = 5;
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            rb.velocity = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
            transform.localScale = new Vector2(Input.GetAxisRaw("Horizontal"), 1);
        }

        float VelocityY = Mathf.Round(rb.velocity.y * 100) / 100;
        anim.SetFloat("VelocityY", VelocityY == 0 ? 0 : Mathf.Sign(VelocityY));
        anim.SetFloat("Moving", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        debug = new Vector2(VelocityY == 0 ? 0 : Mathf.Sign(VelocityY), Mathf.Abs(Input.GetAxisRaw("Horizontal")));
    }
}