﻿using System.Collections;
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

    public EnemyBattleDataStruct[] enemies;
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

    public float timeToWait;

    public bool currentUnitHasExtraTurn = false;

    public BattleUI battleUI;

    public FriendshipControl friendshipControl;
    public BattleAnimationHandler animationHandler;


    private void Start()
    {
        if (friendshipControl == null) { friendshipControl = GetComponent<FriendshipControl>(); }

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

        foreach (EnemyBattleDataStruct enemyUnit in enemies)
        {
            CharacterStats currentUnit = enemyUnit.enemyUnit;
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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            friendshipControl.PrintData();
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
        NextPhase();
    }

    public void NextPhase()
    {
        if (placeInLineup < lineup.Count)
        {
            CharacterStats currentUnit = lineup[placeInLineup];
            StatusEffectHandler currentSEH = currentUnit.GetComponent<StatusEffectHandler>();

            currentSEH.TurnEffects();
            bool canAct = true;
            if (currentSEH.currentStatusEffect)
            {
                canAct = !currentSEH.currentStatusEffect.QuickLostTurnCheck();
            }

            // Decide if you want to require a unit to be able to act before getting an extra turn
            // if yes then put this into the if statement below it, if not then leave it.
            if (currentSEH.ExtraTurnCheck())
            {
                currentUnitHasExtraTurn = true;
                Debug.Log("Extra Turn");
                currentSEH.ExtraTurnIncrementer(-1);
            }

            if (!currentUnit.isDead && !currentSEH.participatedInDuoMove && canAct)
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

            else
            {
                FinishTurn();
            }

        }
        else
        {
            ReshuffleLineup();
        }
    }

    public void ReshuffleLineup()
    {
        lineup.Sort();
        placeInLineup = 0;

        turnNum += 1;
        Debug.Log("Turn Number " + turnNum);
        NextPhase();
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
    }

    public void EnemyTurn(Unit currentUnit)
    {
        state = BattleState.ENEMYTURN;
        Debug.Log(currentUnit.name + "'s turn...");

        // Attack with best damaging move/use best tactic then go to next turn

        StartCoroutine(EndMyTurn(timeToWait));
    }

    // == UI Functions == //

    public void OnStandardAttackButton(UIMoveHolder button)
    {
        if (state != BattleState.PLAYERTURN)
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
        //Attack is just debug for general attacks. It cant understand other things like status or vampireism but it knows weaknesses.
        Attack(currentUnit, target, move);
        //move.PlayAnimation(currentUnit.GetComponent<Animator>());
        move.Use(currentUnit, target);
        //move.CompleteAttackAnimation(currentUnit, target, animationHandler.ReturnAnimMulti());

        if (currentUnit.CompareTag("PlayerControlled") && !currentUnit.GetComponent<StatusEffectHandler>().sealedHeart)
        {
            //Something something move this whole thing to Duo Moves only, but there could be moves that just increase friendship.
            friendshipControl.IncrementFriendship(move.baseFriendshipGiven);
        }

        battleUI.CloseBUI();
        FinishTurn();
    }

    public void OnSpecialAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        Debug.Log("I think letting the player scroll down a list, selecting the one they want, then passing it to the attack function might work.");
    }

    public void FinishTurn()
    {
        StartCoroutine(EndMyTurn(timeToWait));
    }

    IEnumerator EndMyTurn(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);

        if (!currentUnitHasExtraTurn)
        {
            placeInLineup++;
        }
        currentUnitHasExtraTurn = false;
        NextPhase();
    }

    public void Attack(CharacterStats currentUnit, CharacterStats target, Move move)
    {
        // Can desync with the actual value due to being two seperate calls.
        //Debug.Log(currentUnit.name + " hit " + target.name + " with " + move.name + " for a supposed " + move.SoloDamageFormula(currentUnit, target));
        Debug.Log(currentUnit.name + " attacks! ");
        if (target.EType2 != ElementalTyping.None)
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
