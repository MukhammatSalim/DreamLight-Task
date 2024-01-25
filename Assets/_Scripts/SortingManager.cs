using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SortingManager : MonoBehaviour
{
    public bool IsSortingAscend;
    bool isSortingNumber;
    public GameObject Content;
    public UnityEngine.UI.Toggle TextToggle;
    public UnityEngine.UI.Toggle NumberToggle;
    List<GameObject> panels = new List<GameObject>();
    public UIManager uIManager;

    private void Start()
    {
        for (int i = 0; i < Content.transform.childCount; i++) panels.Add(Content.transform.GetChild(i).gameObject);
    }
    public void SortPanels()
    {
        Debug.Log("Sorting Started");
        // GetSortType();
        Debug.Log("Type has been found");
        if (isSortingNumber){
            if (IsSortingAscend)
            {
                Debug.Log("The type is: Sort by number in ascending order");
                SortByNumber(IsSortingAscend);
            }else {
                Debug.Log("The type is: Sort by number in Decending order");
                SortByNumber(IsSortingAscend);
            }
        } else if (IsSortingAscend)
            {
                Debug.Log("The type is: Sort by text in ascending order");
                SortByText(IsSortingAscend);
            }else {
                Debug.Log("The type is: Sort by text in Decending order");
                SortByText(IsSortingAscend);
            }
    }

    public void ChangeSortType(UnityEngine.UI.Toggle toggle, bool IsAscend)
    {
        if(toggle == NumberToggle) isSortingNumber = true;
        else isSortingNumber = false;
        IsSortingAscend = IsAscend;
        SortPanels();
        // if ((getToggleArrowUP(NumberToggle.gameObject).activeInHierarchy) || getToggleArrowDOWN(NumberToggle.gameObject).activeInHierarchy)
        // {
        //     isSortingNumber = true;
        //     if (getToggleArrowUP(NumberToggle.gameObject)) IsSortingAscend = true;
        //     else IsSortingAscend = false;
        // }
        // else if ((getToggleArrowUP(TextToggle.gameObject).activeInHierarchy) || getToggleArrowDOWN(TextToggle.gameObject).activeInHierarchy)
        // {
        //     isSortingNumber = false;
        //     if (getToggleArrowUP(TextToggle.gameObject)) IsSortingAscend = true;
        //     else IsSortingAscend = false;
        // }
    }
    public void SortByNumber(bool isAscendOrder)
    {
        DoBubbleSort();
        Debug.Log("Bubble sorted");
    }

    public void SortByText(bool ascendOrder){

    }

    public void Swap(int first, int second)
    {
        panels[first].transform.SetSiblingIndex(panels[second].transform.GetSiblingIndex());

    }

    public void DoBubbleSort()
    {
        for (int i = 0; i < panels.Count - 1; i++)
        {
            for (int j = 0; j < panels.Count - i - 1; j++)
            {
                if (GetPanelNumber(panels[j]) > (GetPanelNumber(panels[j + 1])))
                {
                    Swap(j, j + 1);
                }
            }
        }
    }
    int GetPanelNumber(GameObject panel)
    {
        return Convert.ToInt32(panel.transform.GetChild(1).gameObject.GetComponent<Text>().text);
    }
    GameObject getToggleArrowUP(GameObject toggle)
    {
        return TextToggle.gameObject.transform.GetChild(0).GetChild(0).gameObject;
    }
    GameObject getToggleArrowDOWN(GameObject toggle)
    {
        return TextToggle.gameObject.transform.GetChild(0).GetChild(1).gameObject;
    }


}
