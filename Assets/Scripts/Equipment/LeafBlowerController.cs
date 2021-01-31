using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LeafBlowerController : MonoBehaviour, IEquipment
{
    [SerializeField]
    PlayerVisions playerVisions;

    [SerializeField]
    GameObject visual;

    [SerializeField]
    KeyCode equipKey;

    [Space]
    [SerializeField]
    private float pointRange;

    [SerializeField]
    private ushort forceRange;

    [SerializeField]
    private ushort forcePower;

    [SerializeField]
    private float upFactor;

    [SerializeField]
    OscillatorAnimation animatedVisual;

	[SerializeField]
	BlowerParticle blowParticle;

	[SerializeField]
	BlowerParticle suckParticle;

	[SerializeField]
	AudioSource audioSource;

	[SerializeField]
	AudioClip blow;
	[SerializeField]
	AudioClip suck;

	[Space]
    [SerializeField]
    private Image uiButton;
    [SerializeField]
    private Sprite activeSprite;
    [SerializeField]
    private Sprite inactiveSprite;

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

    public KeyCode GetEquipKey()
    {
        return equipKey;
    }

    public IEnumerable<KeyCode> GetActionKeys()
    {
        yield return KeyCode.Mouse0;
        yield return KeyCode.Mouse1;
    }

    public bool CanDoAction()
    {
        // Can always do an action
        return true;
    }

    public void DoAction(KeyCode key)
    {
        // Not implemented
    }

    public void NoAction()
    {
        // Not implemented
    }

    public void DoFixedAction(KeyCode key)
    {
        if (key == KeyCode.Mouse0)
        {
            PushObjects();
			blowParticle.ToggleParticle(true);

			if (audioSource.isPlaying == false)
			{
				audioSource.clip = blow;
				audioSource.Play();
			}
			
		}
        else if (key == KeyCode.Mouse1)
        {
            PullObjects();
			suckParticle.ToggleParticle(true);

			if (audioSource.isPlaying == false)
			{
				audioSource.clip = suck;
				audioSource.Play();
			}
		}

        animatedVisual.ToggleAnimation(true);
    }

    public void NoFixedAction()
    {
        animatedVisual.ToggleAnimation(false);
		blowParticle.ToggleParticle(false);
		suckParticle.ToggleParticle(false);

		audioSource.Stop();
	}

	public void Equip()
    {
        if (!visual.activeSelf) visual.SetActive(true);

        uiButton.sprite = activeSprite;
    }

    public void Unequip()
    {
        if (visual.activeSelf) visual.SetActive(false);

        uiButton.sprite = inactiveSprite;
    }
}
