﻿using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
	[SerializeField] private ParticleSystem destroyParticle;
	[SerializeField] private float destroyDelay;
	[SerializeField] AudioSource audioSource;

	public void OnCollisionEnter(Collision other)
	{
		GameObject gameObject = other.gameObject;
		IObjectPoolable op = gameObject.GetComponent<IObjectPoolable>();
		if (op != null)
		{
			op.Deactivate();
			ParticleSystem particle = Instantiate(destroyParticle, transform);
			particle.transform.position = op.GetGameObject().transform.position;

			if (gameObject.transform.childCount > 1)
			{
				var material = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>()?.material;

				if (material != null)
				{
					particle.GetComponent<ParticleSystemRenderer>().material = material;
				}
			}

			Vector3 rotation = gameObject.transform.position - transform.position;
			Quaternion rot = Quaternion.Euler(rotation);
			particle.transform.rotation = rot;

			StartCoroutine(DestroyParticle(particle));
			if (audioSource.isPlaying == false)
				audioSource.Play();
		}
	}

	private IEnumerator<YieldInstruction> DestroyParticle(ParticleSystem particle)
	{
		yield return new WaitForSeconds(destroyDelay);
		Destroy(particle.gameObject);
	}
}
