using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAnimationHandler : MonoBehaviour
{
    public float currentAnimMultiplier = 1;
    public bool canInput = false;
    public bool canCharge = false;
    public float chargeSpeed = 0.2f;

    public Move currentMove;
    public bool moveFinished = false;

    public BattleSystem battleSystem;
    public BattleUI battleUI;
    public string defend;

    public Animator attacker;
    public Animator defender;
    public Animator[] allies;

    private void Awake()
    {
        battleSystem = gameObject.GetComponent<BattleSystem>();
        //battleUI = gameObject.GetComponent<BattleUI>();
    }

    public void Update()
    {

        if (moveFinished) // || attacker attack animation is done
        {
            FinishAnim();
        }
        // Thinking about using this to handle the first bit of attack animations and also the defending animations
        //   Moves would call this upon being used, attack animations would play, trigger SetCurrentAnimMulti with the "timing"
        //    of the move (1x for normal, 1.5x for great, 2x for perfect?, also call it again at the end to set it back to 1x for enemies to use as well).
        //    Upon the player pressing the button, the animation would stop then play a "finishing" animation that actually triggers the
        //    Move's Use Function and passes in the timing (I think for now at least). 
        //
        //   As for defending, I think just playing the animation and manually increasing/decreasing the defence of the character would do well.
        //    I'd say it could be gradual decrease and the animation cant be cancelled so you do actually have to time it well.
        if (Input.GetKeyDown(KeyCode.A) && !moveFinished && canInput)
        {
            if(battleSystem.state == BattleState.PLAYERTURN)
            {
                ButtonPressHandle();
            }
            if(battleSystem.state == BattleState.ENEMYTURN)
            {
                
            }
        }


    }

    public void SetCurrentAnimMulti(float set)
    {
        currentAnimMultiplier = set;
    }
    public void SetCharge(bool currCharge)
    {
        canCharge = currCharge;
    }
    public void SetInput(bool currInput)
    {
        canInput = currInput;
    }
    public float ReturnAnimMulti()
    {
        return currentAnimMultiplier;
    }

    public void PlayAnimation(Animator user)
    {
        user.CrossFade("Attack", 0.2f);
    }

    public void FinishAnim()
    {
        moveFinished = true;
        //battleSystem.FinishTurn();
        battleUI.EndUIFeatures();
    }

    public void ButtonPressHandle()
    {
        switch (currentMove.attackType)
        {
            case MoveAttackType.Timing:
                FinishAnim();
                break;
            case MoveAttackType.Charge:
                if (canCharge) currentAnimMultiplier += chargeSpeed;
                else currentAnimMultiplier -= chargeSpeed;
                break;
            case MoveAttackType.Mash:
                currentAnimMultiplier += chargeSpeed;
                break;
        }
    }

    public void ResetHandlerVars()
    {
        attacker = null;
        defender = null;
        allies = null;
        currentAnimMultiplier = 1;
        currentMove = null;
        moveFinished = false;
        canCharge = false;
        canInput = false;
    }

    public void InitializeHandler(CharacterStats user, CharacterStats target, Move move)
    {
        ResetHandlerVars();
        attacker = user.GetComponent<Animator>();
        defender = target.GetComponent<Animator>();
        currentMove = move;
        //attacker.CrossFade("attack", 0.3f);
    }

    public void InitializeHandler(Animator user, Animator[] helpers, Animator target, Move move)
    {
        ResetHandlerVars();
        attacker = user;
        defender = target;
        allies = helpers;
        currentMove = move;
    }
    // Upon BattleSystem's setup, pass in the animators for each combatent into a function here that puts them into the array.
    //  this way you can access them by a number like the targeting menu.
}
