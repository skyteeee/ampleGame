using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private bool isMoving = false;
    public float openSpeed = 1f;
    public float closeSpeed = 1f;
    private float moveSpeed;
    public float pressLimit = 10;
    public float progress = 100;
    private Vector3 startScale;
    private Vector3 startPos;
    public Switchable target;

    // Start is called before the first frame update
    void Start()
    { 
        startPos = transform.localPosition;
        startScale = transform.localScale;
    }

   
    void FixedUpdate()
    {
        if (isMoving)
        {
            progress -= moveSpeed;
            progress = Mathf.Min(Mathf.Max(progress, pressLimit), 100);

            if (progress <= pressLimit || progress >= 100)
            {
                isMoving = false;
                if (progress >= 100)
                {
                    Debug.Log("target turned off");
                    target.TurnOff();
                }
            }

            transform.localScale = new Vector3(startScale.x, startScale.y * progress / 100, startScale.z);
            transform.localPosition = new Vector3(startPos.x, startPos.y - (startScale.y - transform.localScale.y) / 2, startPos.z);

        }
    }


    private void StartPress()
    {
        Debug.Log("target turned on");
        target.TurnOn();
        isMoving = true;
        moveSpeed = openSpeed;
    }

    private void StartDepress()
    {
        isMoving = true;
        moveSpeed = -closeSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartPress();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StartDepress();
    }

}
