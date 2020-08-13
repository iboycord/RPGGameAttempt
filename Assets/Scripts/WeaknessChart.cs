using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaknessChart : MonoBehaviour
{
    public static float ne = 0.5f; // not effective
    public static float ef = 1f; // effective
    public static float se = 1.5f; // super effective

    static readonly float[][] weaknessChart =
    {
        //                   St, fr, wt, et, el
        /*Std*/ new float[] {ef, ef, ef, ef, ef},
        /*fir*/ new float[] {ef, ef, ne, se, ef},
        /*wat*/ new float[] {ef, se, ef, ef, ef},
        /*ert*/ new float[] {ef, se, ef, ef, se},
        /*ele*/ new float[] {ef, ef, se, ne, ef}

    };

    public static float GetEffective(ElementalTyping attack, ElementalTyping defend)
    {
        if(attack == ElementalTyping.None || defend == ElementalTyping.None)
        {
            return 1;
        }

        int row = (int)attack - 1;
        int col = (int)defend - 1;

        return weaknessChart[row][col];
    }

    public static float GetEffective(ElementalTyping attack, ElementalTyping defend1, ElementalTyping defend2)
    {
        if (attack == ElementalTyping.None || defend1 == ElementalTyping.None && defend2 == ElementalTyping.None)
        {
            return 1;
        }

        int row = (int)attack - 1;
        int col = (int)defend1 - 1;
        float d1 = weaknessChart[row][col];

        col = (int)defend2 - 1;
        float d2 = weaknessChart[row][col];

        return d1 * d2;

    }

}

public enum ElementalTyping { None, Standard, Fire, Water, Earth, Electricity }
