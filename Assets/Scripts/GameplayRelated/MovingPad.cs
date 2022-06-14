using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPad : Switchable
{

    public float speed = 1;
    public Transform point1;
    public Transform point2;
    public Rigidbody2D rb;
    private int direction = 1;
    private float distance12;


    public override void TurnOff()
    {
        
    }

    public override void TurnOn()
    {
        
    }
    

    private void Start()
    {
        distance12 = Vector2.Distance(point1.position, point2.position);
    }
    private void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(point2.position.x - point1.position.x, point2.position.y - point1.position.y);
        moveVector.Normalize();
        rb.MovePosition(transform.position + (moveVector * speed * direction));

        var dist1 = Vector2.Distance(transform.position, point1.position);
        var dist2 = Vector2.Distance(transform.position, point2.position);
        if (dist1 + dist2 > distance12+0.01)
        {
            direction = dist1 > dist2 ? -1 : 1;
            Debug.Log("updated direction to " + direction);
        }
    }
}
