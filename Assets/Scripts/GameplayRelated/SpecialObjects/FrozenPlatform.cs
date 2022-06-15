using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenPlatform : MonoBehaviour
{

    public Rigidbody2D rb;
    public int hitAmount = 1;
    private int hitTimes = 0;
    public float forceMult = 10f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            hitTimes++;
            if (hitTimes >= hitAmount)
            { 
                Unfreeze();
            }
        }
    }

    public void Unfreeze()
    {

        if (rb.bodyType == RigidbodyType2D.Static)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        } else
        {
            rb.AddForce(Vector2.up * forceMult, ForceMode2D.Impulse);
        }
        


    }
}
