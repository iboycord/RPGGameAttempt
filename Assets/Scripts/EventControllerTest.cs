using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventControllerTest : MonoBehaviour
{
    public int id;

    private void Start()
    {
        GameEvents.current.OnDoorwayTrigger += OnDoorway;
    }

    private void OnDoorway(int id)
    {
        if(id == this.id)
        {
            Debug.Log("Yep");
        }
        
    }

    private void OnDestroy()
    {
        GameEvents.current.OnDoorwayTrigger -= OnDoorway;
    }

}
