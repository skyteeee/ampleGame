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
    public bool allowTpBomb = true;
    private int currentAirJumpAmount = 1;
    private TpBomb tpBomb;
    public GameObject tpBombPrefab;
    private float targetGravity;
    public float gravitySwitchSpeed = 0.1f;
    private bool isUpsideDown = false;
    private bool isGravityFlipAllowed = false;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        targetGravity = rb2D.gravityScale;
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

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            TpToBomb();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ThrowTpBomb();
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
        {
            FlipGravity();
        }
        
    }

    public void AllowGravityFlip()
    {
        isGravityFlipAllowed = true;
    }

    public void RestrictGravityFlip()
    {
        isGravityFlipAllowed = false;
    }

    public void FlipGravity()
    {
        if (isGravityFlipAllowed)
        {
            transform.Rotate(new Vector3(0, 180, 180));
            targetGravity *= -1;
            isUpsideDown = isUpsideDown ? false : true;
            RestrictGravityFlip();
        }
    }

    public void ForceFlipGravity()
    {
        
        transform.Rotate(new Vector3(0, 180, 180));
        targetGravity *= -1;
        isUpsideDown = isUpsideDown ? false : true;
        
    }

    void FixedUpdate()
    {
        

        //move
        if (verticalMovement > 0f && !isJumping)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
            rb2D.AddForce(new Vector2(0f, verticalMovement * jumpForce * (isUpsideDown ? -1 : 1)), ForceMode2D.Impulse);
            isJumping = true;
        }


        //jump
        if (horizontalMovement > 0.1f || horizontalMovement < -0.1f)
        {
            rb2D.AddForce(new Vector2(horizontalMovement * movementSpeed, 0f), ForceMode2D.Force);
            Flip(horizontalMovement);
        }


        //gravity
        rb2D.gravityScale = Mathf.Lerp(rb2D.gravityScale, targetGravity, gravitySwitchSpeed);
    }

    private void ThrowTpBomb()
    {
        if (allowTpBomb)
        {
            if (tpBomb != null)
            {
                tpBomb.Die();
            }

            GameObject bomb = Instantiate(tpBombPrefab, transform.position, transform.rotation);
            tpBomb = bomb.GetComponent<TpBomb>();
            tpBomb.Launch();
        }
    }

    private void TpToBomb ()
    {
        if (tpBomb != null)
        {
            tpBomb.Pull(gameObject, 2);
        }
    }    

    private void airJump ()
    {
        if (currentAirJumpAmount > 0 && isJumping)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0f);
            rb2D.AddForce(new Vector2(0, jumpForce * (isUpsideDown ? -1 : 1)), ForceMode2D.Impulse);
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
