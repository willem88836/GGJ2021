using System;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private string horizontalKey = "Horizontal";
    [SerializeField] private string verticalKey = "Vertical";
    [SerializeField] private float acceleration;
    [SerializeField] private float deceleration;
    [SerializeField] private float maxSpeed;

    
    private Rigidbody body;


    private void Awake()
    {
        this.body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float hor_acc = acceleration * Input.GetAxis(horizontalKey);
        float ver_acc = acceleration * Input.GetAxis(verticalKey);

        Vector3 vel = this.body.velocity;

        hor_acc = CappedInterpolate(hor_acc, maxSpeed, vel.x);
        ver_acc = CappedInterpolate(ver_acc, maxSpeed, vel.z);

        this.body.AddForce(hor_acc, 0, ver_acc);
    }

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
}
