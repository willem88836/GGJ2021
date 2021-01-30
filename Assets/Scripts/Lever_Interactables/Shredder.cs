using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
	[SerializeField] private ParticleSystem destroyParticle;
	[SerializeField] private float destroyDelay;

	public void OnCollisionEnter(Collision other)
	{
		GameObject gameObject = other.gameObject;
		IObjectPoolable op = gameObject.GetComponent<IObjectPoolable>();
		if (op != null)
		{
			op.Deactivate();
			ParticleSystem particle = Instantiate(destroyParticle, transform);
			particle.transform.position = op.GetGameObject().transform.position;
			particle.GetComponent<ParticleSystemRenderer>().material = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material;

			Vector3 rotation = gameObject.transform.position - transform.position;
			Quaternion rot = Quaternion.Euler(rotation);
			particle.transform.rotation = rot;

			StartCoroutine(DestroyParticle(particle));
		}
	}

	private IEnumerator<YieldInstruction> DestroyParticle(ParticleSystem particle)
	{
		yield return new WaitForSeconds(destroyDelay);
		Destroy(particle.gameObject);
	}
}
