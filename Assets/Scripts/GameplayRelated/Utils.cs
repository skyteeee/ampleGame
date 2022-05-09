using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
