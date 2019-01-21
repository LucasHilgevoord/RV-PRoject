﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Windows.Kinect;
using Joint = Windows.Kinect.Joint;

public class BodySourceView : MonoBehaviour {
    public BodySourceManager mBodySourceManager;
    public GameObject mJointObject;
    public GameObject handPos;

    //public GameObject temp;
    
    [SerializeField]
    private GameObject obMove;
    ObjectMove objectMoveScript;

    public float turnspeed = 200f;

    private float lastPos = 0f;
    private float curPos = 0f;


    private Dictionary<ulong, GameObject> mBodies = new Dictionary<ulong, GameObject>();
    private List<JointType> _joints = new List<JointType>
    {
        //JointType.HandLeft,
        JointType.HandRight,
    };

    void Start()
    {
        objectMoveScript = obMove.GetComponent<ObjectMove>();
    }

    void Update() {

        #region Get Kinect data
        Body[] data = mBodySourceManager.GetData();
        if (data == null)
            return;

        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data) {
            if (body == null)
                continue;

            if (body.IsTracked)
            {
                trackedIds.Add(body.TrackingId);
            }
        }
        #endregion

        #region Delete kinect bodies
        List<ulong> knownIds = new List<ulong>(mBodies.Keys);
        foreach (ulong trackingId in knownIds) {
            if (!trackedIds.Contains(trackingId)) {
                //Destroy body object
                //Destroy(mBodies[trackingId]);

                //Remove from list
                mBodies.Remove(trackingId);
            }
        }
        #endregion

        #region Create kinect bodies
        foreach (var body in data) {
            // If no body, skip
            if (body == null)
                continue;

            if (body.IsTracked) {

                // If body isn't tracked, create body
                if (!mBodies.ContainsKey(body.TrackingId))
                    mBodies[body.TrackingId] = CreateBodyObject(body.TrackingId);

                // Update positions
                UpdateBodyObject(body, mBodies[body.TrackingId]);
            }
        }
        #endregion
    }

    private GameObject CreateBodyObject(ulong id) {
        GameObject newJoint;
        GameObject clone;

        // Create body parent
        GameObject body = new GameObject();

        // Create joints
        foreach (JointType joint in _joints) {
            // Create object
            /*
            GameObject newJoint = Instantiate(mJointObject);
            newJoint.name = joint.ToString();

            // Parent to body
            newJoint.transform.parent = body.transform;
            */

            newJoint = mJointObject; //Instantiate(mJointObject);
            clone = newJoint;

            body.name = clone.name; 
            newJoint.name = joint.ToString();

            // Parent to body
            newJoint.transform.parent = body.transform;

        }

        return body;
    }

    private void UpdateBodyObject(Body body, GameObject bodyObject) {

        // Update joints
        foreach (JointType _joint in _joints) {
            // Get new target position
            Joint sourceJoint = body.Joints[_joint];
            Vector3 targetPosition = GetVector3FromJoint(sourceJoint);
            targetPosition.z = handPos.transform.position.z;
            targetPosition.y = handPos.transform.position.y;

            ///----
            float rotation = transform.rotation.z;
            float rotationBorder = 0.45f;
            float xPosHand = mJointObject.transform.position.x;

            //Debug.Log(temp.transform.localPosition.x);

            // Track last position
            if (curPos < lastPos)
            {
                //Debug.Log("left");
                //transform.Rotate(Vector3.forward, turnspeed * Time.deltaTime);
            }
            else if (curPos > lastPos)
            {
                //Debug.Log("right");
                //transform.Rotate(Vector3.back, turnspeed * Time.deltaTime);
            }

            if (objectMoveScript.IsMoving)
            {
                //transform.Rotate(0, 0, xPosHand * turnspeed * Time.deltaTime);
                curPos = obMove.transform.position.x;
            }
            else
            {
                lastPos = obMove.transform.position.x;
            }

            //Debug.Log(curPos);

            if (rotation >= rotationBorder)
            {
                transform.Rotate(Vector3.back, turnspeed * Time.deltaTime);
            }
            else if (rotation <= -rotationBorder)
            {
                transform.Rotate(Vector3.forward, turnspeed * Time.deltaTime);
            }

            // Get joint, set new position
            Transform jointObject = bodyObject.transform.Find(_joint.ToString());
            jointObject.position = targetPosition;
            //jointObject.localRotation = targetRotation.Orientation;
        }
    }

    private Vector3 GetVector3FromJoint(Joint joint) {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }
}
