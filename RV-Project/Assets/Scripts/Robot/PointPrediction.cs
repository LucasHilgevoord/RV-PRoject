using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointPrediction : MonoBehaviour {

    [SerializeField]
    float angle;
    [SerializeField]
    int resolution = 10;
    [SerializeField]
    float velocity;

    float gravity;
    float radianAngle;
    [SerializeField]
    Transform firePoint;
    private LineRenderer lr;

    void Start(){
        lr = firePoint.GetComponent<LineRenderer>();
        //https://en.wikipedia.org/wiki/Projectile_motion

        gravity = Mathf.Abs(Physics.gravity.y);
        RenderArc();
    }
    //Line Rendering
    void RenderArc()
    {
        lr.SetVertexCount(resolution + 1);
        lr.SetPositions(CalculateArcArray());
    }

    Vector3[] CalculateArcArray()
    {
        Vector3[] arcArray = new Vector3[resolution + 1];
        radianAngle = Mathf.Deg2Rad * angle;
        float maxDistance = (velocity * velocity * Mathf.Sin(2 * radianAngle)) / gravity;

        for (int i = 0; i <= resolution; i++)
        {
            float t = (float)i / (float)resolution;
            arcArray[i] = CalculateArcPoint(t, maxDistance);
        }

        return arcArray;
    }

    Vector3 CalculateArcPoint(float t, float maxDistance)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(radianAngle) - ((gravity * x * x) / (2 * velocity * velocity * Mathf.Cos(radianAngle) * Mathf.Cos(radianAngle)));
        return new Vector3(x, y);
    }
}
