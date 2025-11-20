using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Platforming : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpPower = 5f;
    private Rigidbody2D rb;
    private Transform player;
    private Transform sprite;
    private Vector2 getGoin; //Read this in Demoman's voice
    private float schmoove; //float for new Input system
    private bool grounded;
    private bool specialJump;
    private Vector2 spawn;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<Transform>();
        sprite = transform.Find("Sprite");
        spawn = player.position;
    }

    void Update()
    {
        //Horizontal Movement
        getGoin = new Vector2(schmoove * speed, rb.linearVelocity.y);
        
        //Jump and Special Jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        } else if (Input.GetKeyDown(KeyCode.Space) && specialJump)
        {
            SpecialJump();
        }

        //Reset button
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = getGoin;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            Vector2 normal = collision.contacts[0].normal;
            if (normal == Vector2.up)
            {
                grounded = true;
                specialJump = true;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (rb.linearVelocity.y != 0)
        {
            grounded = false;
        }
    }

    void SpecialJump()
    {
        /* Special jump power is inversely proportional to upwards velocity. 
         * It starts at 0% directly as you jump, and increases to 100% once you stop going up.
         */
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocityY = 0f;
        }
        rb.AddForce(Vector2.up * (jumpPower - rb.linearVelocityY), ForceMode2D.Impulse);
        specialJump = false;
    }

    void Reset()
    {
        player.position = spawn;
        rb.linearVelocity = Vector2.zero;
    }

    public void Move(InputAction.CallbackContext context)
    {
        schmoove = context.ReadValue<Vector2>().x;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 100), "Version:\nAlpha 0.13\n\nCurrent Objective:Interactions and Flavor Text\nNeed: Fix detection for talkable objects and better input system");
        GUI.Label(new Rect(10, 100, 300, 100), "Controls:\nR to Reset\nE to Interact\nA and D to Move\nSpace to Jump\nJump Midair to Special Jump");
        GUI.Label(new Rect(10, 200, 300, 100), "Grounded:" + grounded + "\nSpecialJump:" + specialJump);
    }
}
