﻿using System.Linq;
using UnityEngine;

public class Bomb : MailItem
{
	[SerializeField] private float explosionRange;
	[SerializeField] private float explosionPower;
	[SerializeField] private float explosionTimer;
	[SerializeField] private float timerDeviation;
	[SerializeField] private float upFactor;
	[SerializeField] private ParticleSystem explosionParticle; 
	[SerializeField] private ParticleSystem sterretjesParticle;

	private bool isTicking;
	private float realExplosionTimer;
	private float tick;

	public override void Activate()
	{
		base.Activate();
		isTicking = true;
		realExplosionTimer = explosionTimer + Random.Range(-timerDeviation, timerDeviation);
		tick = 0;
		sterretjesParticle.Play();
	}

	public override void Deactivate()
	{
		base.Deactivate();
		isTicking = false;
		tick = 0;
		sterretjesParticle.Stop();
	}

	public void Update()
	{
		if (isTicking)
		{
			tick += Time.deltaTime;

			if (tick > realExplosionTimer)
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

				sterretjesParticle.Stop();
				explosionParticle.Play();

				Deactivate();
			}
		}
	}
}