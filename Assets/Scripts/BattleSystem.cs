using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, FLEE }

[RequireComponent(typeof(StatusEffectComboChart))]
public class BattleSystem : MonoBehaviour
{
    //https://gamedevacademy.org/how-to-create-an-rpg-game-in-unity-comprehensive-guide/

    public int turnNum;
    private List<CharacterStats> lineup;

    [SerializeField]
    private GameObject actionsMenu, enemySelectMenu;

    [Space]
    [Header("Player Stuff")]
    public GameObject playerPrefabA;
    public GameObject playerPrefabB;

    public Transform playerAStartArea;
    public Transform playerBStartArea;

    public CharacterStats playerAUnit;
    public CharacterStats playerBUnit;

    [Space]
    [Header("Enemy Stuff")]

    public GameObject enemyPrefabA;
    public CharacterStats enemyAUnit;
        /*
    public GameObject enemyPrefabB;
    public GameObject enemyPrefabC;
    public GameObject enemyPrefabD;
    public GameObject enemyPrefabE;
    public GameObject enemyPrefabF;
    public GameObject enemyPrefabG;
    public GameObject enemyPrefabH;

    public Transform enemyAStartArea;
    public Transform enemyBStartArea;
    public Transform enemyCStartArea;
    public Transform enemyDStartArea;
    public Transform enemyEStartArea;
    public Transform enemyFStartArea;
    public Transform enemyGStartArea;
    public Transform enemyHStartArea;

    
    public CharacterStats enemyBUnit;
    public CharacterStats enemyCUnit;
    public CharacterStats enemyDUnit;
    public CharacterStats enemyEUnit;
    public CharacterStats enemyFUnit;
    public CharacterStats enemyGUnit;
    public CharacterStats enemyHUnit;

    https://stackoverflow.com/questions/49186166/unity-how-to-instantiate-a-prefab-by-string-name-to-certain-locationhttps://stackoverflow.com/questions/49186166/unity-how-to-instantiate-a-prefab-by-string-name-to-certain-location
    https://docs.unity3d.com/ScriptReference/GameObject.Find.html

    */

    //public BattleHUD playerHUD;

    public BattleState state;
    private int placeInLineup;

    public bool moveSelected = false;

    public bool battleDecided;

    GameObject[] enemyUnits;

    public float timeToWait;

    public BattleUI battleUI;

    private void Start()
    {
        state = BattleState.START;

        StartCoroutine(BattleSetup());
    }

    public void GatherAndCompare()
    {
        //Determine if you want to swap out player characters in battle. Currently
        // as of 5/15/2020 Im leaning towards no so I'll just comment this out. 

        /*
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerControlled");
        foreach (GameObject playerUnit in playerUnits)
        {
            Unit currentUnit = playerUnit.GetComponent<Unit>();
            //currentUnit.calcNextActTurn(0);
            lineup.Add(currentUnit);
        }
        */
        lineup.Add(playerAUnit.GetComponent<CharacterStats>());
        battleUI.AddToTargetMenu(playerAUnit.GetComponent<CharacterStats>());

        lineup.Add(playerBUnit.GetComponent<CharacterStats>());
        battleUI.AddToTargetMenu(playerBUnit.GetComponent<CharacterStats>());


        enemyUnits = GameObject.FindGameObjectsWithTag("EnemyControlled");
        foreach (GameObject enemyUnit in enemyUnits)
        {
            CharacterStats currentUnit = enemyUnit.GetComponent<CharacterStats>();
            //currentUnit.calcNextActTurn(0);
            if (currentUnit != null || !currentUnit.isDead)
            {
                lineup.Add(currentUnit);
                battleUI.AddToTargetMenu(currentUnit);
            }
        }

        lineup.Sort();
    }

