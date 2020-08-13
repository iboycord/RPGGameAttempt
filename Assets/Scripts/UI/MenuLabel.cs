using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuLabel : MonoBehaviour
{
    public Button body;
    public TMP_Text myText;
    public GameObject me;

    private void Awake()
    {
        me = gameObject;
        myText = me.GetComponentInChildren<TMP_Text>();

        myText.SetText(me.name);
    }

    public void OpenSubMenu()
    {

    }

}
