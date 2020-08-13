using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    private List<CharacterStats> lineup;

    [SerializeField]
    private GameObject actionsMenu, enemySelectMenu;

    public int turnCount = 0;
    [HideInInspector]
    public int unitNum;

    private void Start()
    {
        lineup = new List<CharacterStats>();
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerControlled");
        foreach (GameObject playerUnit in playerUnits)
        {
            CharacterStats currentUnit = playerUnit.GetComponent<CharacterStats>();
            currentUnit.calcNextActTurn(0);
            lineup.Add(currentUnit);
        }

        GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("EnemyControlled");
        foreach (GameObject enemyUnit in enemyUnits)
        {
            CharacterStats currentUnit = enemyUnit.GetComponent<CharacterStats>();
            currentUnit.calcNextActTurn(0);
            lineup.Add(currentUnit);
        }

        lineup.Sort();

        unitNum = lineup.Count;

        NextTurn();
    }

    // Something something fix it to be able to keep track of the overall turn as well.
    //  So maybe when everyone acts at least once, that is a turn.
    public void NextTurn()
    {
        CharacterStats currentUnit = lineup[0];
        lineup.Remove(currentUnit);

        if (!currentUnit.isDead)
        {
            if(currentUnit == lineup[0])
            {
                turnCount++; //Something illogical is happening here
            }

            GameObject currentUnitGO = currentUnit.gameObject;

            currentUnit.calcNextActTurn(currentUnit.nextActTurn);
            lineup.Add(currentUnit);
            lineup.Sort();

            if(currentUnitGO.tag == "PlayerControlled")
            {
                Debug.Log("Player Go!");
            }
            if(currentUnitGO.tag == "EnemyControlled")
            {
                Debug.Log("Enemy go...");
            }
        }
        else
        {
            NextTurn();
        }

    }

}
