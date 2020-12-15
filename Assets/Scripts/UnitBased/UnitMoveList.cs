using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveList : MonoBehaviour
{
    // Store two numbers and use them to find certain moves.
    [System.Serializable]
    public struct MoveNumHolder
    {
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
    }

    [HideInInspector]
    public CharacterStats stats;
    [HideInInspector]
    public MoveCompendium compendium;

    public MoveNumHolder[] unitsMoves;
    [Space]
    public Move[] currentMoves = new Move[8];

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
        compendium = FindObjectOfType<MoveCompendium>();
    }

    private void Start()
    {
        // Extend for standard physical attacks and special moves.
        InitMoveList();
    }

    public void InitMoveList()
    {
        int i = 0;
        bool foundEnd = false;
        while (i < unitsMoves.Length && !foundEnd)
        {
            if(unitsMoves[i].ReturnLevelLearned() > stats.level) { foundEnd = true; }
            currentMoves[i] = compendium.GetMove(unitsMoves[i].ReturnTypeNum(), unitsMoves[i].ReturnMoveNum());
            ++i;
        }
        
    }

}
