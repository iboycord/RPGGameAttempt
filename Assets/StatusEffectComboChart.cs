using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectComboChart : MonoBehaviour
{
    struct StatusEffectStuff
    {
        public StatusEffect status;
        public string description;
    }

    Dictionary<StatusEffectList, StatusEffectStuff> statusParings = new Dictionary<StatusEffectList, StatusEffectStuff>();

}

public enum StatusEffectList { Burning, Soaked, Frozen, Shocked, Enraged, Stunned}
