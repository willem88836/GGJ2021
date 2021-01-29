using UnityEngine;

public interface IPhysicsEnforcable
{
    void EnforceForce(Vector3 direction, float power);

    GameObject GetGameObject();

    Rigidbody GetRigidbody();
}
