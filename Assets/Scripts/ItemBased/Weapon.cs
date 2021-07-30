using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Weapon")]
public class Weapon : Item
{
    public Range weaponRange;
    public WeaponTypes type;
    public MoveNumHolder[] WeapMoveset;

    [Header("Stat Mods")]
    [Space, Range(-5, 5)]
    public int modAtk;
    [Range(-5, 5)]
    public int modDef;
    [Range(-5, 5)]
    public int modSp;
    [Range(-5, 5)]
    public int modSpD;
    [Range(-5, 5)]
    public int modSpd;
    [Range(-5, 5)]
    public int modLck;
    [Range(-5, 5)]
    public int modSkl;

    public void Awake()
    {
        item_type = ItemType.shard;
        infiniteUses = true;
    }

    public bool CompareIDs(int id)
    {
        return this.item_ID == id;
    }

    public override void Use()
    {
        if (item_type != ItemType.weapon) { return; }
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }

    public void Remove()
    {
        if (item_type != ItemType.weapon) { return; }
        EquipmentManager.instance.Unequip();

        AddToInventory(); // Check this, might be adding it back somewhere else.
    }

    public void AddStats(CharacterStats character)
    {
        character.attack.AddModifier(modAtk);
        character.defense.AddModifier(modDef);
        character.special.AddModifier(modSp);
        character.special_Defense.AddModifier(modSpD);
        character.speed.AddModifier(modSpd);
        character.luck.AddModifier(modLck);
        character.skill.AddModifier(modSkl);
    }

    public void RemoveStats(CharacterStats character)
    {
        character.attack.RemoveModifier(modAtk);
        character.defense.RemoveModifier(modDef);
        character.special.RemoveModifier(modSp);
        character.special_Defense.RemoveModifier(modSpD);
        character.speed.RemoveModifier(modSpd);
        character.luck.RemoveModifier(modLck);
        character.skill.RemoveModifier(modSkl);
    }

    public enum Range
    {
        Close,
        Distance
    }

    public enum WeaponTypes
    {
        // Melee
        Blade, // Sword, dagger, rapier, ext
        Pole, // Lance, halberd, rods ext
        Heavy, // Axe, hammer, club thing
        Brawler, // gauntlets and sports tape
        Shield, // Aegis, buckler, ext

        //  Magic
        Tome,
        Stave, // Stave - physical whacking stick
        Imbued_Tools, // Rings, dice, and Cards (together?, D-D-D-D-DDDDD-DUEL)

        // Physical Ranged
        Gun, // Gun
        Bow,
        Dagger

    }
}
