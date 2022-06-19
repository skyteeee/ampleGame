using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{

    private Vector2 desiredGravity = Vector2.down * 9.81f;
    private float gravitySwitchSmoothness = 1;

    private void FixedUpdate()
    {
        CorrectGravity();
    }

    public static float GetPointing(Vector2 from, Vector2 to)
    {
        float distX = Mathf.Abs(to.x - from.x);
        float distY = to.y - from.y;



        float tan = distY / distX;
        float radian = Mathf.Atan(tan);
        float degreeValue = radian * 180 / Mathf.PI;

        if (to.x < from.x)
        {
            degreeValue = 180 - degreeValue;
        }

        return degreeValue;
    }

    public static Vector2 NormalizeVector(Vector2 vector)
    {
        var z = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
        Vector2 normal = new Vector2(vector.x / z, vector.y / z);
        return normal;
    }

    public void SwitchGravity(Vector2 gravity, float smoothness)
    {
        desiredGravity = gravity;
        gravitySwitchSmoothness = smoothness;
    }

    private void CorrectGravity()
    {

        if (Mathf.Abs(desiredGravity.y - Physics2D.gravity.y) > 0.01 ||
            Mathf.Abs(desiredGravity.x - Physics2D.gravity.x) > 0.01) {
            Physics2D.gravity = new Vector2(
                Mathf.Lerp(Physics2D.gravity.x, desiredGravity.x, gravitySwitchSmoothness),
                Mathf.Lerp(Physics2D.gravity.y, desiredGravity.y, gravitySwitchSmoothness));
        }
    }


}

public enum GravityDirection
{
    left, right, up, down
}


public delegate void Runnable(); 

public class TimeoutCall
{
    private float timeoutDuration;
    private Runnable callback;
    private bool isComplete = false;

    public TimeoutCall(float duration, Runnable callback)
    {
        this.callback = callback;
        timeoutDuration = duration;
    }

    public void FixedUpdate()
    {
        if (!isComplete) {
            timeoutDuration -= 1 / 50f;
            if (timeoutDuration <= 0f)
            {
                callback();
                isComplete = true;
            }
        }
    }

}
