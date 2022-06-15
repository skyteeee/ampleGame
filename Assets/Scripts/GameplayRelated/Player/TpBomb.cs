using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpBomb : MonoBehaviour
{
    public Rigidbody2D rb;

    public void Launch (float magnitude = 10f)
    {
        rb.AddForce(new Vector2(0 , magnitude), ForceMode2D.Impulse);
    }

    public void Pull (GameObject objectToPull, float yOffset = 0f)
    {
        objectToPull.transform.Translate(
            new Vector3(
                -objectToPull.transform.position.x + transform.position.x,
                -objectToPull.transform.position.y + transform.position.y + yOffset,
                0),
            Space.World);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
