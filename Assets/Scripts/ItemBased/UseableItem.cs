using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Useable Item")]
public class UseableItem : Item
{
    public FlavorType flavor1;
    public FlavorType flavor2;

    public Move move;
    public int healAmount;
    public int spAmount;
    public StatusEffectList statusApplied;
    [Tooltip("True for effect on User, false for effect on target.")]
    public bool userOrTarget = true;
    public int baseFriendshipGiven;

    public void Awake()
    {
        useNum = Mathf.Clamp(useNum, 0, int.MaxValue);
    }

    // For useable items
    public override void Use(CharacterStats user, CharacterStats target)
    {
        Debug.Log("Using " + name);
        switch (item_type)
        {
            case ItemType.none:
                Debug.Log("This item has no effect...");
                break;
            case ItemType.attack:
                move.Use(user, target);
                UseIncrementer(-1);
                break;
            case ItemType.heal:
                Heal(user, target);
                UseIncrementer(-1);
                break;
            case ItemType.status:
                // Extend for both target and user
                if (userOrTarget)
                {
                    StatusEffectHandler userSEH = user.gameObject.GetComponent<StatusEffectHandler>();
                    userSEH.AssignStatus(statusApplied);
                }
                else
                {
                    StatusEffectHandler targetSEH = target.gameObject.GetComponent<StatusEffectHandler>();
                    targetSEH.AssignStatus(statusApplied);
                }
                UseIncrementer(-1);
                break;
            case ItemType.key:
                Debug.Log("No use?");
                break;
            case ItemType.weapon:
                Debug.Log("How did you get here? Thats wrong.");
                break;
            default:
                break;
        }

    }

    public void Heal(CharacterStats user, CharacterStats target)
    {
        float multiplier;
        // Set the multiplier
        multiplier = flavor1 != FlavorType.none && flavor2 != FlavorType.none ? 
            FriendshipStats.CheckFlavorPower(target.favoriteFlavor1, target.favoriteFlavor2, flavor1, flavor2)
            : FriendshipStats.CheckFlavorPower(target.favoriteFlavor1, flavor1);

        // Determine if you want ceil to int (gives more points) or round to int (less points = less hp issue)
        if (healAmount > 0)
        {
            target.Heal(Mathf.RoundToInt(healAmount * multiplier));
        }
        if (spAmount > 0)
        {
            target.RecoverSP(Mathf.RoundToInt(spAmount * multiplier));
        }
        GiveFriendship(user, target, multiplier);
    }

    public void GiveFriendship(CharacterStats user, CharacterStats target, float additional)
    {
        if (user.CompareTag("PlayerControlled") && target.CompareTag("PlayerControlled"))
        {
            FriendshipControl f = FindObjectOfType<FriendshipControl>();
            f.IncrementFriendship(Mathf.CeilToInt(baseFriendshipGiven * additional));
        }
    }

    public void UseIncrementer(int uses)
    {
        if (!infiniteUses || isKeyItem)
        {
            useNum += uses;
            if (useNum <= 0)
            {
                RemoveFromInventory();
            }
        }

    }

}

