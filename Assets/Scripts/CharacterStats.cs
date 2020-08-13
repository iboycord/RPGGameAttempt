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
            maxHP.AddModifier(newItem.HPMod);
            currentHP += newItem.HPMod;

            maxSP.AddModifier(newItem.SPMod);
            currentSP += newItem.SPMod;

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
            maxHP.RemoveModifier(oldItem.HPMod);
            currentHP -= oldItem.HPMod;

            maxSP.RemoveModifier(oldItem.SPMod);
            currentSP -= oldItem.SPMod;


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
