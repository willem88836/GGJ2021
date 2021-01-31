using UnityEngine;

public class BlowerParticle : MonoBehaviour
{
	ParticleSystem _particle;

    void Awake()
    {
		_particle = GetComponent<ParticleSystem>();
    }

	public void ToggleParticle(bool active)
	{
		if (active)
			_particle.Play();
		else
			_particle.Stop();
	}
}
