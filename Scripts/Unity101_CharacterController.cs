using UnityEngine;
using TMPro;

public class Unity101_CharacterController : MonoBehaviour
{
    [Header("Walk / Run Setting")] public float walkSpeed;
    public float runSpeed;

    [Header("Jump Force")] public float playerJumpForce;

    [Header("Double Jump")] public bool doubleJumpEnabled;


    private Collider col;
    private Rigidbody rb;
    private Animator anim;
    private float animSpeed;

    private float distToGround;
    private bool playerIsJumping;
    private float currentSpeed;
    private float xAxis;
    private float zAxis;
    private bool leftShiftPressed;
    private int jumpCounter = 0;
    private float jumpDelay = 0.05f;
    private float timer = 0f;
    private bool jumpingHighSpeed;
    [SerializeField]
     private float jumpBoost;

    public static int count;
    public TextMeshProUGUI score;
    public AudioSource collectibleAudio;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider>();
        if(col == null) { Debug.LogError("Collision component missing"); enabled = false; }
        rb = GetComponent<Rigidbody>();
        if(rb == null) { Debug.LogError("Physic body component missing"); enabled = false; }

        // To assert character doesn't fall on the side
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        distToGround = col.bounds.extents.y;
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("no animator");
            enabled = false;
        }
        count = 0;
        SetCountText();
    }

    public void SetCountText()
    {
        score.text = "Score: " + count.ToString() + "/5";
    }

    // Update is called once per frame
    void Update()
    {
        // Walk
        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");
        currentSpeed = (leftShiftPressed && !playerIsJumping) || jumpingHighSpeed ? runSpeed : walkSpeed;

        if(xAxis != 0 || zAxis != 0) //moving forward or backward
        {
            if (zAxis > 0)
            {
                anim.SetFloat("Direction", 1f); //forward
            }
            else
            {
                anim.SetFloat("Direction", -1f); //backward
            }
            animSpeed = (currentSpeed == walkSpeed) ? 0.5f : 1f; //check the speed
        }
        else //is on idle
        {
            anim.SetFloat("Direction", 1f);
            animSpeed = 0f;

        }
        anim.SetFloat("Speed",animSpeed);
       

        // Run
        leftShiftPressed = Input.GetKey(KeyCode.LeftShift);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (playerIsJumping)
        {
            timer += Time.deltaTime;
        }

        if(IsGrounded() && playerIsJumping && timer > jumpDelay)
        {
            playerIsJumping = false;
            jumpCounter = 0;
            timer = 0f;
            jumpingHighSpeed = false;
        }
    }

    // Fixed Update is called once per frame, at fixed time
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + Time.deltaTime * currentSpeed * transform.TransformDirection(xAxis, 0f, zAxis));
    }


    // Check the distance between the player and a ground surface
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.05f);
    }

    private void Jump()
    {
        if (currentSpeed == runSpeed)
        {
            jumpingHighSpeed = true;
        }
        //simple jump
        if (IsGrounded() && !playerIsJumping && jumpCounter < 1)
        {
            rb.velocity = Vector3.up * playerJumpForce * jumpBoost;
            anim.SetTrigger("isJumping");
            jumpCounter++;
            playerIsJumping = true;
        }
        //double jump
        else if (playerIsJumping && (doubleJumpEnabled && jumpCounter < 2))
        {
            rb.velocity = Vector3.up * playerJumpForce;
            jumpCounter++;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
            collectibleAudio.Play();
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            anim.SetTrigger("isWinning");
        }
        
    }



}
