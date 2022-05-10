using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killa : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Die();
        }
    }
}
