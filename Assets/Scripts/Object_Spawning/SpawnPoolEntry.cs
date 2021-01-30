using System;
using UnityEngine;

[Serializable]
public class SpawnPoolEntry
{
	[SerializeField]
	private ObjectPool _pool;

	[SerializeField]
	private ushort _priority;

	public ObjectPool Pool
	{
		get { return _pool; }
	}

	public ushort Priority
	{
		get { return _priority; }
	}
}
