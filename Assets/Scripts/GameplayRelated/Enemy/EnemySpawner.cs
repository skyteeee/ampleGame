using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject player;
    public int minSpawnInterval = 5;
    public int maxSpawnInterval = 10;
    public int minX = -10;
    public int maxX = 10;
    public int minY = 10;
    public int maxY = 10;
    public bool isEnabled = true;
    private Random rnd = new Random();
    private int currentInterval;
    private int spawnTime = 0;
    public List<GameObject> allEnemies = new List<GameObject>();


    void Start()
    {
        minSpawnInterval = minSpawnInterval * 50 / 1000;
        maxSpawnInterval = maxSpawnInterval * 50 / 1000;
        currentInterval = rnd.Next(minSpawnInterval, maxSpawnInterval);
    }

    

    void FixedUpdate()
    {
        spawnTime += 1;
        CheckAndSpawn();
    }

    private void CheckAndSpawn ()
    {
        if (spawnTime % currentInterval == 0 && isEnabled)
        {
            SpawnEnemy(new Vector2 (rnd.Next(minX, maxX), rnd.Next(minY, maxY)));
            spawnTime = 0;
            currentInterval = rnd.Next(minSpawnInterval, maxSpawnInterval);
        }
    }

    public void SpawnEnemy (Vector2 position)
    {
        GameObject enemy = Instantiate(enemyPrefab, new Vector2(transform.position.x, transform.position.y) + position, transform.rotation);
        enemy.GetComponent<Enemy>().enemySpawner = gameObject;
        enemy.GetComponent<Enemy>().player = player;
        //enemy.GetComponent<Enemy>().BounceOff(new Vector2(rnd.Next(-1, 1), 0f));
        allEnemies.Add(enemy);
    }
}
