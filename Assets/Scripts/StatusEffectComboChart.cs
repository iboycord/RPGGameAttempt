using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectComboChart : MonoBehaviour
{

    public static Dictionary<StatusEffectList, StatusEffect> statusParings = new Dictionary<StatusEffectList, StatusEffect>();
    public StatusEffect[] chartElement;

    private void Start()
    {
        foreach (StatusEffect statusEffect in chartElement)
        {
            AddToChart(statusEffect);
        }
    }

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

    public static bool CompareStatus(StatusEffectList status1, StatusEffectList status2)
    {
        if (status1 == status2) { return true; }
        return false;
    }

    public static StatusEffect ReturnStatusEvolution(StatusEffectList statusEffect)
    {
        return LookupStatus(LookupStatus(statusEffect).transformsInto);
    }
}

// Notes:
/*  
 *  Idea (10/26/2020): Break down into: (?) 
 *      bio (physical ailments like burning, poison, ext),
 *      psycho (ex: hp/sp/stats down, confusion, extra turn - pumped up?)
 *      socio (ailments that affect team synergy, like heart seal or sync down)
 *  
 *  > = beats
 *   Bio:
 *    Burn (hp over time, doesnt kill) > Frozen > 
 *    
 *   
 *   Psycho:
 *    Enraged (atk +2, def -2?)
 *    Envigorated (all stats up?)
 *    Feeling Good (Extra Turn/Turn Down)
 *    
 *   Socio:
 *    Heart Seal (cant charge duo skill)
 *    
 *  
 *  // Old --------------------------------------------------
 *  > = beats
 *   Burning > Frozen > Bricked > Shocked > Soaked > Burning
 *   
 *   Enraged + Burning = Stunned?
 *   Stunned + Soaked = 
 *   
 */
public enum StatusEffectList { Burning, Soaked, Frozen, Shocked, Bricked, Enraged, Stunned, Silenced, Sleep, Regen, Drain, Replinish, Phantom, Accelerate }
