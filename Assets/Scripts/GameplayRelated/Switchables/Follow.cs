using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : Switchable
{

    public float posX;
    public float posY;
    //public float posZ;
    public float rotX;
    public float rotY;
    public float rotZ;
    public bool doFollow = true;
    public Transform followTarget;

    public override void TurnOn()
    {
        doFollow = true;
    }

    public override void TurnOff()
    {
        doFollow = false;
    }


    private void FixedUpdate()
    {

        if (doFollow)
        {
            transform.Translate( new Vector3(
                (followTarget.position.x - transform.position.x) * posX,
                (followTarget.position.y - transform.position.y) * posY,
                0f));
            /*transform.Rotate(new Vector3(
                (followTarget.rotation.x - transform.rotation.x) * rotX,
                (followTarget.rotation.y - transform.rotation.y) * rotY,
                (followTarget.rotation.z - transform.rotation.z) * rotZ), Space.World);
*/
            transform.RotateAround(followTarget.position, Vector3.right, (followTarget.rotation.x - transform.rotation.x) * rotX);
            transform.RotateAround(followTarget.position, Vector3.up, (followTarget.rotation.y - transform.rotation.y) * rotY);
            transform.RotateAround(followTarget.position, Vector3.forward, (followTarget.rotation.z - transform.rotation.z) * rotZ);
        }
    }

}
