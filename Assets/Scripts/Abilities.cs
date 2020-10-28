using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Abilities", menuName = "Abilities/New Ability")]
public class Abilities : ScriptableObject
{
    string abilityName;

    [TextArea(0, 4)]
    public string[] description;

    bool triggeredInBattle;

    StatusEffectList statusHolder;
    bool causeOrDeflectStatus;

    float multiplier;
    bool multUsedToAttack;

    [Header("In case we need a specific stat change")]
    TargetStat statAffected;
    int statChange;
    float hpSpThreshold;
}
