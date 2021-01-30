using System.Linq;
using UnityEngine;

public class LeafBlowerController : MonoBehaviour
{
    [SerializeField]
    private float pointRange;
    [SerializeField]
    private ushort forceRange;
    [SerializeField]
    private ushort forcePower;
    [SerializeField]
    private float upFactor;
    [SerializeField]
    OscillatorAnimation rake;

    private PlayerVisions playerVisions;

    private bool leftMouseDown;
    private bool rightMouseDown;

    // Start is called before the first frame update
    private void Start()
    {
        playerVisions = gameObject.GetComponent<PlayerVisions>();
    }

    private void Update()
    {
        leftMouseDown = Input.GetMouseButton(0);
        rightMouseDown = Input.GetMouseButton(1);
    }

	private void FixedUpdate()
	{
        if (leftMouseDown)
        {
            PushObjects();
        }
        else if (rightMouseDown)
        {
            PullObjects();
        }
    }

	private void PullObjects()
    {
        Vector3 inputDirection = (playerVisions.GetMouseWorldPoint() - transform.position).normalized;

        var closePoint = inputDirection * pointRange + transform.position;
        var farPoint = inputDirection * (pointRange * 2) + transform.position;

        // Always target ground
        closePoint.y = 0;
        farPoint.y = 0;

        var closeObjects = Physics.OverlapSphere(closePoint, forceRange).Select(i => i.gameObject);
        var farObjects = Physics.OverlapSphere(farPoint, forceRange * 2).Select(i => i.gameObject);

        var closeEnforcable = closeObjects.Select(i => i.GetComponent<IPhysicsEnforcable>()).Where(i => i != null);
        var farEnforcable = farObjects.Select(i => i.GetComponent<IPhysicsEnforcable>()).Where(i => i != null).Except(closeEnforcable);

        var direction = (-transform.forward).normalized;
        // Add up direction
        direction = (direction + Vector3.up * upFactor).normalized;

        foreach (var enforcable in closeEnforcable)
		{
            enforcable.EnforceForce(direction, forcePower * 2);
        }

        foreach (var enforcable in farEnforcable)
        {
            enforcable.EnforceForce(direction, forcePower);
        }
    }

    private void PushObjects()
    {
        Vector3 inputDirection = (playerVisions.GetMouseWorldPoint() - transform.position).normalized;

        var closePoint = inputDirection * pointRange + transform.position;
        var farPoint = inputDirection * (pointRange * 2) + transform.position;

        // Always target ground
        closePoint.y = 0;
        farPoint.y = 0;

        var closeObjects = Physics.OverlapSphere(closePoint, forceRange).Select(i => i.gameObject);
        var farObjects = Physics.OverlapSphere(farPoint, forceRange * 2).Select(i => i.gameObject);

        var closeEnforcable = closeObjects.Select(i => i.GetComponent<IPhysicsEnforcable>()).Where(i => i != null);
        var farEnforcable = farObjects.Select(i => i.GetComponent<IPhysicsEnforcable>()).Where(i => i != null).Except(closeEnforcable);

        var direction = (transform.forward).normalized;
        // Add up direction
        direction = (direction + Vector3.up * upFactor).normalized;

        foreach (var enforcable in closeEnforcable)
        {
            enforcable.EnforceForce(direction, forcePower * 2);
        }

        foreach (var enforcable in farEnforcable)
        {
            enforcable.EnforceForce(direction, forcePower);
        }
    }
}
