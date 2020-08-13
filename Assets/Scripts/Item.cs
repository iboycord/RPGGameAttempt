using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public itemType item_type = itemType.key;

    public useType use_type = useType.NoLimit;
    public int useNum = 0;

    public void Awake()
    {
        if(use_type == useType.NoLimit){
            useNum = 0;
        }
    }

    public virtual void Use()
    {
        Debug.Log("Using " + name);

    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}

public enum itemType { heal, attack, status, key }
public enum useType { singleUse, MultiUse, NoLimit }
