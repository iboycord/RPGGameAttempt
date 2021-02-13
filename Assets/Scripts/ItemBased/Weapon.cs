using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    public WeaponTypes type;
    public MoveNumHolder[] WeapMoveset;

    public override void Use()
    {
        if(item_type != ItemType.weapon) { return; }
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }

    public void Remove()
    {
        if (item_type != ItemType.weapon) { return; }
        EquipmentManager.instance.Unequip();
    }

    public enum WeaponTypes
    {
        Blade, // Sword, dagger, rapier, ext
        Stave_based, // Lance, halberd, staves, rods ext
        Heavy, // Axe, hammer, club thing
        Brawler, // gauntlets and sports tape
        Gun, // Gun
        Shield, // Aegis, buckler, ext

        Tome,
        Bow,
        Ring // Ring Con accessory.

    }
}
