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

    [SerializeField]
    bool triggeredInBattle;

    [SerializeField]
    StatusEffectList statusHolder;
    [SerializeField]
    bool causeOrDeflectStatus;

    public float multiplierToStat;

    [Header("In case we need a specific stat change")]
    TargetStat statAffected;
    int statChange;
    float hpSpPercentThreshold;

    public void AbilityStatusHandler(CharacterStats character)
    {
        switch (trigger)
        {
            case AbilityTrigger.None:
                break;
            case AbilityTrigger.HealthBased:
                // Check unit's hp
                if (character.hp.GetCurrentValue() <= (character.hp.GetMaxValue() * hpSpPercentThreshold)) { ActivateAbility(); }
                break;
            case AbilityTrigger.SpBased:
                // Check unit's sp
                if (character.sp.GetCurrentValue() <= (character.sp.GetMaxValue() * hpSpPercentThreshold)) { ActivateAbility(); }
                break;
        }
    }

    public void ActivateAbility()
    {

    }

}
public enum AbilityTrigger { None, HealthBased, SpBased, MoveBased, StatusBased, ItemBased, WeaknessBased, DamageBased }