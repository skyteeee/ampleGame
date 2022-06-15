using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowRotate : MonoBehaviour
{
    public float dpsSpeed;
    private float uSpeed;

    private void Start()
    {
        uSpeed = dpsSpeed / 50;
    }


    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, 0, uSpeed));
        
    }
}
