using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuContents : MonoBehaviour
{
    public ListType listType;
    public List<Item> itemList;
    public List<Move> moveList;
    public int listIndex = 0;

    public void OnExit()
    {
        listIndex = 0;
    }

    public Move IncrementMoveList()
    {
        if(listIndex + 1 < moveList.Count)
        {
            listIndex++;
        }
        else
        {
            listIndex = 0;
        }
        return moveList[listIndex];
    }

    public Move DecrementMoveList()
    {
        if(listIndex - 1 > -1)
        {
            listIndex--;
        }
        else
        {
            listIndex = moveList.Count - 1;
        }
        return moveList[listIndex];
    }

    public Move SelectMove()
    {
        return moveList[listIndex];
    }


    public Item IncrementItemList()
    {
        if (listIndex + 1 < moveList.Count)
        {
            listIndex++;
        }
        else
        {
            listIndex = 0;
        }
        return itemList[listIndex];
    }

    public Item DecrementItemList()
    {
        if (listIndex - 1 > -1)
        {
            listIndex--;
        }
        else
        {
            listIndex = moveList.Count - 1;
        }
        return itemList[listIndex];
    }

    public Item SelectItem()
    {
        return itemList[listIndex];
    }
}

public enum ListType { Moves, Items, Various }
