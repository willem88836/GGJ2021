using System.Collections;
using UnityEngine;

public class Truck : MonoBehaviour
{
	[SerializeField]
	ObjectSpawner objectSpawner;

	[Space]
	[SerializeField] 
	float moveTowardsTime;
	[SerializeField] 
	float moveAwayTime;
	[SerializeField] 
	Vector3 moveOffset;

	[Space]
	[SerializeField]
	TruckDoor leftDoor;
	[SerializeField]
	TruckDoor rightDoor;

	Vector3 originalPosition;
	Vector3 targetPosition;

	Coroutine roundSequence = null;

	private void Awake()
	{
		originalPosition = transform.position;
		targetPosition = transform.position + moveOffset;
	}

	public void StartNextRound(int spawnCount)
	{
		if (roundSequence != null) return;

		objectSpawner.ExpireMail();
		roundSequence = StartCoroutine(RoundSequence(spawnCount));
	}

	private IEnumerator RoundSequence(int spawnCount)
	{
		float timer = 0;

		while (timer < moveTowardsTime)
		{
			var percent = timer / moveTowardsTime;
			transform.position = Vector3.Lerp(originalPosition, targetPosition, percent);

			timer += Time.deltaTime;
			yield return null;
		}

		transform.position = targetPosition;

		// Start opening both doors
		var leftOpen = StartCoroutine(leftDoor.OpenSequence());
		var rightOpen = StartCoroutine(rightDoor.OpenSequence());

		// Wait for both doors
		yield return leftOpen;
		yield return rightOpen;

		yield return StartCoroutine(objectSpawner.StartSpawnSequence(spawnCount));

		// Start closing both doors
		var leftClose = StartCoroutine(leftDoor.CloseSequence());
		var rightClose = StartCoroutine(rightDoor.CloseSequence());

		// Wait for both doors
		yield return leftClose;
		yield return rightClose;

		timer = 0;

		while (timer < moveAwayTime)
		{
			var percent = timer / moveAwayTime;
			transform.position = Vector3.Lerp(targetPosition, originalPosition, percent);

			timer += Time.deltaTime;
			yield return null;
		}

		transform.position = originalPosition;

		roundSequence = null;
	}
}
