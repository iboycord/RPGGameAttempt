﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Statuses", menuName = "Status Effects/New Status")]
public class StatusEffect : ScriptableObject
{
    public StatusEffectList id;

    public int turnsLeft = 4;
    public int turnsToIncreaseBy = 4;

    public HPorSP hp_spChoice;
    [Range(0, 1.5f)]
    public float hp_spMultiplier;

    public int basePower;
    public StatusType statusType1;
    public StatusType statusType2;

    [Space]
    public TargetStat targetStat1;
    public TargetStat targetStat2;

    public float maxMultiplier1 = 2, maxMultiplier2 = 2;
    [Range(0,2)]
    public float statMultiplier1 = 1, statMultiplier2 = 1;
    //int statAfterMultiply1;
    //int statAfterMultiply2;
    [Space]
    public GameObject gfx;
    [Space, Tooltip("What status effect does this one combine with?")]
    public StatusEffectList weakness;
    [Tooltip("What do they combine into?")]
    public StatusEffectList transformsInto;
    [Tooltip("What status does this one resist?")]
    public StatusEffectList resists;

    // For executing commands every turn
    public virtual void Execute(CharacterStats characterAfflicted)
    {
        if(statusType1 == StatusType.Damaging || statusType2 == StatusType.Damaging)
        {
            characterAfflicted.TakeDamage(basePower, false, false);
        }
        if (statusType1 == StatusType.Healing || statusType2 == StatusType.Healing)
        {
            characterAfflicted.Heal(basePower);
        }

        if(statusType1 == StatusType.Drain || statusType2 == StatusType.Drain)
        {
            characterAfflicted.LooseSP(basePower);
        }

        if (turnsLeft - 1 > 0)
        {
            TurnIncrementor(-1);
        }
        if (turnsLeft <= 0)
        {
            End(characterAfflicted);
        }
    }

    public virtual void OnCreation(CharacterStats chara)
    {
        StatCheckChange(chara);
        SetBasePower(chara);
        TurnIncrementor(turnsToIncreaseBy);
    }

    public virtual void StatCheckChange(CharacterStats characterAfflicted)
    {

        if (statusType1 == StatusType.StatDown || statusType1 == StatusType.StatUp)
        {
            //statAfterMultiply1 = Mathf.RoundToInt(characterAfflicted.ReturnStatValue(targetStat1) * statMultiplier1);
            characterAfflicted.ReturnStat(targetStat1).AddModifier(Mathf.RoundToInt(characterAfflicted.ReturnStatValue(targetStat1) * statMultiplier1));
        }
        if (statusType2 == StatusType.StatDown || statusType2 == StatusType.StatUp)
        {
            //statAfterMultiply2 = Mathf.RoundToInt(characterAfflicted.ReturnStatValue(targetStat2) * statMultiplier2);
            characterAfflicted.ReturnStat(targetStat2).AddModifier(Mathf.RoundToInt(characterAfflicted.ReturnStatValue(targetStat2) * statMultiplier2));
        }
    }

    public virtual void ChangeMultiplier(CharacterStats chara, float newMulti1, float newMulti2)
    {
        ClearMultiplier(chara);
        statMultiplier1 = Mathf.Clamp(newMulti1, -maxMultiplier1, maxMultiplier1);
        statMultiplier2 = Mathf.Clamp(newMulti2, -maxMultiplier2, maxMultiplier2);
        StatCheckChange(chara);
        TurnIncrementor(turnsToIncreaseBy);
    }

    public virtual void ClearMultiplier(CharacterStats characterAfflicted)
    {
        if (statusType1 == StatusType.StatDown || statusType1 == StatusType.StatUp)
        {
            characterAfflicted.ReturnStat(targetStat1).RemoveModifier(Mathf.RoundToInt(characterAfflicted.ReturnStatValue(targetStat1) * statMultiplier1));
            //statAfterMultiply1 = 0;
        }
        if (statusType2 == StatusType.StatDown || statusType2 == StatusType.StatUp)
        {
            characterAfflicted.ReturnStat(targetStat2).RemoveModifier(Mathf.RoundToInt(characterAfflicted.ReturnStatValue(targetStat2) * statMultiplier2));
            //statAfterMultiply2 = 0;
        }
    }

    public virtual void End(CharacterStats characterAfflicted)
    {
        if(statusType1 == StatusType.DamagesWhenExpired || statusType2 == StatusType.DamagesWhenExpired)
        {
            characterAfflicted.TakeDamage(basePower, false, false);
        }

        ClearMultiplier(characterAfflicted);

        //Remove the status somehow. Thinking theres a script on each combatant that reads a status' effects and then executes them
    }

    public virtual bool QuickLostTurnCheck()
    {
        if(statusType1 == StatusType.LostTurn || statusType2 == StatusType.LostTurn)
        {
            return true;
        }
        return false;
    }

    public virtual void TurnIncrementor(int turns)
    {
        turnsLeft += turns;
    }

    public virtual int SetBasePower(CharacterStats characterAfflicted)
    {
        int retVal = 0;
        if(hp_spChoice == HPorSP.Hp)
        {
            retVal = Mathf.FloorToInt(characterAfflicted.maxHP.GetValue() * hp_spMultiplier);
        }
        if (hp_spChoice == HPorSP.Sp)
        {
            retVal = Mathf.FloorToInt(characterAfflicted.maxSP.GetValue() * hp_spMultiplier);
        }

        return retVal;
    }
}

public enum StatusType { None, Damaging, Healing, LostTurn, Enraged, DamagesWhenExpired, StatDown, StatUp, Drain, Replenish }
public enum HPorSP { Hp, Sp }
