using System.Linq;
using UnityEngine;

public class PhysicsDebugController : MonoBehaviour
{
    public Camera DebugView;
    public bool DebugMode;
    public ushort ForceRange;
    public ushort ForcePower;

    void Awake()
	{

	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (DebugMode)
		{
            if (Input.GetMouseButtonDown(0))
			{
                var point = GetMouseInteractionPosition();
                EnactForceAtPoint(point, ForceRange, ForcePower);
			}
		}
    }

    Vector3 GetMouseInteractionPosition()
	{
        var ray = DebugView.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
		{
            Debug.Log("Debug raycast hit: " + hit.point);
            return hit.point;
		}

        return Vector3.zero;
	}

    void EnactForceAtPoint(Vector3 point, ushort range, ushort power)
	{
        var overlapObjects = Physics.OverlapSphere(point, range).Select(i => i.gameObject);

        var enforcableObjects = overlapObjects.Select(i => i.GetComponent<IPhysicsEnforcable>()).Where(i => i != null);

        Debug.Log($"Applying force to {enforcableObjects.Count()} objects");

        foreach(var enforcable in enforcableObjects)
		{
            var direction = (enforcable.GetGameObject().transform.position - point).normalized;
            enforcable.EnforceForce(direction, power);
		}

        //var affected = overlapObjects
	}
}
