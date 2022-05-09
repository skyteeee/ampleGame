using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telporter : MonoBehaviour
{

    public GameObject targetTeleporter;
    public float tpYOffset = 5f;
    private Telporter targetScript;
    
    // Start is called before the first frame update
    void Start()
    {
        targetScript = targetTeleporter.GetComponent<Telporter>();
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

        float tpYOffset1 = tpYOffset / tpObject.transform.localScale.y + tpYOffset;

        //teleport the object
        if (tpObject.tag != "Rocket")
        {
            rb.velocity = Vector2.zero;
        }

        tpObject.transform.Translate(new Vector3(x, targetCoords.y - coords.y + tpYOffset1, 0), Space.World);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
