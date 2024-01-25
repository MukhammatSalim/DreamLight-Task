using System;
using System.Collections.Generic;
using TMPro;
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
    public UIManager uIManager;
    public bool DebugMode;

    public void SortPanels()
    {
        Debug.Log("Sorting Started");
        if (isSortingNumber)
        {
            if (IsSortingAscend)
            {
                if (DebugMode) Debug.Log("The type is: Sort by number in ascending order");
                SortByNumber(IsSortingAscend);
            }
            else
            {
                if (DebugMode) Debug.Log("The type is: Sort by number in Decending order");
                SortByNumber(IsSortingAscend);
            }
        }
        else if (IsSortingAscend)
        {
            if (DebugMode) Debug.Log("The type is: Sort by text in ascending order");
            SortByText(IsSortingAscend);
        }
        else
        {
            if (DebugMode) Debug.Log("The type is: Sort by text in Decending order");
            SortByText(IsSortingAscend);
        }
    }

    public void ChangeSortType(UnityEngine.UI.Toggle toggle, bool IsAscend)
    {
        if (toggle == NumberToggle) isSortingNumber = true;
        else isSortingNumber = false;
        IsSortingAscend = IsAscend;
        SortPanels();
    }
    public void SortByNumber(bool isAscendOrder)
    {
        DoNumberBubbleSort(isAscendOrder);
        if (DebugMode) Debug.Log("Bubble sorted");
    }

    public void SortByText(bool ascendOrder)
    {
        DoTextBubbleSort(ascendOrder);
    }


    public void DoNumberBubbleSort(bool ascendOrder)
    {
        if (ascendOrder)
        {
            for (int i = 0; i < Content.transform.childCount - 1; i++)
            {
                for (int j = 0; j < Content.transform.childCount - i - 1; j++)
                {
                    if (GetPanelNumber(j) > (GetPanelNumber(j + 1)))
                    {
                        uIManager.Swap(j, j + 1);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < Content.transform.childCount - 1; i++)
            {
                for (int j = 0; j < Content.transform.childCount - i - 1; j++)
                {
                    if (GetPanelNumber(j) < (GetPanelNumber(j + 1)))
                    {
                        uIManager.Swap(j, j + 1);
                    }
                }
            }
        }
    }
    public void DoTextBubbleSort(bool ascendOrder)
    {
        if (ascendOrder)
        {
            for (int i = 0; i < Content.transform.childCount - 1; i++)
            {
                for (int j = 0; j < Content.transform.childCount - i - 1; j++)
                {
                    if (String.Compare(GetPanelText(j), GetPanelText(j+1)) == -1)
                    {
                        uIManager.Swap(j, j + 1);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < Content.transform.childCount - 1; i++)
            {
                for (int j = 0; j < Content.transform.childCount - i - 1; j++)
                {
                    if (String.Compare(GetPanelText(j), GetPanelText(j+1)) == 1)
                    {
                        uIManager.Swap(j, j + 1);
                    }
                }
            }
        }
    }
    int GetPanelNumber(int panelIndex)
    {
        return Convert.ToInt32(Content.transform.GetChild(panelIndex).GetChild(1).gameObject.GetComponent<TMP_Text>().text);
    }
    string GetPanelText(int panelIndex){
        return Content.transform.GetChild(panelIndex).GetChild(0).gameObject.GetComponent<TMP_Text>().text;
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
