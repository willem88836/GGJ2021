using System.Linq;
using UnityEngine;

public class PhysicsDebugController : MonoBehaviour
{
    [SerializeField]
    private Camera debugView;

    [SerializeField]
    private bool debugMode;

    [SerializeField]
    private ushort forceRange;

    [SerializeField]
    private ushort forcePower;

    [SerializeField]
    private float upFactor;

    private bool mouseDown;

    // Update is called once per frame
    void Update()
    {
        if (debugMode)
		{
            if (Input.GetMouseButtonDown(0))
			{
                var point = GetMouseInteractionPosition();
                EnactForceAtPoint(point, forceRange, forcePower);
			}

            if (Input.GetMouseButton(1) && !mouseDown)
			{
                mouseDown = true;
            } 
            else if (mouseDown)
			{
                mouseDown = false;
			}
		}
    }

	private void FixedUpdate()
	{
        if (debugMode)
		{
            if (mouseDown)
            {
                var point = GetMouseInteractionPosition();
                EnactForceAtPoint(point, forceRange, forcePower);
            }
        }
	}

	Vector3 GetMouseInteractionPosition()
	{
        var ray = debugView.ScreenPointToRay(Input.mousePosition);

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

            // Add up direction
            direction = (direction + Vector3.up * upFactor).normalized;

            enforcable.EnforceForce(direction, power);
		}

        //var affected = overlapObjects
	}
}
