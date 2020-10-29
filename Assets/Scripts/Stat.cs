using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField, Range(0,5)]
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
