using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIMoveHolder : MonoBehaviour
{
    public Button button;
    public Move heldMove;

    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
    }

    public void SetMove(Move setThis)
    {
        heldMove = setThis;
    }
    public Move GetMove()
    {
        return heldMove;
    }
}
