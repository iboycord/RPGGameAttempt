using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveList : MonoBehaviour
{
    // Store two numbers and use them to find certain moves.
    

    [HideInInspector]
    public CharacterStats stats;
    [HideInInspector]
    public MoveCompendium compendium;

    public MoveNumHolder[] unitsStandardMoves;
    [Space]
    public MoveNumHolder[] unitsSpecialMoves;
    [Space]
    public int maxNumOfStandardMoves = 8;
    public List<Move> currentStandardMoves = new List<Move>();
    [Space]
    public int maxNumOfSpecialMoves = 8;
    public List<Move> currentSpecialMoves = new List<Move>();

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
        compendium = FindObjectOfType<MoveCompendium>();
    }

    private void Start()
    {
        // Extend for standard physical attacks and special moves.
        // Also extend so there's a move pool that all the available moves go into first before the ones in use are selected

        // For now so test units dont scream in pain
        if (GetComponent<EquipmentManager>()) { InitStandardMoveListWEM(); }
        else { InitStandardMoveList(); }
        InitSpecialMoveList();
    }

    public void InitStandardMoveList()
    {
        if(unitsStandardMoves.Length <= 0) { return; }
        int i = 0;
        bool foundEnd = false;
        while (i < unitsStandardMoves.Length && !foundEnd)
        {
            if (unitsStandardMoves[i].ReturnLevelLearned() > stats.level) { foundEnd = true; }
            currentStandardMoves.Add(compendium.GetMove(unitsStandardMoves[i].ReturnTypeNum(), unitsStandardMoves[i].ReturnMoveNum()));
            ++i;
        }
    }

    public void InitStandardMoveListWEM()
    {
        MoveNumHolder[] EMWM = GetComponent<EquipmentManager>().currentWeapon.WeapMoveset;
        for (int i = 0; i < EMWM.Length; ++i)
        {
            currentStandardMoves.Add(compendium.GetMove(EMWM[i].ReturnTypeNum(), EMWM[i].ReturnMoveNum()));
        }
    }

    public void InitSpecialMoveList()
    {
        if (unitsSpecialMoves.Length <= 0) { return; }
        int i = 0;
        bool foundEnd = false;
        while (i < unitsSpecialMoves.Length && !foundEnd)
        {
            if(unitsSpecialMoves[i].ReturnLevelLearned() > stats.level) { foundEnd = true; }
            currentSpecialMoves.Add(compendium.GetMove(unitsSpecialMoves[i].ReturnTypeNum(), unitsSpecialMoves[i].ReturnMoveNum()));
            ++i;
        }
    }

    public void AddStandardMove(int num)
    {
        Move tmp = compendium.GetMove(0, num);
        if (!currentStandardMoves.Exists(e => tmp) && currentStandardMoves.Count < maxNumOfStandardMoves) { currentStandardMoves.Add(tmp); }
    }
    public void AddStandardMove(Move addition)
    {
        if (!currentStandardMoves.Exists(e => addition) && currentStandardMoves.Count < maxNumOfStandardMoves) { currentStandardMoves.Add(addition); }
    }
    public void RemoveStandardMove(Move removeMatching)
    {
        currentStandardMoves.Remove(removeMatching);
    }
    public void RemoveStandardMove(int removeMatching)
    {
        currentStandardMoves.Remove(compendium.GetMove(0, removeMatching));
    }

}

[System.Serializable]
public struct MoveNumHolder
{
    [Tooltip(" 0 = Standard, 1 = Fire, 2 = Water, 3 = Earth, 4 = Electricity, 5 = Spectral ")]
    public int typeNum;
    public int moveNum;
    public int levelLearned;

    public int ReturnTypeNum() { return typeNum; }
    public int ReturnMoveNum() { return moveNum; }
    public int ReturnLevelLearned() { return levelLearned; }

    public void SetTypeNum(int num) { typeNum = num; }
    public void SetMoveNum(int num) { moveNum = num; }
    public void SetLevelLearned(int num) { levelLearned = num; }

    public void SetMoveNumHolder(MoveNumHolder other)
    {
        typeNum = other.typeNum;
        moveNum = other.moveNum;
        levelLearned = other.levelLearned;
    }
    public void SetMoveNumHolder(int type, int move, int level)
    {
        typeNum = type;
        moveNum = move;
        levelLearned = level;
    }
}
