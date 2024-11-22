using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private InitialPlayerData playerData;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private SoundController soundController;

    [Header("Player Animations")]
    [SerializeField] private Animator playerAnim;

    [Header("Player State")]
    public bool grounded = true;
    public bool inAir = false;
    public bool hasTripleJump = false;

    private bool isJumping = false;
    private bool canDoubleJump = false;
    private bool canTripleJump = false;

    [Header("Player References")]
    [SerializeField] private GameObject feet;

    [Header("Timers")]
    public float airTimer;
    private float jumpTimeCounter;

    [Header("Input")]
    public float horizontallInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = transform.Find("Player Sprite").GetComponent<Animator>();
    }

    private void Update()
    {
        Animations();
        Movement();

        if (!grounded)
        {
            airTimer += Time.deltaTime;
            if (airTimer >= 1f)
            {
                inAir = true;
            }
        }
        else
        {
            airTimer = 0f;
            inAir = false;
        }

        // Flip del jugador
        if (horizontallInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontallInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Movement()
    {
        // Horizontal Movement
        horizontallInput = Input.GetAxis("Horizontal");

        // Reduce el control horizontal en el aire
        if (!grounded)
        {
            horizontallInput *= playerData.airControlFactor;
        }

        // Normaliza la velocidad con un MaxSpeed
        float clampedVelocityX = Mathf.Clamp(horizontallInput * playerData.movementSpeed, -playerData.maxSpeed, playerData.maxSpeed);
        rb.velocity = new Vector2(clampedVelocityX, rb.velocity.y);

        // Jump
        if (Input.GetKeyDown(playerData.jumpKey))
        {
            if (grounded)
            {
                Jump();
                canDoubleJump = true;
                soundController.JumpSFX();
                playerAnim.SetTrigger("jump");
            }
            else if (canDoubleJump) // Doble salto
            {
                DoubleJump();
                canDoubleJump = false;
                canTripleJump = hasTripleJump;
                soundController.JumpSFX();
                playerAnim.SetTrigger("jump");
            }

            else if (canTripleJump && hasTripleJump)
            {
                TripleJump();
                canTripleJump = false;
                soundController.JumpSFX();
                playerAnim.SetTrigger("jump");
            }

        }

        // Reduce el salto al soltar la tecla
        if (Input.GetKeyUp(playerData.jumpKey) && isJumping)
        {
            isJumping = false;
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }

        if (Input.GetKeyDown(playerData.healKey) && playerHealth.currentHealth < 4 && playerHealth.currentHealingVials > 0)
        {
            playerHealth.Heal();
        }
    }


    private void Jump()
    {
        grounded = false;
        isJumping = true;
        rb.velocity = new Vector2(rb.velocity.x, playerData.jumpForce);
    }

    private void DoubleJump()
    {
        isJumping = true;
        jumpTimeCounter = playerData.maxJumpTime / 2; //Menos tiempo en el aire para el segundo salto
        rb.velocity = new Vector2(rb.velocity.x, playerData.jumpForce);
    }

    private void TripleJump()
    {
        isJumping = true;
        jumpTimeCounter = playerData.maxJumpTime / 2; //Menos tiempo en el aire para el tercer salto
        rb.velocity = new Vector2(rb.velocity.x, playerData.jumpForce);
    }

    private void Animations()
    {
        playerAnim.SetBool("run", horizontallInput != 0);

        playerAnim.SetBool("grounded", grounded);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            grounded = true;
            canDoubleJump = false;
            canTripleJump = false;
        }
    }

}
