using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Tab
{
    public TabButton tabButton;
    public GameObject objectToSwap;
    public bool isStandart;
    [SerializeField] private bool _isEnebled;
    public bool isEnebled => _isEnebled;
    public void SetEnebled(bool state)
    {
        if (!isStandart)
        {
            _isEnebled = state;
            TabGroup.instance.ResetTabs();
        }
    }
}

public class TabGroup : MonoBehaviour
{
    public static TabGroup instance;

    public List<Tab> tabs;
    public TabButton selectedTab;
    public Sprite tabLocked;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;

    private void Start()
    {
        instance = this;
        OnTabSelected(selectedTab);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if ((selectedTab == null || button != selectedTab) && tabs.Find(x => x.tabButton == button).isEnebled)
        {
            button.background.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        if (tabs.Find(x => x.tabButton == button).isEnebled)
        {
            if (selectedTab != null)
            {
                selectedTab.Deselect();
            }

            selectedTab = button;

            selectedTab.Select();

            ResetTabs();
            button.background.sprite = tabActive;
            int index = button.transform.GetSiblingIndex();
            for (int i = 0; i < tabs.Count; i++)
            {
                if (i == index)
                {
                    tabs[i].objectToSwap.SetActive(true);
                }
                else
                {
                    tabs[i].objectToSwap.SetActive(false);
                }
            }
        }
    }

    public void ResetTabs()
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            if (selectedTab != null && tabs[i].tabButton == selectedTab) { continue; }
            if (tabs[i].isEnebled)
            {
                tabs[i].tabButton.background.sprite = tabIdle;
            }
            else
            {
                tabs[i].tabButton.background.sprite = tabLocked;
            }
        }
    } 
}
