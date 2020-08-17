using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{
    public StatusEffect positiveStatusEffect;
    public StatusEffect negativeStatusEffect1;
    public StatusEffect negativeStatusEffect2;

    public void AssignStatus(StatusEffect statusEffect)
    {
        if(statusEffect.effectType == PosorNeg.Positive)
        {
            positiveStatusEffect = statusEffect;
        }
        if(statusEffect.effectType == PosorNeg.Negative)
        {
            
        }
    }


}
