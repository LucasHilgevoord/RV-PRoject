
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    private Rigidbody _rb;
    private CapsuleCollider _col;
    private Transform _mainCamera;

    [SerializeField] private float _walkSpeed = 2;
    [SerializeField] private float _runSpeed = 5;
    [SerializeField] private float _sneakMultiplier = 0.5f;
    [SerializeField] private float _accelerationSpeed = 50;
    [SerializeField] private float _rotationSpeed = 5;
    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private float _gravity = 1;

    private bool sneaking;
    private bool grounded;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        _mainCamera = Camera.main.transform;
    }

    private void Update() {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        sneaking = Input.GetKey(KeyCode.LeftControl);

        if (horizontal != 0 || vertical != 0) {
            float speed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _walkSpeed;

            if (sneaking) {
                speed *= _sneakMultiplier;
            }

            // Velocity
            Vector3 forward = transform.forward * speed;
            forward.y = _rb.velocity.y;
            _rb.velocity = Vector3.Slerp(_rb.velocity, forward, _accelerationSpeed * Time.deltaTime);

            // Rotation
            Vector3 targetDirection = _mainCamera.forward * vertical + _mainCamera.right * horizontal;
            targetDirection.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        } else { 
            Vector3 zero = Vector3.zero;
            zero.y = _rb.velocity.y;

            _rb.velocity = Vector3.Slerp(_rb.velocity, zero, _accelerationSpeed * Time.deltaTime);
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && grounded && !sneaking) {
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
        //gravity
        _rb.AddForce(-transform.up * _gravity, ForceMode.Acceleration);
    }

    private void FixedUpdate() {
        Vector3 start = new Vector3(transform.position.x, transform.position.y - _col.bounds.size.y / 2 + 0.1f + _col.center.y, transform.position.z);
        RaycastHit hit;
        grounded = Physics.Raycast(start, Vector3.down, out hit, 0.2f) && hit.collider.tag != "Player";
        Debug.DrawRay(start, Vector3.down * 0.2f, Color.red);
    }
}