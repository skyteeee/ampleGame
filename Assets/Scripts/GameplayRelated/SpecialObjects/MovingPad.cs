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
    private float aSpeed;
    public bool startOn = true;

    public override void TurnOff()
    {
        speed = 0;
        isOn = false;
    }

    public override void TurnOn()
    {
        speed = aSpeed;
        isOn = true;
    }
    

    private void Start()
    {
        distance12 = Vector2.Distance(point1.localPosition, point2.localPosition);
        aSpeed = speed;
        if (startOn)
        {
            TurnOn();
        } else
        {
            TurnOff();
        }
    }
    private void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(point2.localPosition.x - point1.localPosition.x, point2.localPosition.y - point1.localPosition.y);
        moveVector.Normalize();
        transform.Translate(moveVector * speed * direction);
//        rb.MovePosition(transform.position + (moveVector * speed * direction));

        var dist1 = Vector2.Distance(transform.localPosition, point1.localPosition);
        var dist2 = Vector2.Distance(transform.localPosition, point2.localPosition);
        if (dist1 + dist2 > distance12+0.01)
        {
            direction = dist1 > dist2 ? -1 : 1;
        }
    }
}
