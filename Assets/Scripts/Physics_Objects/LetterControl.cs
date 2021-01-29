using UnityEngine;

public class LetterControl : MonoBehaviour, IObjectPoolable, IPhysicsEnforcable
{
	// Editor stuff
	public Rigidbody RigidBody;
	public Collider Collider;
	public Renderer Renderer;

	void Awake()
    {
        
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

	// IObjectPoolable

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

	public GameObject GetGameObject()
	{
		return gameObject;
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
