using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{
    public CharacterStats character;
    public StatusEffect currentStatusEffect = null;

    public void AssignStatus(StatusEffectList statusEffectToAssign)
    {
        if(currentStatusEffect == null) { currentStatusEffect = StatusEffectComboChart.LookupStatus(statusEffectToAssign); }
        else { CompareStatus(statusEffectToAssign); }
    }

    public void CompareStatus(StatusEffectList statusEffectToAssign)
    {
        if(StatusEffectComboChart.CompareStatusWeakness(currentStatusEffect.id, statusEffectToAssign))
        {
            currentStatusEffect = StatusEffectComboChart.LookupStatus(statusEffectToAssign);
            int dmg = currentStatusEffect.basePower > 0 ? Mathf.RoundToInt(currentStatusEffect.basePower * 1.5f) : Mathf.RoundToInt(character.maxHP.GetValue() * 0.1f);
            character.TakeDamage(dmg, false, false);
        }
    }

    public void TurnEffects()
    {
        currentStatusEffect.Execute(character);
    }

    public void ClearStatus()
    {
        currentStatusEffect = null;
    }

}
