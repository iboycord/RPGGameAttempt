using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField, Range(0,30), Tooltip("0-5 for normal characters, 0-10 for bosses, 11+ reserved for rare enemies and secret bosses. IF THIS IS NOT FOLLOWED THEN EVERYTHINGS BUNGLED.")]
    private int baseValue;

    [SerializeField]
    private int growth;

    private List<int> modifiers = new List<int>();

    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public int GetGrowth()
    {
        return growth;
    }

    public void UpdateValue(int val)
    {
        baseValue += val;
    }

    public void UpdateGrowth(int val)
    {
        growth += val;
    }

    public void AddModifier(int modifier)
    {
        if(modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }
    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }

}
