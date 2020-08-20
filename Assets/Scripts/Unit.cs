using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Header("Character")]
    public string charName;
    [TextArea(1,4)]
    public string[] description;

    [Space]
    [Header("Level, HP, and SP")]
    public int level;
    public Stat maxHP;
    public int currentHP;
    public Stat maxSP;
    public int currentSP;
    public int currentXp;
    public int XpToNextLevel;
    [Space]
    [Header("Stats")]
    public Stat attack;
    public Stat defense;
    public Stat special;
    public Stat special_Defense;
    public Stat luck;
    public Stat skill;
    public Stat speed;
    [Space]
    [Header("Elemental Typings")]

    public ElementalTyping EType1;
    public ElementalTyping EType2;

    public int nextActTurn;
    public float dmgReduction = 1;
    public bool isDead;

    private void Awake()
    {
        currentHP = maxHP.GetValue();
    }

    public void TakeDamage(int damage, bool canDodge, bool allowsDMGRedux)
    {
        if(canDodge && LuckRoll())
        {
            Debug.Log(transform.name + " got lucky!");
        }

        if(!canDodge || !LuckRoll())
        {
            if (allowsDMGRedux)
            {
                damage = Mathf.FloorToInt(Mathf.Clamp(damage, 0, int.MaxValue) * dmgReduction);
            }
            else
            {
                damage = Mathf.Clamp(damage, 0, int.MaxValue);
            }

            currentHP -= damage;
            Debug.Log(transform.name + " takes " + damage);

            if (currentHP <= 0)
            {
                Die();
            }
        }
    }

    public void LooseSP(int spLost)
    {
        spLost = Mathf.Clamp(spLost, 0, int.MaxValue);

        currentSP -= spLost;

        if(currentSP <= 0)
        {
            currentSP = 0;
        }
    }

    public void ResetDMGRedux()
    {
        if (dmgReduction != 1) { dmgReduction = 1; }
    }

    public void Heal(int heal)
    {
        heal = Mathf.Clamp(heal, 0, int.MaxValue);

        currentHP += heal;
        Debug.Log(transform.name + " heals " + heal);

        int temp = maxHP.GetValue();

        if (currentHP >= temp)
        {
            currentHP = temp;
        }
    }

    public void RecoverSP(int addSP)
    {
        addSP = Mathf.Clamp(addSP, 0, int.MaxValue);

        currentSP += addSP;
        Debug.Log(transform.name + " gets sp: " + addSP);

        int temp = maxSP.GetValue();

        if (currentSP >= temp)
        {
            currentSP = temp;
        }
    }

    public virtual void Die()
    {
        currentHP = 0;
        Debug.Log(transform.name + " died");
        isDead = true;
    }

    public virtual void Revive(int hpRestored)
    {
        currentHP += hpRestored;
        Debug.Log(transform.name + " has been revived.");
        isDead = false;
    }

    public virtual bool LuckRoll()
    {
        int chance = Mathf.FloorToInt(luck.GetValue() / 100);
        int t = Random.Range(0, 100);
        if (t <= chance)
        {
            return true;
        }
        return false;
    }

    public void LevelUp()
    {
        // Go through each stat and roll for it.
        // HP + however much
        level += 1;

        HpSpLvlUp(maxHP, currentHP);
        HpSpLvlUp(maxSP, currentSP);

        LvlUpStatHelper(attack);
        LvlUpStatHelper(defense);
        LvlUpStatHelper(special);
        LvlUpStatHelper(special_Defense);
        LvlUpStatHelper(luck);
        LvlUpStatHelper(skill);
        LvlUpStatHelper(speed);
    }

    public bool LevelDown()
    {
        if ((level - 1) < 0)
        {
            level -= 1;
            return true;
        }
        else return false;
    }

    public void HpSpLvlUp(Stat stat, int statCurrent)
    {
        int rand;

        if((stat.GetGrowth() - 5) < 0)
        {
            rand = Random.Range(0, stat.GetGrowth());
            stat.UpdateValue(rand);
            statCurrent += rand;
        }
        else
        {
            rand = Random.Range(stat.GetGrowth() - 5, stat.GetGrowth());
            stat.UpdateValue(rand);
            statCurrent += rand;
        }
        
    }

    public void LvlUpStatHelper(Stat changeStat)
    {
        int temp = changeStat.GetGrowth();
        int rand = Random.Range(0, 100);
        if(temp > 100)
        {
            int t = Mathf.CeilToInt(temp / 100);
            changeStat.UpdateValue(t);
            //temp -= 100;
        }
        if(rand <= temp)
        {
            changeStat.UpdateValue(1);
        }
    }

    public int ReturnStatValue(TargetStat target)
    {
        int statVal = 0;

        switch (target)
        {
            case TargetStat.defense:
                statVal = defense.GetValue();
                break;
            case TargetStat.special_defense:
                statVal = special_Defense.GetValue();
                break;
            case TargetStat.attack:
                statVal = attack.GetValue();
                break;
            case TargetStat.special:
                statVal = special.GetValue();
                break;
            case TargetStat.luck:
                statVal = luck.GetValue();
                break;
            case TargetStat.skill:
                statVal = skill.GetValue();
                break;
            case TargetStat.speed:
                statVal = speed.GetValue();
                break;
            case TargetStat.level:
                statVal = level;
                break;
            case TargetStat.maxHP:
                statVal = maxHP.GetValue();
                break;
            case TargetStat.currentHP:
                statVal = currentHP;
                break;
            case TargetStat.maxSP:
                statVal = maxSP.GetValue();
                break;
            case TargetStat.currentSP:
                statVal = currentSP;
                break;
            case TargetStat.currentXp:
                statVal = currentXp;
                break;
            case TargetStat.XpToNextLevel:
                statVal = XpToNextLevel;
                break;
        }

        return statVal;
    }

    public Stat ReturnStat(TargetStat target)
    {
        if(target == TargetStat.defense)
        {
            Stat stat = defense;
            return stat;
        }
        if (target == TargetStat.special_defense)
        {
            Stat stat = special_Defense;
            return stat;
        }
        if (target == TargetStat.attack)
        {
            Stat stat = attack;
            return stat;
        }
        if (target == TargetStat.special)
        {
            Stat stat = special;
            return stat;
        }
        if (target == TargetStat.luck)
        {
            Stat stat = luck;
            return stat;
        }
        if (target == TargetStat.skill)
        {
            Stat stat = skill;
            return stat;
        }
        if (target == TargetStat.speed)
        {
            Stat stat = speed;
            return stat;
        }
        if (target == TargetStat.maxHP)
        {
            Stat stat = maxHP;
            return stat;
        }
        if (target == TargetStat.maxSP)
        {
            Stat stat = maxSP;
            return stat;
        }
        return attack;
    }

}
