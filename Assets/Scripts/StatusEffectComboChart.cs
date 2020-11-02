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
 *  "Damage" over time. Defence/Stat adjust. Turn/Action economy, 
 *  debilitator/action stoppers, Loss of control (avoid), shields/Nulls,
 *  Setups, Transformations/Wild Card, KOs/Instakill (Not happening)
 *  
 *  Balance between situational (battles take short time), difficulty, and status' power (both strong and weak)
 *  Moves that have bonus damage against statuses
 *  Problems outside of battle? Mess/mesh with other parts of game,
 *  
 *  
 *  Burn - hp damage over time
 *  Frozen - loose turns while active
 *  Poisoned - hp and sp damage over time(?)
 *  
 *  Fury - atk + 2, def - 2
 *  
 *  Danger - 5 hp left, atk + 1
 *  Peril - 1 hp left, atk + 3 (should override Danger)
 *  
 *  Guard Phys - protects against physical
 *  Guard Spec - protects against special
 *  Guard Stus - protects against status moves
 *  Guard All  - protects against all above
 *  
 *  > = beats
 *   Bio:
 *    Burn (hp over time, doesnt kill) > Frozen > Toxic > Stoned >- Loop
 *    Work on this since there'd only be two unique things in the worst case
 *    (ie two things do damage over time and the other two take away tuns)
 *   
 *   Psycho:
 *    Enraged (atk +2, def -2?)
 *    Calm? (def +2, atk -2, opposite of enraged)
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
public enum StatusEffectList { None, Burning, Frozen, Poisoned, Stoned, Enraged, Stunned, Silenced, Sleep, Regen, Drain, Replinish, Phantom, Accelerate }
