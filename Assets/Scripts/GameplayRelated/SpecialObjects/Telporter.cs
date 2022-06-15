using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telporter : MonoBehaviour
{

    public GameObject targetTeleporter;
    public float tpYOffset = 5f;
    private Telporter targetScript;
    public bool switchGravity = false;
    public Transform tpLandPoint;
    private bool allowPlayer = true;
    public float playerTpTimeout = 0.5f;
    private float timeAfterFreeze = 0f;
    
    void Start()
    {
        targetScript = targetTeleporter.GetComponent<Telporter>();
        playerTpTimeout *= 50;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Projectile")
        {
            Teleport(collision.gameObject);
        }
    }

    private void Teleport(GameObject tpObject)
    {
        Vector3 coords = tpObject.transform.position;
        Vector3 targetCoords = targetTeleporter.transform.position;
        float x = targetCoords.x - coords.x;
        Rigidbody2D rb = tpObject.GetComponent<Rigidbody2D>();
        PlayerMovement player = tpObject.GetComponent<PlayerMovement>();
        //float tpYOffset1 = tpYOffset / tpObject.transform.localScale.y + tpYOffset;

        //teleport the object

        if (!allowPlayer && player != null)
        {
            return;
        }

        if (tpObject.tag != "Rocket")
        {
            rb.velocity = Vector2.zero;
        }

        tpObject.transform.Translate(new Vector3(x, targetCoords.y - coords.y + (-targetCoords.y + targetScript.tpLandPoint.position.y), 0), Space.World);

        

        if (player != null && switchGravity)
        {
            StartPlayerTimeout();
            player.ForceFlipGravity();
        }

    }

    private void StartPlayerTimeout()
    {
        allowPlayer = false;
        timeAfterFreeze = 0;
    }

    private void StopPlayerTimeout()
    {
        allowPlayer = true;
        timeAfterFreeze = 0;
    }

    void FixedUpdate()
    {
        if (!allowPlayer)
        {
            timeAfterFreeze++;
        }

        if (timeAfterFreeze >= playerTpTimeout)
        {
            StopPlayerTimeout();
        }
    }
}
