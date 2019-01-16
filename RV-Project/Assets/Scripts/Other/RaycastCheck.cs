using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCheck : MonoBehaviour {

    public LayerMask mask;

    private float theDistance = 5.0f;
    Vector3 orientation;

    public RaycastHit2D hit;


    // Update is called once per frame
    public Transform RaycastChecker()
    {
        Vector3 z = Vector3.forward * theDistance;
        if (z != null)
            orientation = z;
        hit = Physics2D.Raycast(transform.position, orientation, theDistance, mask);
        Debug.DrawRay(transform.position, orientation, Color.green);

        if (hit.collider != null)
            return hit.transform;//.tag;

        return null; //"no hit";
    }
}
