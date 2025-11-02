using Unity.Mathematics.Geometry;
using UnityEditor.AssetImporters;
using UnityEngine;

public class Player_Platforming : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float jumpPower = 5f;
    private Rigidbody2D cunt;
    private Transform player;
    private Transform sprite;
    private Vector2 getGoin;
    private bool grounded;
    private bool specialJump;
    private Vector2 spawn;

    void Start()
    {
        cunt = GetComponent<Rigidbody2D>();
        player = GetComponent<Transform>();
        sprite = transform.Find("Sprite");
        spawn = player.position;
    }

    void Update()
    {
        //Horizontal Movement
        getGoin = new Vector2(Input.GetAxis("Horizontal") * speed, cunt.linearVelocity.y);
        
        //Jump and Special Jump
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            cunt.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
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
        cunt.linearVelocity = getGoin;
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
        grounded = false;
    }

    void SpecialJump()
    {
        /* Special jump power is inversely proportional to upwards velocity. 
         * It starts at 0% directly as you jump, and increases to 100% once you stop going up.
         */
        if (cunt.linearVelocity.y < 0)
        {
            cunt.linearVelocityY = 0f;
        }
        cunt.AddForce(Vector2.up * (jumpPower - cunt.linearVelocityY), ForceMode2D.Impulse);
        specialJump = false;
    }

    void Reset()
    {
        player.position = spawn;
        cunt.linearVelocity = Vector2.zero;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 100), "Version:\nAlpha 0.1\n\nCurrent Objective:\nInteractions and Flavor Text");
        GUI.Label(new Rect(10, 100, 300, 100), "Controls:\nR to Reset\nE to Interact\nA and D to Move\nSpace to Jump\nJump Midair to Special Jump");
    }
}
