using UnityEngine;

public class SimplePooledPhysicsObject : MonoBehaviour, IObjectPoolable, IPhysicsEnforcable
{
	[SerializeField]
	private Rigidbody RigidBody;

	[SerializeField]
	private Collider Collider;

	[SerializeField]
	private Renderer Renderer;


	// IObjectPoolable

	public GameObject GetGameObject()
	{
		return gameObject;
	}

	public bool IsActivePoolObject { get; private set; }

	public void Activate()
	{
		if (!RigidBody.detectCollisions) RigidBody.detectCollisions = true;
		if (RigidBody.isKinematic) RigidBody.isKinematic = false;

		if (!Collider.enabled) Collider.enabled = true;
		if (!Renderer.enabled) Renderer.enabled = true;

		IsActivePoolObject = true;
	}

	public void Deactivate()
	{
		if (Renderer.enabled) Renderer.enabled = false;
		if (Collider.enabled) Collider.enabled = false;

		if (!RigidBody.isKinematic) RigidBody.isKinematic = true;
		if (RigidBody.detectCollisions) RigidBody.detectCollisions = false;

		IsActivePoolObject = false;
	}

	// IOPhysicsEnforcable

	public void EnforceForce(Vector3 direction, float power)
	{
		RigidBody.AddForce(direction * power, ForceMode.Impulse);
	}

	public Rigidbody GetRigidbody()
	{
		return RigidBody;
	}
}
