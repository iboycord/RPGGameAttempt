using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAnim : MonoBehaviour
{
    [SerializeField] private AnimatorOverrideController[] overrideControllersList;
    [SerializeField] private AnimOverrideHandle overrider;

    public void Set(int val)
    {
        overrider.SetAnimation(overrideControllersList[val]);
    }

}
