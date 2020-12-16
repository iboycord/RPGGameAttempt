using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCompendium : MonoBehaviour
{
    public static Dictionary<int, Move> standardMoveDictionary = new Dictionary<int, Move>();
    public Move[] standardMoveList;
    [Space]
    public static Dictionary<int, Move> fireMoveDictionary = new Dictionary<int, Move>();
    public Move[] fireMoveList;
    [Space]
    public static Dictionary<int, Move> waterMoveDictionary = new Dictionary<int, Move>();
    public Move[] waterMoveList;
    [Space]
    public static Dictionary<int, Move> earthMoveDictionary = new Dictionary<int, Move>();
    public Move[] earthMoveList;
    [Space]
    public static Dictionary<int, Move> electricityMoveDictionary = new Dictionary<int, Move>();
    public Move[] electricityMoveList;
    [Space]
    public static Dictionary<int, Move> spectralMoveDictionary = new Dictionary<int, Move>();
    public Move[] spectralMoveList;

    private void Awake()
    {
        if(standardMoveList.Length > 0)
        {
            for (int i = 0; i < standardMoveList.Length; ++i) { standardMoveDictionary.Add(i, standardMoveList[i]); }
        }
        
        if (fireMoveList.Length > 0) 
        {
            for (int i = 0; i < fireMoveList.Length; ++i) { fireMoveDictionary.Add(i, fireMoveList[i]); }
        }
        
        if (waterMoveList.Length > 0) 
        {
            for (int i = 0; i < waterMoveList.Length; ++i) { waterMoveDictionary.Add(i, waterMoveList[i]); }
        }
        
        if (earthMoveList.Length > 0) 
        {
            for (int i = 0; i < earthMoveList.Length; ++i) { earthMoveDictionary.Add(i, earthMoveList[i]); }
        }
        
        if (electricityMoveList.Length > 0) 
        {
            for (int i = 0; i < electricityMoveList.Length; ++i) { electricityMoveDictionary.Add(i, electricityMoveList[i]); }
        }

        if (spectralMoveList.Length > 0) 
        { 
            for (int i = 0; i < spectralMoveList.Length; ++i) { spectralMoveDictionary.Add(i, spectralMoveList[i]); } 
        }
    }

    public Move GetMove(int dicNum, int moveNum)
    {
        switch (dicNum)
        {
            case 0:
                return standardMoveDictionary[moveNum];
            case 1:
                return fireMoveDictionary[moveNum];
            case 2:
                return waterMoveDictionary[moveNum];
            case 3:
                return earthMoveDictionary[moveNum];
            case 4:
                return electricityMoveDictionary[moveNum];
            case 5:
                return spectralMoveDictionary[moveNum];
            default:
                // Failsafe
                return standardMoveDictionary[moveNum];
        }
    }

    public void PrintStandardMoves()
    {
        if(standardMoveList.Length <= 0) 
        {
            Debug.Log("No moves in Standard Dictionary");
            return;
        }
        for(int i = 0; i < standardMoveList.Length; ++i)
        {
            Move temp = standardMoveDictionary[i];
            Debug.Log("Current Move: " + temp.name + " Power: " + temp.basePower + "\n");
        }
    }
}
