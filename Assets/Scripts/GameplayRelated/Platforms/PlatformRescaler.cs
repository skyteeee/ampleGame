using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRescaler : MonoBehaviour
{

    public float thickness = 0.2f;
    public Transform innerBody;

    void Start()
    {
        innerBody.localScale = new Vector3(
            (transform.localScale.x - thickness) / transform.localScale.x, 
            (transform.localScale.y - thickness) / transform.localScale.y,
            innerBody.localScale.z);
    }
}
