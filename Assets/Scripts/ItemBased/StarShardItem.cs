using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Star Shard")]
public class StarShardItem : Item
{
    int ssid;

    public shardEffect effect1;
    public shardEffect effect2;
    public HPorSP hporsp;
    [Tooltip("Used as both a threshold to check against and as the damage value for the user")]
    public int threshold;
    public int percentage;
    public Move extraMove;
    public StatusEffectList statusEffect;

    [Space, Range(-10,10)]
    public int modAtk;
    [Range(-10, 10)]
    public int modDef;
    [Range(-10, 10)]
    public int modSp;
    [Range(-10, 10)]
    public int modSpD;
    [Range(-10, 10)]
    public int modSpd;
    [Range(-10, 10)]
    public int modLck;
    [Range(-10, 10)]
    public int modSkl;

    public bool CompareIDs(int id)
    {
        return ssid == id;
    }

    public void SetupShard(CharacterStats character)
    {
        if(effect1 == shardEffect.statChange || effect2 == shardEffect.statChange)
        {
            character.attack.AddModifier(modAtk);
            character.defense.AddModifier(modDef);
            character.special.AddModifier(modSp);
            character.special_Defense.AddModifier(modSpD);
            character.speed.AddModifier(modSpd);
            character.luck.AddModifier(modLck);
            character.skill.AddModifier(modSkl);
        }
        
        if(effect1 == shardEffect.CauseStatus || effect2 == shardEffect.CauseStatus)
        {
            character.GetComponent<StatusEffectHandler>().AssignStatus(statusEffect);
        }

        if (effect1 == shardEffect.GiveMove || effect2 == shardEffect.GiveMove)
        {
            character.GetComponent<UnitMoveList>().AddStandardMove(extraMove);
        }

        if (effect1 == shardEffect.MoveCostAlter || effect2 == shardEffect.MoveCostAlter)
        {
            character.moveCostReduction += percentage;
        }

        if (effect1 == shardEffect.RNGRollAlter || effect2 == shardEffect.RNGRollAlter)
        {
            character.rngAlteration += percentage;
        }
    }

    public void ShardEffects(CharacterStats character)
    {
        switch (effect1)
        {
            case shardEffect.Threashold:
                if (hporsp == HPorSP.Hp && character.hp.GetCurrentValue() <= threshold) { ActivateEffect(); }
                if (hporsp == HPorSP.Sp && character.sp.GetCurrentValue() <= threshold) { ActivateEffect(); }
                break;
            case shardEffect.DamageUser:
                character.TakeDamage(threshold, false, false);
                break;

        }
        // Copy for effect2
    }

    public void RemoveShard(CharacterStats character)
    {
        if (effect1 == shardEffect.statChange || effect2 == shardEffect.statChange)
        {
            character.attack.RemoveModifier(modAtk);
            character.defense.RemoveModifier(modDef);
            character.special.RemoveModifier(modSp);
            character.special_Defense.RemoveModifier(modSpD);
            character.speed.RemoveModifier(modSpd);
            character.luck.RemoveModifier(modLck);
            character.skill.RemoveModifier(modSkl);
        }

        if (effect1 == shardEffect.GiveMove || effect2 == shardEffect.GiveMove)
        {
            character.GetComponent<UnitMoveList>().RemoveStandardMove(extraMove);
        }

        if (effect1 == shardEffect.MoveCostAlter || effect2 == shardEffect.MoveCostAlter)
        {
            character.moveCostReduction -= percentage;
        }

        if (effect1 == shardEffect.RNGRollAlter || effect2 == shardEffect.RNGRollAlter)
        {
            character.rngAlteration -= percentage;
        }
    }

    public void ActivateEffect()
    {

    }
}

public enum shardEffect { none, statChange, MoveCostAlter, RNGRollAlter, CauseStatus, Threashold, GiveMove, DamageUser, StatusFoe, Field}

public struct StarShardAvailability
{
    public StarShardItem starShard;
    public bool isAvailable;
}