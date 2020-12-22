using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuContents : MonoBehaviour
{
    public ListType listType;
    public List<Item> itemList;
    public List<Move> moveList;
    public int listIndex = 0;
    public UnitMoveList currentList;

    public void ListSetup(UnitMoveList list, bool stdOrSpl)
    {
        listType = ListType.Moves;
        currentList = list;
        moveList = stdOrSpl ? currentList.currentStandardMoves : currentList.currentSpecialMoves;
    }

    public void OnExit()
    {
        listIndex = 0;
        moveList = null;
    }

    public Move IncrementMoveList()
    {
        if(listIndex + 1 < moveList.Count)
        {
            ++listIndex;
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
            --listIndex;
        }
        else
        {
            listIndex = moveList.Count - 1;
        }

        if(listIndex <= -1) { listIndex = 0; }
        return moveList[listIndex];
    }

    public Move SelectMove()
    {
        return moveList[listIndex];
    }

    public void ItemListSetup(List<Item> i)
    {
        listType = ListType.Items;
        itemList = i;
    }

    public Item IncrementItemList()
    {
        if (listIndex + 1 < itemList.Count)
        {
            ++listIndex;
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
            --listIndex;
        }
        else
        {
            listIndex = itemList.Count - 1;
        }
        if (listIndex <= -1) { listIndex = 0; }
        return itemList[listIndex];
    }

    public Item SelectItem()
    {
        return itemList[listIndex];
    }
}

public enum ListType { Moves, Items, Various }
