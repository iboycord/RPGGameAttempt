using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    Equipment[] currentEquipment;
    public Weapon currentWeapon;
    Inventory inventory;
    [SerializeField]
    StarShardItem[] equippedStarShards;
    [SerializeField]
    int ssNum = 0;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    public delegate void OnEquipmentChangedWeapon(Weapon newItem, Weapon oldItem);
    public OnEquipmentChangedWeapon onEquipmentChangedWeapon;
    public delegate void OnEquipmentChangedStarShard(StarShardItem newItem, StarShardItem oldItem);
    public OnEquipmentChangedStarShard onEquipmentChangedStarShard;

    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        equippedStarShards = new StarShardItem[5];
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if(onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }
        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    // For Weapons
    public void Equip(Weapon newItem)
    {
        Weapon oldItem = null;

        if (currentWeapon != null)
        {
            oldItem = currentWeapon;
            inventory.Add(oldItem);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChangedWeapon.Invoke(newItem, oldItem);
        }
        currentWeapon = newItem;
    }

    public void Unequip()
    {
        if (currentWeapon != null)
        {
            Weapon oldItem = currentWeapon;
            inventory.Add(oldItem);

            currentWeapon = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChangedWeapon.Invoke(null, oldItem);
            }
        }
    }

    // For Star Shards
    public void Equip(StarShardItem newItem)
    {
        if(ssNum < 5)
        {
            if (equippedStarShards[ssNum] != null)
            {
                inventory.Add(equippedStarShards[ssNum]);
            }

            if (onEquipmentChanged != null)
            {
                //onEquipmentChangedStarShard.Invoke(newItem, equippedStarShards[ssNum]);
            }
            equippedStarShards[ssNum] = newItem;
            newItem.SetupShard(GetComponent<CharacterStats>());
            ++ssNum;
        }
    }

    public void UnequipSS(int slotIndex)
    {
        if (equippedStarShards[slotIndex] != null)
        {
            StarShardItem oldItem = equippedStarShards[slotIndex];
            inventory.Add(oldItem);

            equippedStarShards[slotIndex] = null;
            equippedStarShards[slotIndex].RemoveShard(GetComponent<CharacterStats>());
            --ssNum;

            if (onEquipmentChanged != null)
            {
                onEquipmentChangedStarShard.Invoke(null, oldItem);
            }
        }
    }


    public void UnequipAll()
    {
        for(int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

}
