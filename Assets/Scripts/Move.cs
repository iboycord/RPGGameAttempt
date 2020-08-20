using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Moves", menuName = "Moves/New Move")]
public class Move : ScriptableObject
{
    [HideInInspector]
    public CharacterStats moveUser;
    [HideInInspector]
    public CharacterStats movePartner;
    [HideInInspector]
    public CharacterStats movetarget;

    [TextArea(0, 4)]
    public string[] description;
    //[Tooltip("The move's power or rarity simplified.")]
    //public MoveStrength starRating;
    [Space, Tooltip("Does this move do damage, heal, cause a status, or a combination? ")]
    public MoveType type;

    [Space, Tooltip("Does this move cost something to use, and how much?")]
    public MoveCost cost;
    public int costAmnt;

    [Space, Tooltip("Is this move a solo move or a duo move?")]
    public Duality duality;
    [Tooltip("Is this move up close or far away?")]
    public Range range;
    [Tooltip("The move's elemental typing. Important in the damage calcs.")]
    public ElementalTyping elementType = ElementalTyping.Standard;
    [Tooltip("Which stat on the user does this move target.")]
    public TargetStat atkStat = TargetStat.attack;
    [Tooltip("Which stat on the target does this move target.")]
    public TargetStat defStat = TargetStat.defense;

    [Space, Tooltip("The move's power. Please confine to multiples of 5 please. My brain cant do math that well.")]
    public float basePower;
    [Tooltip("The move's natual ability to acchive a lucky blow and the multiplier for it... Might be getting removed due to crazy damage, dont know yet.")]
    public float baseLBlow;
    public float luckyMultiplier = 1.5f;
    [Tooltip("This is multiplied by Base Power to give moves damage variance. Higher = wacker values in both directions.")]
    public float randomnessCoefficient = 0.2f;

    [Space, Tooltip("Can this move cause a status?")]
    public bool canStatus;
    [Tooltip("...that status being...?")]
    public StatusEffectList status;
    [Tooltip("...and the chance?")]
    public int chanceToStatus;

    [Space, Tooltip("Does this move multihit?")]
    public bool multihit;
    [Tooltip("...how many times?")]
    public int noOfHits;

    public GameObject gfx;

    // Accesser Functions
    public float GetBaseDamage()
    {
        return basePower;
    }
    public string GetCost()
    {
        return costAmnt.ToString() + " " + cost.ToString();
    }



    // Use
    //  The general function that will be called for every move. Please put all move functions into Use for ease of access.
    public virtual void Use(CharacterStats user, CharacterStats target)
    {

    }

    // Attack
    //  Quick and dirty Attack method. Could easly put the damage formula function in here to run it easily from Use.
    public virtual void Attack(CharacterStats user, CharacterStats target)
    {
        SoloDamageFormula(user, target);
    }

    // Heal
    //  Quick and dirty Heal method. Could easly put the Heal formula function in here to run it easily from Use.
    public virtual void Heal()
    {

    }

    // ApplyStatus
    //  No status have been written yet. However this would hopefully apply them. Need a status manager to clear status in like 3 turns though
    public virtual void ApplyStatus()
    {

    }

    // Defend
    //  sets the user's damage reduction to the float passed in by the battle system and plays an animation.
    public virtual void Defend(CharacterStats user, float standardDefReduction)
    {
        user.dmgReduction = standardDefReduction;
    }

    // HealFormula
    //  Adds the certain stat and the move's base power
    public virtual int HealFormula(CharacterStats user)
    {
        int healer = user.ReturnStatValue(atkStat);

        float tempHeal = Mathf.Max(1, healer + basePower);

        int heal = Mathf.CeilToInt(tempHeal);

        return heal;
    }

    // SoloDamageFormula
    //  
    public virtual int SoloDamageFormula(CharacterStats user, CharacterStats target)
    {
        // This is just for testing
        moveUser = user;
        movetarget = target;
        // End

        float weak = 1;
        if (target.EType1 != ElementalTyping.None && target.EType2 != ElementalTyping.None)
        {
            weak = WeaknessChart.GetEffective(elementType, target.EType1, target.EType2);
        }
        if (target.EType1 != ElementalTyping.None)
        {
            weak = WeaknessChart.GetEffective(elementType, target.EType1);
        }

        // get target stat from both units, using the base power and ext ext.
        //int attacker = StatValue(user, atkStat);
        //int defender = StatValue(target, defStat);
        int attacker = user.ReturnStatValue(atkStat);
        int defender = target.ReturnStatValue(defStat);

        float tempDmg = Mathf.Max(1, ((attacker + ((basePower + Random.Range(-basePower * randomnessCoefficient, basePower * randomnessCoefficient)) * weak)) - (defender - CritStrike(defender))));
        
        int dmg = Mathf.CeilToInt(tempDmg);

        return dmg;
    }

    public virtual int DuoDamageFormula(CharacterStats user, CharacterStats partner, CharacterStats target)
    {
        int dmg;


        // get target stat from both units, using the base power and ext ext.
        int attacker1 = user.ReturnStatValue(atkStat);
        int defender = target.ReturnStatValue(defStat);

        float tempDmg = Mathf.Max(1, ((attacker1 + basePower) - (defender - CritStrike(defender))));

        dmg = Mathf.CeilToInt(tempDmg);

        return dmg;
    }

    public virtual int CritStrike(int def)
    {
        //int chance = Mathf.FloorToInt(StatValue(moveUser, TargetStat.skill) / 3);
        int chance = Mathf.FloorToInt(moveUser.ReturnStatValue(TargetStat.skill) / 3);

        // Come back here and change this constant 0.3 to the true guarding value later.
        int t = RNG(chance) ? Mathf.FloorToInt(def * 0.3f) : 0;

        return t;
        /*
        int t = Random.Range(0, 100);
        if (t <= chance)
        {
            // Come back here and change this constant to the true guarding value later.
            return Mathf.FloorToInt(def * 0.3f);
        }
        return 0;
        */
    }

    public virtual bool RNG(int numberToBeat)
    {
        int t = Random.Range(0, 100);
        if (t <= numberToBeat)
        {
            return true;
        }
        return false;
    }

    public virtual bool CostFn()
    {
        if(cost != MoveCost.None && costAmnt > 0)
        {
            if (cost == MoveCost.HP && moveUser.currentHP >= costAmnt + 1)
            {
                moveUser.currentHP -= costAmnt;
                return true;
            }
            if (cost == MoveCost.SP && moveUser.currentSP >= costAmnt)
            {
                moveUser.currentSP -= costAmnt;
                return true;
            }
        }
        return false;
    }

}

//public enum MoveStrength { One_Star, Two_Star, Three_Star, Four_Star, Five_Star, Six_Star }
public enum MoveType { Damage, Healing, Status, Defend, All }
public enum MoveCost { None, HP, SP }
public enum Duality { Solo, Duo }
public enum Range { Close, Far }
public enum TargetStat { defense, special_defense, attack, special, luck, skill, speed, level, maxHP, currentHP, maxSP, currentSP, currentXp, XpToNextLevel }