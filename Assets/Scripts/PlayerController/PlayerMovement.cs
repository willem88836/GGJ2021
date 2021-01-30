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

        //rigidBody.velocity = inputVelocity;

        rigidBody.velocity = Vector3.SmoothDamp(rigidBody.velocity, inputVelocity, ref dampVelocity, dampTime);

        /*
        hor_acc = CappedInterpolate(hor_acc, maxSpeed, vel.x);
        ver_acc = CappedInterpolate(ver_acc, maxSpeed, vel.z);
        */

        //this.body.AddForce(horizontalInput, 0, ver_acc);
    }

    /*
    private float CappedInterpolate(float acc, float cap, float vel)
    {
        if (Math.Abs(vel) >= cap && Math.Sign(vel) == Math.Sign(acc))
        {
            return 0;
        } 
        else if (acc == 0 && Math.Abs(vel) > Mathf.Epsilon)
        {
            return deceleration * -Mathf.Sign(vel);
        }
        else
        {
            return acc; 
        }
    }
    */
}
