using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	// Editor stuff
	public GameObject Prefab;
	public ushort StartCount;

	public bool DebugMode;
	public KeyCode DebugSpawnKey;
	public KeyCode DebugResetKey;

	private readonly List<IObjectPoolable> _poolObjects = new List<IObjectPoolable>();

	void Awake()
    {
		SpawnInitialObjects();
    }

	private void Update()
	{
		if (DebugMode)
		{
			if (Input.GetKeyDown(DebugSpawnKey))
			{
				var poolObject = GetAvailableObject();
				poolObject.GetGameObject().transform.position = new Vector3(0, 5, 0);
				poolObject.Activate();
			}

			if (Input.GetKeyDown(DebugResetKey))
			{
				Reset();
			}
		}
	}

	private void SpawnInitialObjects()
	{
		// Prefab should be suitable for object pooling
		if (Prefab == null || Prefab.GetComponent<IObjectPoolable>() == null)
		{
			Debug.LogWarning("Prefab should contain a behaviour that implements IObjectPoolable");
			return;
		}

		for (int i = 0; i < StartCount; i++)
		{
			SpawnObject();
		}
	}

	private IObjectPoolable SpawnObject()
	{
		var newObject = GameObject.Instantiate(Prefab, transform);
		var poolableObject = newObject.GetComponent<IObjectPoolable>();

		poolableObject.Deactivate();

		_poolObjects.Add(poolableObject);

		return poolableObject;
	}

	private void ReturnObject(IObjectPoolable poolObject)
	{
		poolObject.Deactivate();
		poolObject.GetGameObject().transform.position = new Vector3(0, 5, 0);
	}

	private void Reset()
	{
		_poolObjects.ForEach(i => ReturnObject(i));
	}

	public IObjectPoolable GetAvailableObject()
	{
		return _poolObjects.FirstOrDefault(i => !i.IsActivePoolObject) ?? SpawnObject();
	}
}
