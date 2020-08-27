using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public ItemType item_type;
    public bool isKeyItem;
    public bool isStackable;

    public bool infiniteUses;
    public int useNum = 0;

    public FlavorType flavor1;
    public FlavorType flavor2;

    public Move move;
    public int healAmount;
    public int spAmount;
    public StatusEffectList statusApplied;

    public void Awake()
    {
        useNum = Mathf.Clamp(useNum, 0, int.MaxValue);
    }

    // For equipment
    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    // For useable items
    public virtual void Use(CharacterStats user, CharacterStats target)
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
                StatusEffectHandler userSEH = user.gameObject.GetComponent<StatusEffectHandler>();
                userSEH.AssignStatus(statusApplied);
                UseIncrementer(-1);
                break;
        }
        
    }

    public virtual void Heal(CharacterStats user, CharacterStats target)
    {
        float multiplier = FriendshipStats.CheckFlavorPower(target.favoriteFlavor1, target.favoriteFlavor2, flavor1, flavor2);
        if (healAmount > 0)
        {
            target.Heal(Mathf.RoundToInt(healAmount * multiplier));
        }
        if (spAmount > 0)
        {
            target.RecoverSP(Mathf.RoundToInt(spAmount * multiplier));
        }

    }

    public virtual void UseIncrementer(int uses)
    {
        if (!infiniteUses || isKeyItem)
        {
            useNum -= uses;
            if (useNum <= 0)
            {
                RemoveFromInventory();
            }
        }
        
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}

public enum ItemType { none, heal, attack, status }
public enum FlavorType { none, Sweet, Salty, Sour, Savory, Spicy, Bitter, Greasy, Natural, Meaty, Chocolate, Peanut_Butter }
