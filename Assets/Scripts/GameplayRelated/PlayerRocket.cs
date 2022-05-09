using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRocket : MonoBehaviour
{
    public GameObject rocketPrefab;
    public GameObject enemySpawner;
    public HealthBar timeBar;
    public float spitForce = 10f;
    public int rocketInterval = 2000;
    private int time = 0;
    private bool shotAllowed = true;

    private void Start()
    {
        rocketInterval = rocketInterval * 50 / 1000;
        timeBar.SetMaxHealth(rocketInterval);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            createRocket();
        }    
    }

    private void createRocket()
    {
        if (shotAllowed)
        {
            Rocket rocket = Instantiate(rocketPrefab, transform.position, rocketPrefab.transform.rotation).GetComponent<Rocket>();
            rocket.enemyManager = enemySpawner;
            rocket.playerRocket = this;
            shotAllowed = false;
            time = 0;
        }
    }

    public void RestoreShot ()
    {
        time = rocketInterval;
        timeBar.SetHealth(time);
        shotAllowed = true;
    }

    private void FixedUpdate()
    {
        if (!shotAllowed)
        { 
            time++;
            timeBar.SetHealth(time);
            if (time % rocketInterval == 0)
            {
                RestoreShot();
            }
        }
    }
}
