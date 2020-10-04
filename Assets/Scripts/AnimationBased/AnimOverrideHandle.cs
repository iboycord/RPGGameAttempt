using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimOverrideHandle : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetAnimation(AnimatorOverrideController overrideController)
    {
        anim.runtimeAnimatorController = overrideController;
    }

    public void PlayAnim(string animToPlay)
    {
        anim.CrossFade(animToPlay, 0.3f);
    }
}
