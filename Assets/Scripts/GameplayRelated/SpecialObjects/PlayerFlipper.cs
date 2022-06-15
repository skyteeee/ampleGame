using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlipper : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.AllowGravityFlip();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.RestrictGravityFlip();
        }
    }

}
