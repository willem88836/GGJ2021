using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
	Renderer renderer;

	[SerializeField] float animSpeed = 0.5f;

	float t = 0;

    void Start()
    {
		renderer = GetComponent<Renderer>();
	}

    void Update()
    {
		t -= Time.deltaTime * animSpeed;
		renderer.material.SetTextureOffset("_MainTex", new Vector2(0, t));
	}
}
