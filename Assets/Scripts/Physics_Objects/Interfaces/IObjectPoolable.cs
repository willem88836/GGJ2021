using UnityEngine;

public interface IObjectPoolable
{
	bool IsActivePoolObject { get; }

    void Activate();

    void Deactivate();

    GameObject GetGameObject();
}
