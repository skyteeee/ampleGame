using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int health = 100;
    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if (health < 0)
        {
            PlayerGameOver();
        }
    }

    void PlayerGameOver ()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
