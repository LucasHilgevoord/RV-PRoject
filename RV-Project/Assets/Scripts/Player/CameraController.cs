
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private Transform cameraTarget;
    private float x = 0.0f;
    private float y = 0.0f;

    private int mouseXSpeedMod = 5;
    private int mouseYSpeedMod = 5;

    [SerializeField] private float distance = 3f;
    [SerializeField] private float cameraTargetHeight = 1.0f;

    private void Start() {
        Vector3 Angles = transform.eulerAngles;
        x = Angles.x;
        y = Angles.y;
    }

    private void LateUpdate() {
            x += Input.GetAxis("Mouse X") * mouseXSpeedMod;
            y += Input.GetAxis("Mouse Y") * mouseYSpeedMod;

            y = ClampAngle(y, -15, 25);
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            Vector3 position = cameraTarget.position - (rotation * Vector3.forward * distance);
            Vector3 cameraTargetPosition = new Vector3(cameraTarget.position.x, cameraTarget.position.y + cameraTargetHeight, cameraTarget.position.z);
          
            position = cameraTarget.position - (rotation * Vector3.forward * distance + new Vector3(0, -cameraTargetHeight, 0));

            transform.rotation = rotation;
            transform.position = position;
        
    }

    private static float ClampAngle(float angle, float min, float max) {
        if (angle < -360) {
            angle += 360;
        }
        if (angle > 360) {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
