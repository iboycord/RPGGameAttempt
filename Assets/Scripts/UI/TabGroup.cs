using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    public TabButton selectedTab;
    public List<GameObject> objectsToSwap;
    int currentTabButtonIndex = 0;

    public PanelGroup panelGroup;

    public TabButton hoverTab;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();
        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if(selectedTab == null || button != selectedTab)
        {
            button.background.sprite = tabHover;
            Debug.Log(button.name + " " + currentTabButtonIndex);
        }
        hoverTab = button;
        
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        if(selectedTab != null)
        {
            selectedTab.Deselect();
        }

        selectedTab = button;
        selectedTab.Select();
        ResetTabs();

        button.background.sprite = tabActive;
        int index = button.transform.GetSiblingIndex();
        currentTabButtonIndex = index;
        for(int i = 0; i < objectsToSwap.Count; ++i)
        {
            if(i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }

        if(panelGroup != null)
        {
            panelGroup.SetPageIndex(index);
        }
    }

    public void OnMenuExit()
    {
        selectedTab.Deselect();
        selectedTab = null;
        for (int i = 0; i < objectsToSwap.Count; ++i)
        {
            objectsToSwap[i].SetActive(false);
        }
        ResetTabs();
    }

    public void ResetTabs()
    {
        foreach(TabButton button in tabButtons)
        {
            if(selectedTab != null && button == selectedTab) { continue; }
            button.background.sprite = tabIdle;
        }
    }

    public void Update()
    {
        Debug.DrawRay(transform.position, transform.forward, Color.white);
        if (selectedTab == null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                /*
                if ((currentTabButtonIndex - 1) < 0)
                {
                    OnTabSelected(tabButtons[tabButtons.Count - 1]);
                }
                if ((currentTabButtonIndex - 1) >= 0)
                {
                    OnTabSelected(tabButtons[currentTabButtonIndex - 1]);
                }
                */

                if (currentTabButtonIndex - 1 > -1)
                {
                    currentTabButtonIndex--;
                    OnTabEnter(tabButtons[currentTabButtonIndex]);
                }
                else
                {
                    currentTabButtonIndex = tabButtons.Count - 1;
                    OnTabEnter(tabButtons[currentTabButtonIndex]);
                }

            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                /*
                if ((currentTabButtonIndex + 1) >= tabButtons.Count)
                {
                    OnTabSelected(tabButtons[0]);
                }
                if ((currentTabButtonIndex + 1) < tabButtons.Count)
                {
                    OnTabSelected(tabButtons[currentTabButtonIndex + 1]);

                }
                */

                if (currentTabButtonIndex + 1 < tabButtons.Count)
                {
                    currentTabButtonIndex++;
                    OnTabEnter(tabButtons[currentTabButtonIndex]);
                }
                else
                {
                    currentTabButtonIndex = 0;
                    OnTabEnter(tabButtons[currentTabButtonIndex]);
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.Space) && hoverTab != null)
        {
            OnTabSelected(hoverTab);
        }
        if (Input.GetKeyDown(KeyCode.I) && selectedTab != null)
        {
            OnMenuExit();
        }
    }

}
