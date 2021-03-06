﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RakeController : MonoBehaviour, IEquipment
{
    [SerializeField]
    PlayerVisions playerVisions;

    [SerializeField] 
    GameObject visual;

    [SerializeField]
    KeyCode equipKey;

    [SerializeField]
    Color color;

    [Space]
    [SerializeField] 
    private float rakeRange;

    [SerializeField]
    private ushort forceRange;

    [SerializeField]
    private ushort forcePower;

    [SerializeField]
    private float upFactor;

    [SerializeField] 
    OscillatorAnimation animatedVisual;

    [SerializeField]
    private float cooldownTime;

    [Space]
    [SerializeField]
    private Image uiButton;
    [SerializeField]
    private Sprite activeSprite;
    [SerializeField]
    private Sprite inactiveSprite;

    private bool isOnCooldown;

	[SerializeField]
	AudioSource audioSource;

	[SerializeField]
	AudioClip hark;

	[SerializeField]
	AudioClip trek;

	private IEnumerator StartCooldown()
	{
        // Break if already on cooldown
        if (isOnCooldown) yield break;

        isOnCooldown = true;

        yield return new WaitForSeconds(cooldownTime);

        animatedVisual.ToggleAnimation(false);
        isOnCooldown = false;
	}

    private void PullObjects()
	{
        Vector3 inputDirection = (playerVisions.GetMouseWorldPoint() - transform.position).normalized;
        Vector3 point = inputDirection * rakeRange + transform.position;

        // Always target ground
        point.y = 0;

        var overlapObjects = Physics.OverlapSphere(point, forceRange).Select(i => i.gameObject);

        // Only affect objects that have the matching color or not color information
        var filteredObjects = overlapObjects.Where(i =>
        {
            var colorObject = i.GetComponent<IColorObject>();
            if (colorObject == null) return true;

            var objectColor = i.GetComponent<IColorObject>().GetColor();

            // Also always rake gray items
            return objectColor == Color.Gray || objectColor == color;
        });

        var enforcableObjects = filteredObjects.Select(i => i.GetComponent<IPhysicsEnforcable>()).Where(i => i != null);

        foreach (var enforcable in enforcableObjects)
        {
            var direction = (-transform.forward).normalized;

            // Add up direction
            direction = (direction + Vector3.up * upFactor).normalized;
            enforcable.EnforceForce(direction, forcePower);
        }

		AudioClip clip = hark;
		audioSource.clip = clip;
		audioSource.Play();
	}

    private void PushObjects()
	{
        Vector3 inputDirection = (playerVisions.GetMouseWorldPoint() - transform.position).normalized;
        Vector3 point = inputDirection * rakeRange + transform.position;

        var overlapObjects = Physics.OverlapSphere(point, forceRange).Select(i => i.gameObject);

        // Only affect objects that have the matching color or not color information
        var filteredObjects = overlapObjects.Where(i =>
        {
            var colorObject = i.GetComponent<IColorObject>();
            if (colorObject == null) return true;

            var objectColor = i.GetComponent<IColorObject>().GetColor();

            // Also always rake gray items
            return  objectColor == Color.Gray || objectColor == color;
        });

        var enforcableObjects = filteredObjects.Select(i => i.GetComponent<IPhysicsEnforcable>()).Where(i => i != null);

        foreach (var enforcable in enforcableObjects)
        {
            var direction = (transform.forward).normalized;

            // Add up direction
            direction = (direction + Vector3.up * upFactor).normalized;
            enforcable.EnforceForce(direction, forcePower);
        }

		AudioClip clip = trek;
		audioSource.clip = clip;
		audioSource.Play();
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
        return !isOnCooldown;
    }

    public void DoAction(KeyCode key)
    {
        if (key == KeyCode.Mouse0)
        {
            PushObjects();
        }
        else if (key == KeyCode.Mouse1)
        {
            PullObjects();
        }

        StartCoroutine(StartCooldown());
        animatedVisual.ToggleAnimation(true);
    }

    public void NoAction()
    {
        // Not implemented
    }

    public void DoFixedAction(KeyCode key)
    {
        // Not implemented
    }

    public void NoFixedAction()
    {
        // Not implemented
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
