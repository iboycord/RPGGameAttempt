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
 *  Setups, Transformations/Wild Card, KOs/Instakill (Not happening aganst player)
 *  
 *  Balance between situational (battles take short time), difficulty, and status' power (both strong and weak)
 *  Moves that have bonus damage against statuses
 *  Problems outside of battle? Mess/mesh with other parts of game,
 *  
 *  
 *  Burned - 1 hp damage per turn, atk - 1, weak to stunned
 *  Frozen - loose turns while active, 1 hp damage when released, fire gets rid of this instantly
 *      unthaw - secret status maybe? would make canceling frozen easier, but would also need to check for burn? Unless unthaw is the only one active for that turn.
 *  Poisoned - 3 hp damage per turn(?), weak to frozen
 *  Stunned - 5 sp damange, chance character afflicted cant move that turn, weak to poisoned
 *  
 *  Sleep - cant move but regain 1 hp and 5 sp per missed turn. Getting hit wakes them up
 *  Mute - Cannot open the Specials tab in battle
 *  Plushed/Baublefy - turn into a plush object (maybe plush frog or other animals), unable to use physical attacks
 *  Berserked - Cannot open/use the items or tactics tabs in battle
 *  Sackless - Cannot open the items tab in battle
 *  Topple - loose turns, fall on back
 *  
 *  HP Regen - regenerate 1 hp every turn this is active
 *  SP Regen - regenerate 5 sp every turn this is active
 *  Haste - gain extra turn(s)
 *  
 *  Fury - atk + 2, def - 2
 *  Calm - atk - 2, def + 2
 *  
 *  Danger - 5 hp left, atk + 1
 *  Peril - 1 hp left, atk + 3 (should override Danger)
 *  
 *  Amped Heart - Duo bar charges faster when the aflicted unit preforms a successful action command
 *  Heart Seal - Duo bar cannot be charged.
 *  Desync - seal duo items and duo move.
 *  
 *  Stat decreases - can go up to 8 or -8
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
