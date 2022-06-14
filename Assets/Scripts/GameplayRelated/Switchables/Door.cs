using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Switchable
{
    private bool isMoving = false;
    public float openSpeed = 1f;
    public float progress = 100;
    private Vector3 startScale;
    private Vector3 startPos;
    public float openLimit = 0;
    public float openDirection = -1;
    public bool openOnce = false;
    void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale;
    }

    public void Open ()
    {
        isMoving = true;
        isOn = true;
        openSpeed = Mathf.Abs(openSpeed);
    }

    public void Close()
    {
        if (!openOnce)
        {
            isMoving = true;
            openSpeed = -Mathf.Abs(openSpeed);
            isOn = false;
        }
        
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            progress -= openSpeed;
            progress = Mathf.Min(Mathf.Max(progress, openLimit), 100);

            if (progress <= openLimit || progress >= 100)
            {
                isMoving = false;
            }

            transform.localScale = new Vector3(startScale.x, startScale.y * progress/100, startScale.z);
            transform.position = new Vector3(startPos.x, startPos.y + (startScale.y - transform.localScale.y) / 2 * openDirection, startPos.z);

        }
    }

    public override void TurnOn()
    {
        Open();
    }

    public override void TurnOff()
    {
        Close();
    }
}
