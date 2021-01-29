using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchPit : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		// TODO dont destroy but use Justins interface
		Destroy(other.gameObject);
	}
}
