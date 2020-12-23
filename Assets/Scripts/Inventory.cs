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
    //[SerializeField]
    //public Dictionary<Item, int> itemList = new Dictionary<Item, int>();
    public List<Item> keyItems = new List<Item>();
    [Space]
    public List<Item> items = new List<Item>();
    [Space]
    public List<Weapon> weapons = new List<Weapon>();    

    public bool Add(Item item)
    {
        // For Normal Items
        if (!item.isDefaultItem && !item.isKeyItem)
        {
            if (items.Count >= space) //itemList.Count >= space
            {
                Debug.Log("Not enough room");
                return false;
            }
            
            if(item.isStackable && items.Exists(e => item)) //itemList.TryGetValue(item, out int value)
            {
                if (item.useNum < maxStack)
                {
                    //itemList[item] += 1;
                    item.IncrementUseNum(1);
                }
                else
                {
                    Debug.Log("Full on this item");
                    return false;
                }
            }
            else
            {
                //itemList.Add(item, 1);
                items.Add(item);
                item.IncrementUseNum(1);
            }
            
            if(onItemChangedCallback != null)
            {
                onItemChangedCallback.Invoke();
            }
            
        }
        // For Key Items
        if (!item.isDefaultItem && item.isKeyItem)
        {
            if (item.isStackable && !keyItems.Exists(e => item)) //itemList.TryGetValue(item, out int value)
            {
                if (item.useNum < maxStack)
                {
                    //itemList[item] += 1;
                    //item.IncrementUseNum(1);
                    item.infiniteUses = true;
                }
                else
                {
                    Debug.Log("Full on this item");
                    return false;
                }
            }
            else
            {
                //itemList.Add(item, 1);
                keyItems.Add(item);
                //item.IncrementUseNum(1);
                item.infiniteUses = true;
            }

            if (onItemChangedCallback != null)
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
        if(item.isKeyItem) { return; }
        if(item.isStackable && items.Exists(e => item)) //itemList.TryGetValue(item, out int value)
        {
            if (item.useNum > 0)
            {
                //itemList[item] -= 1;
                item.IncrementUseNum(-1);
            }
            else
            {
                //itemList.Remove(item);
                items.Remove(item);
            }
 
        }
        else
        {
            //itemList.Remove(item);
            //items.Remove(items.Find(e => item));
            items.Remove(item);
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
