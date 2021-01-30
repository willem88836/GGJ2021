using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class TruckDoor : MonoBehaviour
{
	[SerializeField]
	private float angleOffset;

	[SerializeField]
	private float openTime;

	[SerializeField]
	private float closeTime;

	Vector3 orignalEuler;
	Vector3 targetEuler;

	private void Awake()
	{
		orignalEuler = transform.localEulerAngles;
		targetEuler = orignalEuler + Vector3.up * angleOffset;
	}

	public IEnumerator OpenSequence()
	{
		float timer = 0;

		while (timer < openTime)
		{
			var percent = Mathf.Sqrt(timer / openTime);
			transform.localEulerAngles = Vector3.Lerp(orignalEuler, targetEuler, percent);

			timer += Time.deltaTime;
			yield return null;
		}

		transform.localEulerAngles = targetEuler;
	}

	public IEnumerator CloseSequence()
	{
		float timer = 0;

		while (timer < closeTime)
		{
			var percent = timer / closeTime;
			transform.localEulerAngles = Vector3.Lerp(targetEuler, orignalEuler, percent);

			timer += Time.deltaTime;
			yield return null;
		}

		transform.localEulerAngles = orignalEuler;
	}
}
