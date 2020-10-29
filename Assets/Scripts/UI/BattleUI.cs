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
    public TargetingSubMenu targetingSubMenu;

    public PanelGroup panelGroup;

    public BattleSystem battleSystem;
    public BattleAnimationHandler animationHandler;

    bool canAct = true;

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
            if (Input.GetKeyDown(KeyCode.W))
            {
                OpenSubMenu(0);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                OpenSubMenu(1);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                OpenSubMenu(2);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                OpenSubMenu(3);
            }
        }

    }

    public void OpenBUI()
    {
        canAct = true;
        gameObject.SetActive(true);
    }
    public void CloseBUI()
    {
        SubMenuReset();
        gameObject.SetActive(false);
    }


    public void OpenSubMenu(int menuToOpen)
    {
        currentSubMenu = subMenus[menuToOpen];
        currentSubMenu.gameObject.SetActive(true);
        subMenuState = MenuState.Selecting;
    }
    public IEnumerator CloseSubMenu()
    {
        canAct = false;
        currentSubMenu.OnExit();
        for (int i = 0; i < subMenus.Count; ++i)
        {
            subMenus[i].gameObject.SetActive(false);
        }
        currentSubMenu = null;
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
        currentSubMenu.gameObject.SetActive(false);
        subMenuState = MenuState.Unopened;
    }

    public void EndUIFeatures()
    {
        animationHandler.enabled = false;
        battleSystem.OnStandardAttackButton(battleSystem.PlaceInLineup(), targetingSubMenu.SelectTarget(), currentSubMenu.SelectMove());
    }

}

public enum MenuState { Unopened, Selecting, Targeting, Info, Acting, NotMyTurn }
