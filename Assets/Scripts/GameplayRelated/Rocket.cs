using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;
using System.Linq;

public class Rocket : MonoBehaviour
{

    public Transform target;
    public GameObject enemyManager;
    public PlayerRocket playerRocket;
    public int damage = 110;
    public int speed = 10;
    public float correctionSpeed = 0.01f;
    private Vector2 desiredVector;
    private Vector2 currentVector = new Vector2 (0f, 1f);
    public Rigidbody2D rb;
    private Random rnd = new Random();
    private bool isOn = true;
    public ParticleSystem particleSys;
    public GameObject child;
    public GameObject deathPrefab;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = currentVector * speed;
    }

    private void SelectNewTarget()
    {
        List<GameObject> enemies = enemyManager.GetComponent<EnemySpawner>().allEnemies;
        target = enemies[rnd.Next(enemies.Count())].transform;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isOn) 
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage, transform.position);
                playerRocket.RestoreShot();
            }

            if (collider.tag != "Player" && collider.tag != "Projectile")
            {
                Die();
            }

        }
        
    }

    private void Die()
    {
        Destroy(gameObject, 2f);
        Destroy(child);
        gameObject.GetComponent<Renderer>().enabled = false;
        isOn = false;
        particleSys.Stop();
        Instantiate(deathPrefab, transform.position, transform.rotation);
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            desiredVector = Utils.NormalizeVector(target.position - transform.position);
            if (desiredVector.x != currentVector.x && desiredVector.y != currentVector.y)
            {
                currentVector = Vector2.Lerp(currentVector, desiredVector, correctionSpeed);
                rb.velocity = new Vector2 (currentVector.x * speed, currentVector.y * speed);
            }

            float angle = Utils.GetPointing(Vector2.zero, currentVector);
            angle = (360 + angle) % 360;
            transform.Rotate(0f, 0f, angle - transform.rotation.eulerAngles.z, Space.World);
        } else
        {
            if (enemyManager.GetComponent<EnemySpawner>().allEnemies.Count() > 0)
            {
                SelectNewTarget();
            }
        }
        
    }

    

}
