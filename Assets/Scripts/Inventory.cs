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
            Debug.LogWarning("More than one Inventroy found");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;
    public int maxStack = 99;

    public List<Item> Items = new List<Item>();
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
