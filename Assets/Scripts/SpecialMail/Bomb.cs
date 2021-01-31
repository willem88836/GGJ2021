using System.Linq;
using UnityEngine;

public class Bomb : MailItem
{
	[SerializeField] private float explosionRange;
	[SerializeField] private float explosionPower;
	[SerializeField] private float explosionTimer;
	[SerializeField] private float upFactor;

	private bool isTicking;
	private float tick;

	public override void Activate()
	{
		base.Activate();
		isTicking = true;
		tick = 0;
	}

	public override void Deactivate()
	{
		base.Deactivate();
		isTicking = false;
		tick = 0;
	}

	public void Update()
	{
		if (isTicking)
		{
			tick += Time.deltaTime;

			if (tick > explosionTimer)
			{
				var overlapObjects = Physics.OverlapSphere(transform.position, explosionRange).Select(i => i.gameObject);

				var enforcableObjects = overlapObjects.Select(i => i.GetComponent<IPhysicsEnforcable>()).Where(i => i != null);

				foreach (var enforcable in enforcableObjects)
				{
					var direction = (enforcable.GetGameObject().transform.position - transform.position).normalized;

					// Add up direction
					direction = (direction + Vector3.up * upFactor).normalized;

					enforcable.EnforceForce(direction, explosionPower);
				}

				Deactivate();
			}
		}
	}
}
