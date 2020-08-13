using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Statuses", menuName = "Status Effects/New Status")]
public class StatusEffect : ScriptableObject
{
    public CharacterStats characterAfflicted;
    public int turnsLeft = 4;

    public int basePower;
    public StatusType statusType1;
    public StatusType statusType2;

    public TargetStat targetStat1;
    public TargetStat targetStat2;
    [Range(0,5)]
    public float statMultiplier;
    int statAfterMultiply;
    bool causedStatChange = false;

    public GameObject gfx;

    public virtual void Execute()
    {
        if(statusType1 == StatusType.Damaging || statusType2 == StatusType.Damaging)
        {
            characterAfflicted.TakeDamage(basePower, false, false);
        }
        if (statusType1 == StatusType.Healing || statusType2 == StatusType.Healing)
        {
            characterAfflicted.Heal(basePower);
        }



    }

    public virtual void OnCreation()
    {
        if (statusType1 == StatusType.StatDown)
        {
            statAfterMultiply = Mathf.RoundToInt(characterAfflicted.ReturnStatValue(targetStat1) * statMultiplier);
            characterAfflicted.ReturnStat(targetStat1).AddModifier(statAfterMultiply);
        }
    }

    public virtual void Update()
    {


    }

    public virtual void Clear()
    {
        if (statusType1 == StatusType.StatDown)
        {
            characterAfflicted.ReturnStat(targetStat1).RemoveModifier(statAfterMultiply);
            statAfterMultiply = 0;
        }
    }

    public virtual void End()
    {
        if(statusType1 == StatusType.DamagesWhenExpired || statusType2 == StatusType.DamagesWhenExpired)
        {
            characterAfflicted.TakeDamage(basePower, false, false);
        }
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
