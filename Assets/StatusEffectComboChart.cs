using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectComboChart : MonoBehaviour
{
    [System.Serializable]
    public class StatusChartElement
    {
        public StatusEffectList statusEffectID;
        public StatusEffect statusEffect;
    }

    public static Dictionary<StatusEffectList, StatusEffect> statusParings = new Dictionary<StatusEffectList, StatusEffect>();
    public StatusChartElement[] chartElement;

    public static void AddToChart(StatusEffect statusEffect)
    {
        statusParings.Add(statusEffect.id, statusEffect);
    }

    public static StatusEffect LookupStatus(StatusEffectList statusEffect)
    {
        return statusParings[statusEffect];
    }

    public static StatusEffect FindStatusWeakness(StatusEffectList statusEffectToFind)
    {
        return LookupStatus(LookupStatus(statusEffectToFind).weakness);
    }

    public static bool CompareStatusWeakness(StatusEffectList statusWithWeakness, StatusEffectList maybeTheWeakness)
    {
        if(LookupStatus(statusWithWeakness).weakness == maybeTheWeakness) { return true; }
        return false;
    }

    public static StatusEffect ReturnStatusEvolution(StatusEffectList statusEffect)
    {
        return LookupStatus(LookupStatus(statusEffect).transformsInto);
    }
}

// Notes:
/*
 *  > = beats
 *   Burning > Frozen > Bricked > Shocked > Soaked > Burning
 *   
 *   Enraged + Burning = Stunned?
 *   Stunned + Soaked = 
 *   
 */
public enum StatusEffectList { Burning, Soaked, Frozen, Shocked, Bricked, Enraged, Stunned, Silenced, Sleep, Regen, Drain, Replinish, Phantom }
