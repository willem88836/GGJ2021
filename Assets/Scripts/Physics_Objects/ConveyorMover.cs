using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConveyorMover : MonoBehaviour
{
	[SerializeField]
	private float power;

	[SerializeField]
	private float upFactor;

	private List<IPhysicsEnforcable> affectedObjects = new List<IPhysicsEnforcable>();

	private void FixedUpdate()
	{
		// Backwards because of texture orientation
		var direction = (-transform.forward + Vector3.up * upFactor).normalized;
		ApplyForceToAffected(direction);
	}

	private void ApplyForceToAffected(Vector3 direction)
	{
		affectedObjects = affectedObjects.Where(i => i != null).ToList();
		affectedObjects.ForEach(i => ApplyForce(i, direction));
	}

	private void ApplyForce(IPhysicsEnforcable enforcable, Vector3 direction)
	{
		enforcable.EnforceForce(-transform.forward, power);
	}

	private void OnTriggerEnter(Collider other)
	{
		var enforcable = other.GetComponent<IPhysicsEnforcable>();

		if (enforcable != null && !affectedObjects.Contains(enforcable))
		{
			affectedObjects.Add(enforcable);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		var enforcable = other.GetComponent<IPhysicsEnforcable>();

		if (enforcable != null && affectedObjects.Contains(enforcable))
		{
			affectedObjects.Remove(enforcable);
		}
	}
}
