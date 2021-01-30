using System.Collections.Generic;
using UnityEngine;

public interface IEquipment
{
    KeyCode GetEquipKey();

    IEnumerable<KeyCode> GetActionKeys();

    void DoAction(KeyCode key);

    void NoAction();

    void DoFixedAction(KeyCode key);

    void NoFixedAction();

    bool CanDoAction();

    void Equip();

    void Unequip();
}
