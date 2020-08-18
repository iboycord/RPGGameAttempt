using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectHandler : MonoBehaviour
{
    public StatusEffect currentStatusEffect = null;

    public void AssignStatus(StatusEffect statusEffect)
    {
        
    }

    public void ClearStatus()
    {
        currentStatusEffect = null;
    }

}
