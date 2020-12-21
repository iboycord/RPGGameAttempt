using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("More than one Inventory found");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;
    public int maxStack = 99;

    public List<Weapon> weapons = new List<Weapon>();
    [Space]
    public List<ItemInList> items = new List<ItemInList>();

    public Dictionary<Item, int> itemList = new Dictionary<Item, int>();

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            
            if (itemList.Count >= space)
            {
                Debug.Log("Not enough room");
                return false;
            }
            
            if(item.isStackable && itemList.TryGetValue(item, out int value))
            {
                if(value < maxStack)
                {
                    itemList[item] += 1;
                }
                else
                {
                    Debug.Log("Full on this item");
                    return false;
                }
            }
            else
            {
                itemList.Add(item, 1);
            }
            
            if(onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            
        }
        return true;
    }

    public bool Add(Weapon newWeap)
    {
        if (weapons.Count >= space)
        {
            Debug.Log("Not enough room");
            return false;
        }

        weapons.Add(newWeap);
        return true;
    }

    public void Remove(Weapon removeWeap)
    {
        weapons.Remove(removeWeap);
    }

    public void Remove(Item item)
    {
        if(item.isStackable && itemList.TryGetValue(item, out int value))
        {
            if(value > 0)
            {
                itemList[item] -= 1;
            }
            else
            {
                itemList.Remove(item);
            }
 
        }
        else
        {
            itemList.Remove(item);
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}

[System.Serializable]
public struct ItemInList
{
    public Item item;
    public int amount;

    public Item GetItem() { return item; }
    public int GetAmount() { return amount; }

    public void IncrementAmount(int a) { amount += a; }
}
