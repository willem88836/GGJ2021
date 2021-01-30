using UnityEngine;

public class HatchPit : MonoBehaviour
{
	private Hatch parent;

	public void SetHatch(Hatch parent)
	{
		this.parent = parent;
	}

	private void OnTriggerEnter(Collider other)
	{
		IObjectPoolable poolObject = other.GetComponent<IObjectPoolable>();
		if(poolObject != null)
		{
			poolObject.Deactivate();
			parent.OnObjectCaught(poolObject);
		}
	}
}
