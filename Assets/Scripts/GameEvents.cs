using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> OnDoorwayTrigger;
    public void DoorwayTriggerEnter(int id)
    {
        if(OnDoorwayTrigger != null)
        {
            OnDoorwayTrigger(id);
        }
    }


}