    private void Update()
    {
        //Check to see if someone has died (both the player characters or the enemies)

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerAUnit.gameObject.GetComponent<StatusEffectHandler>().AssignStatus(StatusEffectList.Burning);
        }

    }

    IEnumerator BattleSetup()
    {
        // Create everything here, like the player characters and the enemies that stand
        //  in a specific position. The HUD. ext.

        playerAUnit = playerPrefabA.GetComponent<CharacterStats>();
        playerBUnit = playerPrefabB.GetComponent<CharacterStats>();

        lineup = new List<CharacterStats>();
        GatherAndCompare();

        yield return new WaitForSeconds(0.5f);

        turnNum = 1;
        //NextPersonInLine();
        NextPhase();
    }

    public void NextPersonInLine()
    {
        /*
        if(position <= lineup.Count)
        {
            CharacterStats currentUnit = lineup[position];

            if (!currentUnit.isDead)
            {
                GameObject currentUnitGO = currentUnit.gameObject;

                currentUnit.calcNextActTurn(currentUnit.nextActTurn);
                lineup.Add(currentUnit);
                lineup.Sort();
                moveSelected = false;

                if (currentUnitGO.tag == "PlayerControlled")
                {
                    Debug.Log("Player Go!");
                    PlayerTurn(currentUnit);
                }
                if (currentUnitGO.tag == "EnemyControlled")
                {
                    Debug.Log("Enemy go...");
                    EnemyTurn(currentUnit);
                }
            }
        }
        */

        for(int i = 0; i < lineup.Count; ++i)
        {
            CharacterStats currentUnit = lineup[i];

            if (!currentUnit.isDead)
            {
                GameObject currentUnitGO = currentUnit.gameObject;

                //currentUnit.calcNextActTurn(currentUnit.nextActTurn);
                //lineup.Add(currentUnit);
                //lineup.Sort();
                //moveSelected = false;

                if (currentUnitGO.CompareTag("PlayerControlled"))
                {
                    Debug.Log("Player Go!");
                    PlayerTurn(currentUnit);
                }
                if (currentUnitGO.CompareTag("EnemyControlled"))
                {
                    Debug.Log("Enemy go...");
                    EnemyTurn(currentUnit);
                }
            }
        }

        Debug.Log("Turn Number " + turnNum.ToString());

        ReshuffleLineup();

        /*
        else
        {
            ReshuffleLineup();
        }
        */
    }

    public void NextPhase()
    {
        if (placeInLineup < lineup.Count)
        {
            CharacterStats currentUnit = lineup[placeInLineup];
            StatusEffectHandler currentSEH = currentUnit.GetComponent<StatusEffectHandler>();

            currentSEH.TurnEffects();

            if (!currentUnit.isDead)
            {
                currentUnit.ResetDMGRedux();
                GameObject currentUnitGO = currentUnit.gameObject;

                moveSelected = false;

                if (currentUnitGO.CompareTag("PlayerControlled"))
                {
                    Debug.Log("Player Go!");
                    PlayerTurn(currentUnit);
                }
                if (currentUnitGO.CompareTag("EnemyControlled"))
                {
                    Debug.Log("Enemy go...");
                    EnemyTurn(currentUnit);
                }
            }
        }
        else
        {
            ReshuffleLineup();
        }
    }

    public void ReshuffleLineup()
    {
        //lineup.Clear();
        //lineup.RemoveRange(0, lineup.Count - 1);

        lineup.Sort();
        placeInLineup = 0;

        turnNum += 1;
        Debug.Log("Turn Number " + turnNum);
        NextPhase();
        //GatherAndCompare();
    }

    public CharacterStats PlaceInLineup()
    {
        return lineup[placeInLineup];
    }

    public void ReallocateAfterBattle()
    {
        lineup.Clear();
        lineup.TrimExcess();
    }

    public void PlayerTurn(Unit currentUnit)
    {
        state = BattleState.PLAYERTURN;
        Debug.Log(currentUnit.name + "'s turn!");

        // Open Menu
        battleUI.OpenBUI();
        //NextPersonInLine(posForLater + 1);

    }



    public void EnemyTurn(Unit currentUnit)
    {
        state = BattleState.ENEMYTURN;
        Debug.Log(currentUnit.name + "'s turn...");

        // Attack with best damaging move/use best tactic then go to next turn

        //NextPersonInLine(posForLater + 1);
        StartCoroutine(EndMyTurn(timeToWait));
    }

    // == UI Functions == //

    public void OnStandardAttackButton(UIMoveHolder button)
    {
        if(state != BattleState.PLAYERTURN)
        {
            return;
        }
        //Debug.Log("Um, figure out how to get an attack though");
        Attack(lineup[placeInLineup], enemyAUnit, button.GetMove());
        actionsMenu.SetActive(false);
        StartCoroutine(EndMyTurn(timeToWait));
    }

    public void OnStandardAttackButton(CharacterStats currentUnit, CharacterStats target, Move move)
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        Attack(currentUnit, target, move);
        battleUI.CloseBUI();
        StartCoroutine(EndMyTurn(timeToWait));
    }

    public void OnSpecialAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        Debug.Log("I think letting the player scroll down a list, selecting the one they want, then passing it to the attack function might work.");
    }

    IEnumerator EndMyTurn(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        placeInLineup++;
        NextPhase();
    }

    public void Attack(CharacterStats currentUnit, CharacterStats target, Move move)
    {
        Debug.Log(currentUnit.name + " hit " + target.name + " with " + move.name + " for a supposed " + move.SoloDamageFormula(currentUnit, target));
        if(target.EType2 != ElementalTyping.None)
        {
            Debug.Log("Target has weakness of " + WeaknessChart.GetEffective(move.elementType, target.EType1, target.EType2)); 
        }
        else
        {
            Debug.Log("Target has weakness of " + WeaknessChart.GetEffective(move.elementType, target.EType1));
        }
        
    }

    public void Defend(CharacterStats currentUnit)
    {

    }

    public void Charge(CharacterStats currentUnit)
    {

    }

    public void Item(CharacterStats currentUnit, CharacterStats target, Item item)
    {

    }

    public void flee(CharacterStats currentUnit)
    {

    }
}
