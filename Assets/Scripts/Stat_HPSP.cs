using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat_HPSP
{
    [SerializeField]
    private int maxValue;

    [SerializeField]
    private int currentValue;

    [SerializeField]
    private int maxGrowth;

    private List<int> mod = new List<int>();

    public void SetValue()
    {
        currentValue = maxValue;
    }
    public void SetValue(int newVal)
    {
        currentValue = newVal;
    }

    public int GetMaxValue()
    {
        int finalValue = maxValue;
        mod.ForEach(x => finalValue += x);
        return finalValue;
    }

    public int GetCurrentValue()
    {
        int finalValue = currentValue;
        mod.ForEach(x => finalValue += x);
        return finalValue;
    }

    public int GetGrowth()
    {
        return maxGrowth;
    }

    public void UpdateMaxValue(int val)
    {
        maxValue += val;
    }

    public void UpdateCurrentValue(int val)
    {
        currentValue += val;
    }

    public void UpdateGrowth(int val)
    {
        maxGrowth += val;
    }

    public void AddModifier(int modifier)
    {
        if (modifier != 0)
        {
            mod.Add(modifier);
        }
    }
    public void RemoveModifier(int modifier)
    {
        if (modifier != 0)
        {
            mod.Remove(modifier);
        }
    }
}
