using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerVisions))]
public class RakeController : MonoBehaviour
{
    [SerializeField] 
    private float rakeRange;
    [SerializeField]
    private ushort forceRange;
    [SerializeField]
    private ushort forcePower;
    [SerializeField]
    private float upFactor;
    [SerializeField] 
    OscillatorAnimation rake;

    [SerializeField]
    private float cooldownTime;

    private PlayerVisions playerVisions;

    private bool isOnCooldown;

    // Start is called before the first frame update
    private void Start()
    {
        playerVisions = gameObject.GetComponent<PlayerVisions>();
    }

    private void Update()
    {
        if (!isOnCooldown)
		{
            // Left mouse for push
            if (Input.GetMouseButtonDown(0))
			{
                PushObjects();
                StartCoroutine(StartCooldown());
			}

            // Right mouse for pull
            if (Input.GetMouseButtonDown(1))
            {
                PullObjects();
                StartCoroutine(StartCooldown());
            }
        }
    }

    private IEnumerator StartCooldown()
	{
        // Break if already on cooldown
        if (isOnCooldown) yield break;

        isOnCooldown = true;

        rake.ToggleAnimation(true);
        yield return new WaitForSeconds(cooldownTime);
        rake.ToggleAnimation(false);

        isOnCooldown = false;
	}

    private void PullObjects()
	{
        Vector3 inputDirection = (playerVisions.GetMouseWorldPoint() - transform.position).normalized;
        Vector3 point = inputDirection * rakeRange + transform.position;

        // Always target ground
        point.y = 0;

        var overlapObjects = Physics.OverlapSphere(point, forceRange).Select(i => i.gameObject);
        var enforcableObjects = overlapObjects.Select(i => i.GetComponent<IPhysicsEnforcable>()).Where(i => i != null);

        foreach (var enforcable in enforcableObjects)
        {
            var direction = (-transform.forward).normalized;

            // Add up direction
            direction = (direction + Vector3.up * upFactor).normalized;
            enforcable.EnforceForce(direction, forcePower);
        }
    }

    private void PushObjects()
	{
        Vector3 inputDirection = (playerVisions.GetMouseWorldPoint() - transform.position).normalized;
        Vector3 point = inputDirection * rakeRange + transform.position;

        var overlapObjects = Physics.OverlapSphere(point, forceRange).Select(i => i.gameObject);
        var enforcableObjects = overlapObjects.Select(i => i.GetComponent<IPhysicsEnforcable>()).Where(i => i != null);

        foreach (var enforcable in enforcableObjects)
        {
            var direction = (transform.forward).normalized;

            // Add up direction
            direction = (direction + Vector3.up * upFactor).normalized;
            enforcable.EnforceForce(direction, forcePower);
        }
    }
}
