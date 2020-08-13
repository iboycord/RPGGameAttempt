using System.Collections;
using System.Collections.Generic;
//using System.Linq;
using UnityEngine;
using TMPro;

public class TargetingSubMenu : MonoBehaviour
{
    public List<CharacterStats> activeTargets;
    public int index = 0;
    public GameObject targetUI;
    public Vector3 targetUIOffset = new Vector3(0, 0.5f, 0);
    public TMP_Text targetUIText;
    [Tooltip("0 to 1 - this should be the player's units. This to the list's count should be the enemies.")]
    public int playerUnitLowestBound = 2;

    public void OnExit()
    {
        index = 0;
    }

    public void Setup(CharacterStats playerUnit1, CharacterStats playerUnit2, CharacterStats[] enemies)
    {
        if(activeTargets == null)
        {
            activeTargets = new List<CharacterStats>();
        }
        activeTargets.Add(playerUnit1);
        activeTargets.Add(playerUnit2);
        for (int i = 0; i < enemies.Length; i++)
        {
            activeTargets.Add(enemies[i]);
        }
    }

    public void AddTarget(CharacterStats targetUnit)
    {
        if(activeTargets == null)
        {
            activeTargets = new List<CharacterStats>();
        }
        activeTargets.Add(targetUnit);
    }

    public void RemoveTarget(CharacterStats targetUnit)
    {
        activeTargets.Remove(targetUnit);
    }

    public CharacterStats SelectTarget()
    {
        return activeTargets[index];
    }

    public void IncrementTargetList()
    {
        CharacterStats target = IncrementList();
        targetUI.transform.position = target.transform.position + targetUIOffset;
        //targetUIText.SetText(target.charName);

        Debug.Log(target.name);
    }

    public void DecrementTargetList()
    {
        CharacterStats target = DecrementList();
        targetUI.transform.position = target.transform.position + targetUIOffset;
        //targetUIText.SetText(target.charName);

        Debug.Log(target.name);
    }

    // Takes in ints to allow targeting only certain things. For instance the player unit lowest bound can be passed in to either
    //   only look at the player's units or only look at the enemies.
    public void IncrementTargetList(int highestBound)
    {
        CharacterStats target = IncrementList(highestBound);
        targetUI.transform.position = target.transform.position + targetUIOffset;
        targetUIText.SetText(target.charName);
    }

    public void DecrementTargetList(int lowestBound)
    {
        CharacterStats target = DecrementList(lowestBound);
        targetUI.transform.position = target.transform.position + targetUIOffset;
        targetUIText.SetText(target.charName);
    }

    CharacterStats IncrementList()
    {
        if (index + 1 < activeTargets.Count)
        {
            index++;
        }
        else
        {
            index = 0;
        }
        return activeTargets[index];
    }

    CharacterStats DecrementList()
    {
        if (index - 1 > -1)
        {
            index--;
        }
        else
        {
            index = activeTargets.Count - 1;
        }
        return activeTargets[index];
    }

    // Increment/Decrement that take a number making the list bound to it.
    CharacterStats IncrementList(int highestBound)
    {
        if (index + 1 < highestBound)
        {
            index++;
        }
        else
        {
            index = highestBound;
        }
        return activeTargets[index];
    }

    CharacterStats DecrementList(int lowestBound)
    {
        if (index - 1 > lowestBound)
        {
            index--;
        }
        else
        {
            index = lowestBound - 1;
        }
        return activeTargets[index];
    }

    public void ShowUI()
    {
        targetUI.SetActive(true);
    }
    public void HideUI()
    {
        targetUI.SetActive(false);
    }

}
