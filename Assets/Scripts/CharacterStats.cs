using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : Unit, IComparable<CharacterStats>
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += onEquipmentChanged;
    }

    void onEquipmentChanged (Equipment newItem, Equipment oldItem)
    {
        if(newItem != null)
        {
            hp.AddModifier(newItem.HPMod);
            hp.UpdateCurrentValue(newItem.HPMod);

            sp.AddModifier(newItem.SPMod);
            sp.UpdateCurrentValue(newItem.SPMod);

            attack.AddModifier(newItem.attackMod);
            defense.AddModifier(newItem.defenceMod);
            special.AddModifier(newItem.defenceMod);
            special_Defense.AddModifier(newItem.defenceMod);
            luck.AddModifier(newItem.defenceMod);
            skill.AddModifier(newItem.defenceMod);
            speed.AddModifier(newItem.defenceMod);
        }
        if (oldItem != null)
        {
            hp.RemoveModifier(oldItem.HPMod);
            hp.UpdateCurrentValue(oldItem.HPMod);

            sp.RemoveModifier(oldItem.SPMod);
            sp.UpdateCurrentValue(-oldItem.SPMod);


            attack.RemoveModifier(oldItem.attackMod);
            defense.RemoveModifier(oldItem.defenceMod);
            special.RemoveModifier(oldItem.attackMod);
            special_Defense.RemoveModifier(oldItem.attackMod);
            luck.RemoveModifier(oldItem.attackMod);
            skill.RemoveModifier(oldItem.attackMod);
            speed.RemoveModifier(oldItem.attackMod);
        }

    }


    public void calcNextActTurn(int currentTurn)
    {
        nextActTurn = currentTurn + (int)Mathf.Ceil(100.0f / speed.GetValue());
    }
    public int CompareTo(CharacterStats otherStats)
    {
        //calcNextActTurn(nextActTurn);
        
        return -speed.GetValue().CompareTo(otherStats.speed.GetValue());
    }
}
