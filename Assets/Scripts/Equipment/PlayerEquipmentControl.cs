using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerEquipmentControl : MonoBehaviour
{
    [SerializeField]
    private Transform equipmentContainer;

    private readonly Dictionary<KeyCode, IEquipment> equipments = new Dictionary<KeyCode, IEquipment>();
    private IEquipment currentEquipment;

    private Dictionary<KeyCode, bool> activeKeys;

	void Awake()
	{
        // Register equipment components

        foreach (var equipment in equipmentContainer.GetComponents<IEquipment>())
		{
            // Avoid double entries (would throw an exception anyway)
            if (!equipments.ContainsKey(equipment.GetEquipKey())) equipments[equipment.GetEquipKey()] = equipment;
		}
	}

	void Start()
    {
        foreach(var equipment in equipments.Values)
		{
            equipment.Unequip();
		}

        var firstEquipment = equipments.FirstOrDefault().Value;

        if (firstEquipment != null) Equip(firstEquipment);
    }

    void Equip(IEquipment equipment)
	{
        if (currentEquipment != null) currentEquipment.Unequip();

        currentEquipment = equipment;
        activeKeys = new Dictionary<KeyCode, bool>();

        // Fill relevant action keys, default inactive
        foreach(var key in equipment.GetActionKeys())
		{
            activeKeys.Add(key, false);
		}

        equipment.Equip();
    }

    void Update()
    {
        // Detect equipment swap input
        foreach (var key in equipments.Keys)
		{
            if (Input.GetKeyDown(key))
			{
                Equip(equipments[key]);
                return;
			}
		}

        // Update equipment input
        UpdateActiveKeys();

        // Detect equipment input
        if (currentEquipment.CanDoAction())
        {
            var activeKey = GetFirstActiveKey();
            if (activeKey != KeyCode.None)
            {
                currentEquipment.DoAction(activeKey);
            }
            else
			{
                currentEquipment.NoAction();
			}
        }
    }

    void FixedUpdate()
	{
        if (currentEquipment.CanDoAction())
        {
            var activeKey = GetFirstActiveKey();
            if (activeKey != KeyCode.None)
            {
                currentEquipment.DoFixedAction(activeKey);
            }
            else
			{
                currentEquipment.NoFixedAction();
			}
        }
    }

    void UpdateActiveKeys()
	{
        var toggleKeys = new List<KeyCode>();

        foreach (var key in activeKeys.Keys)
        {
            if (Input.GetKey(key) != activeKeys[key]) toggleKeys.Add(key);
        }

        // Toggle keys
        foreach(var key in toggleKeys)
		{
            activeKeys[key] = !activeKeys[key];

        }
    }

	KeyCode GetFirstActiveKey()
	{
        foreach (var key in activeKeys.Keys)
        {
            if (activeKeys[key]) return key;
        }

        return KeyCode.None;
    }

    public void UIPressed (int keyCode)
	{
        // Hardcode resulting key
        var inputKey = KeyCode.None;

        switch(keyCode)
		{
            case 1:
                inputKey = KeyCode.Alpha1;
                break;
            case 2:
                inputKey = KeyCode.Alpha2;
                break;
            case 3:
                inputKey = KeyCode.Alpha3;
                break;
            case 4:
                inputKey = KeyCode.Alpha4;
                break;
		}

        // Detect equipment swap input
        foreach (var key in equipments.Keys)
        {
            if (key == inputKey)
            {
                Equip(equipments[key]);
                return;
            }
        }
    }
}
