using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Statuses", menuName = "Status Effects/New Status")]
public class StatusEffect : ScriptableObject
{
    public int turnsLeft = 4;

    public int basePower;
    public StatusType statusType1;
    public StatusType statusType2;

    public TargetStat targetStat1;
    public TargetStat targetStat2;

    public float maxMultiplier = 5;
    [Range(0,5)]
    public float statMultiplier;
    int statAfterMultiply;

    public GameObject gfx;

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


        TurnIncrementor(-1);
    }

    public virtual void OnCreation(CharacterStats chara)
    {
        StatCheckChange(chara);
    }

    public virtual void StatCheckChange(CharacterStats characterAfflicted)
    {

        if (statusType1 == StatusType.StatDown || statusType1 == StatusType.StatUp)
        {
            statAfterMultiply = Mathf.RoundToInt(characterAfflicted.ReturnStatValue(targetStat1) * statMultiplier);
            characterAfflicted.ReturnStat(targetStat1).AddModifier(statAfterMultiply);
        }
        if (statusType2 == StatusType.StatDown || statusType2 == StatusType.StatUp)
        {
            statAfterMultiply = Mathf.RoundToInt(characterAfflicted.ReturnStatValue(targetStat2) * statMultiplier);
            characterAfflicted.ReturnStat(targetStat2).AddModifier(statAfterMultiply);
        }
    }

    public virtual void ChangeMultiplier(CharacterStats chara, int newMulti)
    {
        Clear(chara);
        if(newMulti > maxMultiplier)
        {
            statMultiplier = maxMultiplier;
        }
        if(newMulti < -maxMultiplier)
        {
            statMultiplier = maxMultiplier;
        }
        else
        {
            statMultiplier = newMulti;
        }
        StatCheckChange(chara);

    }

    public virtual void Clear(CharacterStats characterAfflicted)
    {
        if (statusType1 == StatusType.StatDown || statusType1 == StatusType.StatUp)
        {
            characterAfflicted.ReturnStat(targetStat1).RemoveModifier(statAfterMultiply);
            statAfterMultiply = 0;
        }
        if (statusType2 == StatusType.StatDown || statusType2 == StatusType.StatUp)
        {
            characterAfflicted.ReturnStat(targetStat2).RemoveModifier(statAfterMultiply);
            statAfterMultiply = 0;
        }
    }

    public virtual void End(CharacterStats characterAfflicted)
    {
        if(statusType1 == StatusType.DamagesWhenExpired || statusType2 == StatusType.DamagesWhenExpired)
        {
            characterAfflicted.TakeDamage(basePower, false, false);
        }

        Clear(characterAfflicted);

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
}

public enum StatusType { None, Damaging, Healing, LostTurn, Enraged, DamagesWhenExpired, StatDown, StatUp }
