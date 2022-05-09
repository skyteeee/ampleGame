using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb2D;

    private bool isJumping;
    public float jumpForce = 400f;
    public float movementSpeed = 3f;
    private float horizontalMovement;
    private float verticalMovement;
    private float facing = 1f;
    public float teleportMagnitude = 2f;
    public float airJumpFactor = 30f;
    public int maxAirJumpAmount = 1;
    private int currentAirJumpAmount = 1;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            airJump();
        }

        
    }

    void FixedUpdate()
    {
        
        if (verticalMovement > 0f && !isJumping)
        {
            rb2D.AddForce(new Vector2(0f, verticalMovement * jumpForce), ForceMode2D.Impulse);
            isJumping = true;
        }

        if (horizontalMovement > 0.1f || horizontalMovement < -0.1f)
        {
            rb2D.AddForce(new Vector2(horizontalMovement * movementSpeed, 0f), ForceMode2D.Force);
            Flip(horizontalMovement);
        }

    }

    

    private void airJump ()
    {
        if (currentAirJumpAmount > 0 && isJumping)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
            rb2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            currentAirJumpAmount--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
            currentAirJumpAmount = maxAirJumpAmount;
        }
    }


    private void Flip(float direction)
    {
        if ((direction < -0.1f && facing > 0.1f) || (direction > 0.1f && facing < 0.1f))
        {
            transform.Rotate(0f, 180f, 0f);
            facing = direction;
        }
    }
}
