using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowerParticle : MonoBehaviour
{
	ParticleSystem _particle;

    void Start()
    {
		_particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Space))
			ToggleParticle();
    }

	void ToggleParticle()
	{
		if (_particle.isPlaying)
			_particle.Stop();
		else
			_particle.Play();
	}
}
