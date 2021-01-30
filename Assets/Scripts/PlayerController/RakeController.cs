using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PlayerVisions))]
public class RakeController : MonoBehaviour
{
    [SerializeField] private float rakeRange;
    [SerializeField]
    private ushort forceRange;
    [SerializeField]
    private ushort forcePower;
    [SerializeField]
    private float upFactor;
    [SerializeField] OscillatorAnimation rake;

    private PlayerVisions playerVisions;

    private bool mouseIsDown = false;
    private bool mouseWasDown = false;

    // Start is called before the first frame update
    private void Start()
    {
        this.playerVisions = gameObject.GetComponent<PlayerVisions>();
    }

    private void Update()
    {
        //mouseWasDown = mouseIsDown;
        mouseIsDown = Input.GetMouseButton(0);

        if (mouseIsDown)
        {
            Vector3 mouse = playerVisions.GetMouseWorldPoint();
            Vector3 point = (mouse - transform.position).normalized * rakeRange + transform.position;

            var overlapObjects = Physics.OverlapSphere(point, forceRange).Select(i => i.gameObject);
            var enforcableObjects = overlapObjects.Select(i => i.GetComponent<IPhysicsEnforcable>()).Where(i => i != null);

            foreach (var enforcable in enforcableObjects)
            {
                var direction = (transform.position - enforcable.GetGameObject().transform.position).normalized;
                // Add up direction
                direction = (direction + Vector3.up * upFactor).normalized;
                enforcable.EnforceForce(direction, forcePower);
            }
        }

        rake.ToggleAnimation(mouseIsDown);
    }
}
