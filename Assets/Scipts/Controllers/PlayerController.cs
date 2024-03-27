using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector2 debug;
    
    [Header("References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource warpSound;
    [SerializeField] GameObject pauseMenu;

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
        warpSound.volume = Settings.volume;
        pauseMenu.SetActive(false);
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

        //pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.active == true)
            {
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
            }
            else if (pauseMenu.active == false)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }

        //Time Travel Mode
        if (Input.GetKeyDown(Settings.keys[0]))
        {
            if (timeTravelMode == State.box)
            {
                anim.SetFloat("Pink", 0);
                timeTravelMode = State.together;
            }
            else if (timeTravelMode == State.together)
            {
                anim.SetFloat("Pink", 1);
                timeTravelMode = State.box;
            }
        }

        //Time Travel
        if (Input.GetKeyDown(Settings.keys[1]) && TryGetComponent(out TimeMachine TM) && timeTravelMode == State.together && TM.echo.transform.localScale != Vector3.zero) // Checks if pressed the correct key as set in the "settings" script, if it's in the right timetravel mode to travel or if it's just the box, if the "Echo" exists or if the game has not been running for long enough
        {
            warpSound.Play();
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

        float velocityY = Mathf.Round(rb.velocity.y * 100) / 100;
        anim.SetFloat("VelocityY", velocityY == 0 ? 0 : Mathf.Sign(velocityY));
        anim.SetFloat("Moving", Mathf.Abs(Input.GetAxisRaw("Horizontal")));

        if (TryGetComponent(out TimeMachine Tm) && Tm.echo.TryGetComponent(out Animator A))
        {
            float echoVelocityY = Mathf.Round(Tm.previusVelocity.y * 100) / 100;
            A.SetFloat("VelocityY", echoVelocityY == 0 ? 0 : Mathf.Sign(echoVelocityY));
            A.SetFloat("Moving", Mathf.Abs(Tm.previusHorizontalInput));
            debug = new Vector2(echoVelocityY, Mathf.Abs(Tm.previusHorizontalInput));
        }
    }
}