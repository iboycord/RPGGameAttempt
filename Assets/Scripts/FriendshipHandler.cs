using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendshipHandler : MonoBehaviour
{
    [HideInInspector]
    public CharacterStats character;

    // If there were more than one other person, I'd say to use a dictionary with the other characters' names as the key.
    // Relocated this into a more central script. Having it here would mean theres two instances of affinity when there only needs to be one.
    //public int affinity = 0;

    public FlavorType favoriteFlavor1;
    public FlavorType favoriteFlavor2;

    public void Awake()
    {
        character = gameObject.GetComponent<CharacterStats>();
    }

}