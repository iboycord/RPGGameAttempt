using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public int item_ID;
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public ItemType item_type;
    public bool isKeyItem;
    public bool isStackable = false;
    public bool infiniteUses;
    // Only to be used for player interactions
    public int useNum = 0;

    // For everything
    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }
    public virtual void Use(CharacterStats p1, CharacterStats t1)
    {
        Debug.Log(p1.charName + " using " + name + " on " + t1.charName);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

    public void AddToInventory()
    {
        Inventory.instance.Add(this);
    }

    public void IncrementUseNum(int a)
    {
        useNum += a;
        if(useNum < 0) { useNum = 0; }
    }
}

public enum ItemType { none, heal, attack, status, key, weapon, shard }
public enum FlavorType { none, Sweet, Salty, Sour, Savory, Spicy, Bitter, Greasy, Natural, Meaty, Peanut_Butter }
