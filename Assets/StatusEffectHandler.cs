using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{
    public CharacterStats character;
    public StatusEffect currentStatusEffect = null;

    public bool participatedInDuoMove = false;
    int duoCooldown = 0;
    public int extraTurns = 0;
    [HideInInspector]
    public int extraTurnsToGive = 1;

    public int currentStatusTurnCount = 0;

    public void Awake()
    {
        character = gameObject.GetComponent<CharacterStats>();
    }

    public void AssignStatus(StatusEffectList statusEffectToAssign)
    {
        if(currentStatusEffect == null) 
        { 
            currentStatusEffect = StatusEffectComboChart.LookupStatus(statusEffectToAssign);
            currentStatusEffect.StatCheckChange(character);
            currentStatusEffect.AccelerateStartup(this, extraTurnsToGive);
        }
        else { CompareStatus(statusEffectToAssign); }
    }

    public void CompareStatus(StatusEffectList statusEffectToAssign)
    {
        if(!StatusEffectComboChart.CompareStatus(statusEffectToAssign, currentStatusEffect.id))
        {
            if (StatusEffectComboChart.CompareStatusWeakness(currentStatusEffect.id, statusEffectToAssign))
            {
                currentStatusEffect = StatusEffectComboChart.LookupStatus(statusEffectToAssign);
                //int dmg = currentStatusEffect.basePower > 0 ? Mathf.RoundToInt(currentStatusEffect.basePower * 1.5f) : Mathf.RoundToInt(character.maxHP.GetValue() * 0.1f);
                character.TakeDamage(Mathf.RoundToInt(character.maxHP.GetValue() * 0.2f), false, false);
            }
        }
        else
        {
            TurnCountIncrementer(3);
        }
    }

    public void TurnEffects()
    {
        if(currentStatusEffect != null)
        {
            currentStatusEffect.Execute(character);
            Debug.Log("Effect is Active?");

            if (currentStatusTurnCount - 1 >= 0) { TurnCountIncrementer(-1); }
            if (currentStatusTurnCount <= 0) 
            { 
                ClearStatus(); 
                currentStatusTurnCount = 0; 
            }
        }
        
    }

    public void ClearStatus()
    {
        currentStatusEffect.End(character);
        currentStatusEffect = null;
    }
    public void ClearDuoCooldown()
    {
        if (participatedInDuoMove && duoCooldown > 0)
        {
            duoCooldown = 0;
            participatedInDuoMove = false;
        }
    }

    public void TurnCountIncrementer(int turns)
    {
        currentStatusTurnCount += turns;
    }

    public void HelpedDuoMove()
    {
        participatedInDuoMove = true;
        duoCooldown = 1;
    }

    public void ExtraTurnIncrementer(int turns)
    {
        if(extraTurns < 0)
        {
            extraTurns = 0;
        }
        else
        {
            extraTurns += turns;
        }
    }
    public bool ExtraTurnCheck()
    {
        if (extraTurns > 0) { return true; }
        return false;
    }
    public void ExtraTurnGiverReset()
    {
        if (extraTurnsToGive > 1) { extraTurnsToGive = 1; }
    }
}
