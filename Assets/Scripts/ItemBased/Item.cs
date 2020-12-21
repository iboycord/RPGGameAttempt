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

    // For everything
    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}

public enum ItemType { none, heal, attack, status, key, weapon }
public enum FlavorType { none, Sweet, Salty, Sour, Savory, Spicy, Bitter, Greasy, Natural, Meaty, Peanut_Butter }
