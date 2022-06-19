using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTurner : MonoBehaviour
{

    public GravityDirection exitGravity;
    public GravityDirection trargetGravity;
    public float smoothness = 0.01f;
    public float cooldown = 0.2f;
    private bool didEnter = false;
    private TimeoutCall timer;
    private bool frozen = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerMovement>();
        if (player != null && !frozen)
        {
            
            if (didEnter)
            {
                player.GlobalFlip(exitGravity, smoothness);
                didEnter = false;
            } else
            {
                didEnter = true;
                player.GlobalFlip(trargetGravity, smoothness);
            }

            frozen = true;

            timer = new TimeoutCall(cooldown, () => {
                frozen = false;
                Debug.Log("callback called");
            });
            
        }
    }

    private void FixedUpdate()
    {
        timer?.FixedUpdate();
    }


}
