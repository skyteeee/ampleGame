using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySensor : MonoBehaviour
{

    public string targetTag = "Platform";
    public bool left;
    public Enemy enemy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == targetTag)
        {

            enemy.BounceOff(left ? Vector2.left : Vector2.right, 1);

        }
    }
}
