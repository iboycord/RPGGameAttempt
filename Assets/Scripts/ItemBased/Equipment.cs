using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public int equipSlot;
    public int HPMod;
    public int SPMod;
    public int attackMod;
    public int defenceMod;
    public int specialMod;
    public int special_DefenceMod;
    public int luckMod;
    public int skillMod;
    public int speedMod;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Feet, Weapon }