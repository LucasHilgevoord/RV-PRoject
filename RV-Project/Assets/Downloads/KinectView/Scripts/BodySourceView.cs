using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Windows.Kinect;
using Joint = Windows.Kinect.Joint;

public class BodySourceView : MonoBehaviour {
    public BodySourceManager mBodySourceManager;
    public GameObject mJointObject;
    public GameObject handPos;
    public GameObject body;
    public GameObject centerObject;

    //public GameObject temp;
    
    [SerializeField]
    private GameObject obMove;
    ObjectMove objectMoveScript;

    public Vector3 targetPosition;

    public float turnspeed = 100f;

    private float lastPos = 0f;
    private float curPos = 0f;

    ///
    public GameObject centerBody;
    public GameObject handObj;

    private Vector3 RacketPos;
    private Quaternion RacketRot;

    private float dir = 0;
    private float rot = 0;

    [SerializeField]
    private float moveSpeed = 5f;
    [SerializeField]
    private float rotateSpeed = 200f;


    private Dictionary<ulong, GameObject> mBodies = new Dictionary<ulong, GameObject>();
    private List<JointType> _joints = new List<JointType>
    {
        //JointType.HandLeft,
        JointType.HandRight,
    };

    void Start()
    {
        objectMoveScript = obMove.GetComponent<ObjectMove>();
        //targetPosition.z = handPos.transform.position.z;
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

    private void SetParent(Transform hand)
    {
        hand.SetParent(body.transform);
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

    public void UpdateBodyObject(Body body, GameObject bodyObject) {

        // Update joints
        foreach (JointType _joint in _joints) {
            // Get new target position
            Joint sourceJoint = body.Joints[_joint];
            targetPosition = GetVector3FromJoint(sourceJoint);
            
            targetPosition.y = handPos.transform.position.y;
            targetPosition.z = handPos.transform.position.z;

            ///----
            float position = transform.position.x;
            float rotationBorder = 0.45f;
            float xPosHand = mJointObject.transform.position.x;

            //Debug.Log(position);

            //Debug.Log(temp.transform.localPosition.x);

            // Border
            RacketPos = transform.position;
            RacketRot = handObj.transform.rotation;


            // Border left
            if (RacketPos.x <= centerBody.transform.localPosition.x - 0.5f)
            {
                if (dir == -1f)
                {
                    moveSpeed = 0;
                }
                else if (dir == 1f)
                {
                    moveSpeed = 5f;
                }
                Debug.Log("Left");
            }
            // Border right
            else if (RacketPos.x >= centerBody.transform.localPosition.x + 0.5f)
            {
                if (dir == 1f)
                {
                    moveSpeed = 0;
                }
                else if (dir == -1f)
                {
                    moveSpeed = 5f;
                }
                Debug.Log("Right");
            }
            else
            {
                Debug.Log("Middle");
            }


            // Border up
            if (RacketRot.z >= centerBody.transform.rotation.z + 0.5f)
            {
                if (rot == 2f)
                {
                    rotateSpeed = 0;
                }
                else if (rot == -2f)
                {
                    rotateSpeed = 200f;
                }
                Debug.Log("Right");
            }
            // Border down
            else if (RacketRot.z <= centerBody.transform.rotation.z - 0.5f)
            {
                if (rot == -2f)
                {
                    rotateSpeed = 0;
                }
                else if (rot == 2f)
                {
                    rotateSpeed = 200f;
                }
                Debug.Log("Right");
            }


            // Track last position
            if (position < 0)
            {
                //Debug.Log("left");
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 45), turnspeed * Time.time);
                dir = -1f;
            }
            else if (position > 0)
            {
                //Debug.Log("right");
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, -45), turnspeed * Time.time);
                dir = 1f;
            }

            if (objectMoveScript.IsMoving)
            {
                //transform.Rotate(0, 0, xPosHand * turnspeed * Time.deltaTime);
                curPos = mJointObject.transform.localPosition.x;
            }
            else
            {
                lastPos = mJointObject.transform.localPosition.x;
            }

            //Debug.Log(curPos);

            /*
            if (rotation >= rotationBorder)
            {
                transform.Rotate(Vector3.back, turnspeed * Time.deltaTime);
            }
            else if (rotation <= -rotationBorder)
            {
                transform.Rotate(Vector3.forward, turnspeed * Time.deltaTime);
            }
            */

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
