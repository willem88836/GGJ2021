using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoolEntry> spawnPools;

	[Space]
	[SerializeField]
	private float spawnDelay;
	[SerializeField]
	private int spawnCount;
	[SerializeField]
	private Vector2 spawnForce;
	[SerializeField]
	private float directionFactor;

	[Space]
	[SerializeField]
	private bool debugMode;
	[SerializeField]
	private KeyCode spawnKey;

    private readonly List<ObjectPool> _priorityList = new List<ObjectPool>();

	private Coroutine spawnRoutine = null;

	public bool IsSpawning { get; private set; }

	private void Awake()
	{
		BuildPriorityList();
	}

	private void Update()
	{
		if (debugMode)
		{
			if (Input.GetKeyDown(spawnKey))
			{
				StartCoroutine(StartSpawnSequence());
			}
		}
	}

	private void BuildPriorityList()
	{
		// Build a list that represent each pool based on its priority
		foreach (var pool in spawnPools)
		{
			for (int i = 0; i < pool.Priority; i++)
			{
				_priorityList.Add(pool.Pool);
			}
		}
	}

	private IObjectPoolable GetRandomObject()
	{
		var pool = GetRandomPool();
		return pool.GetAvailableObject();
	}

	private ObjectPool GetRandomPool()
	{
		// List entries already represent priority, just pick a random one
		var index = Random.Range(0, _priorityList.Count);

		return _priorityList[index];
	}

	private IEnumerator SpawnSequence(int count)
	{
		IsSpawning = true;

		for (int i = 0; i < count; i++)
		{
			var spawned = GetRandomObject();
			spawned.GetGameObject().GetComponent<MailItem>().Unexpire();
			var spawnedTransform = spawned.GetGameObject().transform;
			var spawnedEnforcable = spawned.GetGameObject().GetComponent<IPhysicsEnforcable>();

			// Prepare position and rotation
			spawnedTransform.position = transform.position;
			spawnedTransform.forward = Random.onUnitSphere;

			// Activate physics and renders
			spawned.Activate();

			// Do start physics stuff
			var direction = transform.forward + Random.onUnitSphere * directionFactor;
			var force = Random.Range(spawnForce.x, spawnForce.y);
			spawnedEnforcable.EnforceForce(direction, force);

			yield return new WaitForSeconds(spawnDelay);
		}

		IsSpawning = false;
	}

	public IEnumerator StartSpawnSequence(int count)
	{
		// Do not start if already spawning
		if (IsSpawning) yield break;

		spawnRoutine = StartCoroutine(SpawnSequence(count));
		yield return spawnRoutine;
	}

	public IEnumerator StartSpawnSequence()
	{
		// Do not start if already spawning
		if (IsSpawning) yield break;

		spawnRoutine = StartCoroutine(SpawnSequence(spawnCount));
		yield return spawnRoutine;
	}

	public void ExpireMail()
	{
		foreach(ObjectPool pool in _priorityList)
		{
			pool.Foreach((IObjectPoolable poolable) => {
				if (poolable.IsActivePoolObject)
				{
					MailItem mi = poolable.GetGameObject().GetComponent<MailItem>();

					if(mi.GetType() != Type.baddie)
					{
						mi.Expire();
					}
				}
			});
		}
	}
}
