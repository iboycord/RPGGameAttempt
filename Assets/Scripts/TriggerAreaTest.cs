using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAreaTest : MonoBehaviour
{
    public int id;

    private void OnTriggerEnter(Collider other)
    {
        GameEvents.current.DoorwayTriggerEnter(id);
    }

}
