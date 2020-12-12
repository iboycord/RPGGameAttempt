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
        //                   St, fr, wt, et, el  sp 
        
        // Standard - basic untyped attack
        /*Std*/ new float[] {ef, ef, ef, ef, ef, ef},
        
        // Fire - burn bright
        /*fir*/ new float[] {ef, ef, ne, se, ef, ef},
        
        // Water - wet and wild, includes ice
        /*wat*/ new float[] {ef, se, ef, ef, ne, ef},

        // Earth - has nature (grass & metal), wind, and toxins
        /*ert*/ new float[] {ef, ne, ef, ef, se, ef},
        
        // Electricity - zappy zappy + mental zappy zappy
        /*ele*/ new float[] {ef, ef, se, ne, ef, ef},

        // Spectral - ghostly attacks that dont make sense
        /*spc*/ new float[] {ef, ef, ef, ef, ef, se},
        /**/
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

public enum ElementalTyping { None, Standard, Fire, Water, Earth, Electricity, Spectral }
