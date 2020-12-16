using System.Collections;
using System.Collections.Generic;
//using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public List<GameObject> objectsToSwap;
    //int currentTabButtonIndex = -1;

    public MenuState subMenuState = MenuState.Unopened;
    [Tooltip("0 = Attack, 1 = Special, 2 = Items, 3 = Tactics")]
    public List<SubMenuContents> subMenus;
    public SubMenuContents currentSubMenu;
    public int sMtoClose;
    public TargetingSubMenu targetingSubMenu;

    public bool atkTabAvailable = true;
    public bool speTabAvailable = true;
    public bool itmTabAvailable = true;
    public bool tacTabAvailable = true;

    public BattleSystem battleSystem;
    public BattleAnimationHandler animationHandler;
    public UnitMoveList currentUnitsMoves;

    bool canAct = true;

    public void InitializeTabs(bool atk, bool spe, bool itm, bool tac)
    {
        atkTabAvailable = atk;
        speTabAvailable = spe;
        itmTabAvailable = itm;
        tacTabAvailable = tac;
    }

    public void ResetBattleTabStatus()
    {
        atkTabAvailable = true;
        speTabAvailable = true;
        itmTabAvailable = true;
        tacTabAvailable = true;
    }

    public void Update()
    {
        if(subMenuState == MenuState.Targeting && canAct)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                targetingSubMenu.IncrementTargetList();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                targetingSubMenu.DecrementTargetList();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //OpenTargetMenu();
                //subMenuState = MenuState.Acting; //Is this part needed?
                canAct = false;
                //battleSystem.OnStandardAttackButton(battleSystem.PlaceInLineup(), targetingSubMenu.SelectTarget(), currentSubMenu.SelectMove());
                EndUIFeatures();
                //animationHandler.enabled = true;
                //animationHandler.InitializeHandler(battleSystem.PlaceInLineup(), targetingSubMenu.SelectTarget(), currentSubMenu.SelectMove());
            }
            if (Input.GetKey(KeyCode.I))
            {
                StartCoroutine(CloseTargetMenu());
            }
        }

        if(subMenuState == MenuState.Selecting && canAct)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if(currentSubMenu.listType == ListType.Moves)
                {
                    Debug.Log(currentSubMenu.DecrementMoveList().name);
                }
                if (currentSubMenu.listType == ListType.Items)
                {
                    Debug.Log(currentSubMenu.DecrementItemList().name);
                }
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (currentSubMenu.listType == ListType.Moves)
                {
                    Debug.Log(currentSubMenu.IncrementMoveList().name);
                }
                if (currentSubMenu.listType == ListType.Items)
                {
                    Debug.Log(currentSubMenu.IncrementItemList().name);
                }
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                OpenTargetMenu();
            }
            if (Input.GetKey(KeyCode.I))
            {
                StartCoroutine(CloseSubMenu());
            }
        }
        
        if(subMenuState == MenuState.Unopened && canAct)
        {
            // 0 = Attack, 1 = Special, 2 = Items, 3 = Tactics
            if (Input.GetKeyDown(KeyCode.W) && atkTabAvailable)
            {
                OpenSubMenu(0);
            }
            if (Input.GetKeyDown(KeyCode.D) && speTabAvailable)
            {
                OpenSubMenu(1); // If the list is empty then an error is thrown. Please fix
            }
            if (Input.GetKeyDown(KeyCode.S) && itmTabAvailable)
            {
                OpenSubMenu(2);
            }
            if (Input.GetKeyDown(KeyCode.A) && tacTabAvailable)
            {
                OpenSubMenu(3);
            }
        }

    }

    public void OpenBUI(UnitMoveList passed)
    {
        canAct = true;
        gameObject.SetActive(true);
        currentUnitsMoves = passed;
    }
    public void CloseBUI()
    {
        currentSubMenu.OnExit();
        SubMenuReset();
        ResetBattleTabStatus();
        gameObject.SetActive(false);
        currentUnitsMoves = null;
    }


    public void OpenSubMenu(int menuToOpen)
    {
        sMtoClose = menuToOpen;
        objectsToSwap[menuToOpen].SetActive(true);
        if(menuToOpen < 2)
        {
            if (menuToOpen == 0) { currentSubMenu.ListSetup(currentUnitsMoves, true); }
            if (menuToOpen == 1) { currentSubMenu.ListSetup(currentUnitsMoves, false); }
        }
        subMenuState = MenuState.Selecting;
    }
    public IEnumerator CloseSubMenu()
    {
        canAct = false;
        currentSubMenu.OnExit();
        for (int i = 0; i < objectsToSwap.Count; ++i)
        {
            objectsToSwap[i].SetActive(false);
        }
        yield return new WaitForSeconds(0.2f);
        subMenuState = MenuState.Unopened;
        canAct = true;
    }


    public void OpenTargetMenu()
    {
        subMenuState = MenuState.Targeting;
        targetingSubMenu.gameObject.SetActive(true);
        targetingSubMenu.ShowUI();
    }
    public IEnumerator CloseTargetMenu()
    {
        canAct = false;
        targetingSubMenu.OnExit();
        targetingSubMenu.gameObject.SetActive(false);
        targetingSubMenu.HideUI();
        yield return new WaitForSeconds(0.2f);
        subMenuState = MenuState.Selecting;
        canAct = true;
    }
    public void AddToTargetMenu(CharacterStats targetToAdd)
    {
        targetingSubMenu.AddTarget(targetToAdd);
    }
    public void SetupTargetMenu(CharacterStats Ptarget1, CharacterStats Ptarget2, CharacterStats[] targetArray)
    {
        targetingSubMenu.Setup(Ptarget1, Ptarget2, targetArray);
    }


    public void SubMenuReset()
    {
        targetingSubMenu.OnExit();
        targetingSubMenu.gameObject.SetActive(false);
        currentSubMenu.OnExit();
        objectsToSwap[sMtoClose].SetActive(false);
        subMenuState = MenuState.Unopened;
    }

    public void EndUIFeatures()
    {
        animationHandler.enabled = false;
        battleSystem.OnStandardAttackButton(battleSystem.PlaceInLineup(), targetingSubMenu.SelectTarget(), currentSubMenu.SelectMove());
    }

}

public enum MenuState { Unopened, Selecting, Targeting, Info, Acting, NotMyTurn }
