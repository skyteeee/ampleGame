using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public int health = 100;
    public HealthBar healthBar;
    public bool healthBarStatus = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (healthBarStatus)
        {
            healthBar.SetHealth(health);
        }
        if (health < 0)
        {
            PlayerGameOver();
        }
    }

    void PlayerGameOver ()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
