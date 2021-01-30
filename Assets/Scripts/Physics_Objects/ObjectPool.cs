using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
	[SerializeField]
	private GameObject prefab;

	[SerializeField]
	private ushort startCount;

	[SerializeField]
	private bool debugMode;

	[SerializeField]
	private KeyCode debugSpawnKey;

	[SerializeField]
	private KeyCode debugResetKey;

	private readonly List<IObjectPoolable> _poolObjects = new List<IObjectPoolable>();

	void Awake()
    {
		SpawnInitialObjects();
    }

	private void Update()
	{
		if (debugMode)
		{
			if (Input.GetKey(debugSpawnKey))
			{
				var poolObject = GetAvailableObject();
				poolObject.GetGameObject().transform.position = new Vector3(0, 5, 0);
				poolObject.Activate();
			}

			if (Input.GetKeyDown(debugResetKey))
			{
				Reset();
			}
		}
	}

	private void SpawnInitialObjects()
	{
		// Prefab should be suitable for object pooling
		if (prefab == null || prefab.GetComponent<IObjectPoolable>() == null)
		{
			Debug.LogWarning("Prefab should contain a behaviour that implements IObjectPoolable");
			return;
		}

		for (int i = 0; i < startCount; i++)
		{
			SpawnObject();
		}
	}

	private IObjectPoolable SpawnObject()
	{
		var newObject = GameObject.Instantiate(prefab, transform);
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
