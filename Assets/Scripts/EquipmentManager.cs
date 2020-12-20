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

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;
    public delegate void OnEquipmentChangedWeapon(Weapon newItem, Weapon oldItem);
    public OnEquipmentChangedWeapon onEquipmentChangedWeapon;

    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
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

    public void UnequipAll()
    {
        for(int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

}
