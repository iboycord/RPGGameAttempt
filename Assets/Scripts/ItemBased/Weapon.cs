using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    public MoveNumHolder[] WeapMoveset;

    public override void Use()
    {
        if(item_type != ItemType.weapon) { return; }
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}
