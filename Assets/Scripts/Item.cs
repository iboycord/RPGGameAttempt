using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public itemType item_type;
    public bool isKeyItem;

    public bool infiniteUses;
    public int useNum = 0;

    public Move move;
    public int healAmount;
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
            case itemType.attack:
                move.Use(user, target);
                UseIncrementer(-1);
                break;
            case itemType.heal:
                user.Heal(healAmount);
                UseIncrementer(-1);
                break;
            case itemType.status:
                StatusEffectHandler userSEH = user.gameObject.GetComponent<StatusEffectHandler>();
                userSEH.AssignStatus(statusApplied);
                break;
            case itemType.none:
                Debug.Log("This item has no effect...");
                break;
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

public enum itemType { heal, attack, status, none }
