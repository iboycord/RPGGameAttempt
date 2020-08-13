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
    bool causedStatChange = false;

    public GameObject gfx;

    public virtual void Execute()
    {





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
