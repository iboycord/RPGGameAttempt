using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendshipStats : MonoBehaviour
{

    public static float CheckFlavorPower(FlavorType f1A, FlavorType f2A, FlavorType f1B, FlavorType f2B)
    {
        if (((f1A == f1B) || (f1A == f2B)) && ((f2A == f1B) || (f2A == f2B))) return 1.5f;
        if (f1A == f1B || f1A == f2B || f2A == f1B || f2A == f2B) return 1.25f;
        else return 1;
    }

    public static float CheckFlavorPower(FlavorType f1, FlavorType f2)
    {
        if (f1 == f2)
        {
            return 1.25f;
        }
        else return 1;
    }

}
