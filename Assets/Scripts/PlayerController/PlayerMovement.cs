using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private string horizontalKey = "Horizontal";
    [SerializeField] 
    private string verticalKey = "Vertical";
    [SerializeField] 
    private float speed;

    [Space]
    [SerializeField]
    float dampTime;

    private Rigidbody rigidBody;
    Vector3 dampVelocity;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var horizontalInput = Input.GetAxisRaw(horizontalKey) * Vector3.right;
        var verticalInput = Input.GetAxisRaw(verticalKey) * Vector3.forward;

        var inputDirection = (horizontalInput + verticalInput).normalized;
        var inputVelocity = inputDirection * speed;

        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, inputVelocity, ref dampVelocity, dampTime);
    }
}
