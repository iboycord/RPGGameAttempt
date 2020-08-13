using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public override void Interact()
    {
        base.Interact();

        Pickup();
    }

    void Pickup()
    {
        Debug.Log("Picking up " + item.name);
        //add to invintory
        bool wasGotten = Inventory.instance.Add(item);
        if (wasGotten)
        {
            Destroy(gameObject);
        }
    }
}
