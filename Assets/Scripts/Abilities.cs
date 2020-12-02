using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Abilities", menuName = "Abilities/New Ability")]
public class Abilities : ScriptableObject
{
    string abilityName;

    [TextArea(0, 4)]
    public string[] description;

    public AbilityTrigger trigger;

    bool triggeredInBattle;

    StatusEffectList statusHolder;
    bool causeOrDeflectStatus;

    float multiplier;
    bool multUsedToAttack;

    [Header("In case we need a specific stat change")]
    TargetStat statAffected;
    int statChange;
    int hpSpThreshold;

    public void AbilityStatusHandler(CharacterStats character)
    {
        switch (trigger)
        {
            case AbilityTrigger.None:
                break;
            case AbilityTrigger.HealthBased:
                // Check unit's hp
                if (character.hp.GetCurrentValue() <= hpSpThreshold) { ActivateAbility(); }
                break;
            case AbilityTrigger.SpBased:
                // Check unit's sp
                if (character.sp.GetCurrentValue() <= hpSpThreshold) { ActivateAbility(); }
                break;
        }
    }

    public void ActivateAbility()
    {

    }

}
public enum AbilityTrigger { None, HealthBased, SpBased, MoveBased, StatusBased, ItemBased, WeaknessBased, DamageBased }