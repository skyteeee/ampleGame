using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class Enemy : MonoBehaviour
{

    public int health = 100;
    public float bounceForce = 20f;
    public int minJumpTime = 1000;
    public float jumpForceMultiplier = 2f;
    public int maxJumpTime = 3000;
    private int currentJumpInterval;
    public int damage = 1;
    private Rigidbody2D rb;
    private Random rnd = new Random();
    private int jumpTime = 0;
    public GameObject player;
    public GameObject enemySpawner;
    private bool isJumping = false;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        minJumpTime = minJumpTime * 50 / 1000;
        maxJumpTime = maxJumpTime * 50 / 1000;
        currentJumpInterval = rnd.Next(minJumpTime, maxJumpTime);
    }

    private void FixedUpdate()
    {
        jumpTime += 1;
        Movement();
    }


    private void Movement()
    {
        RaycastHit2D leftRay = Physics2D.Raycast(transform.position, new Vector2(-5, 1));
        if (leftRay.collider != null && leftRay.collider.tag == "Platform")
        {
            BounceOff(Vector2.left, 1);
        }

        CheckAndAttack();
    }

    private void CheckAndAttack ()
    {
        if (jumpTime % currentJumpInterval == 0)
        {
            BounceOff(player.transform.position, 1);
            jumpTime = 0;
            currentJumpInterval = rnd.Next(minJumpTime, maxJumpTime);
        }
    }


    public void TakeDamage(int dmg, Vector2 hitPosition)
    {
        health -= dmg;

        BounceOff(hitPosition);

        if (health <= 0)
        {
            Die();
        }
    }

    public void BounceOff(Vector2 hitPosition, int offOrTo = -1)
    {

        if (!isJumping)
        {
            int hitDirection = Math.Sign(hitPosition.x - transform.position.x);
            isJumping = true;
            rb.AddForce(new Vector2(bounceForce * hitDirection * offOrTo, bounceForce * jumpForceMultiplier), ForceMode2D.Impulse);
        }
    }

    void Die()
    {
        enemySpawner.GetComponent<EnemySpawner>().allEnemies.Remove(gameObject);
        Destroy(gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(damage);
        }

        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
    }

}
