using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreMailCatcher : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		IObjectPoolable poolObject = other.GetComponent<IObjectPoolable>();
		if (poolObject != null)
		{
			poolObject.Deactivate();
		}
	}
}
