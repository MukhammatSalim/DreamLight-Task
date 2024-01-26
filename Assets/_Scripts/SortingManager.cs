using System;
using TMPro;
using UnityEngine;

public class SortingManager : MonoBehaviour
{
    [Header("Sorting Type")]
    bool IsAscendOrder;
    bool isSortingNumber;
    [Header("Connections")]
    public GameObject Content;
    public UnityEngine.UI.Toggle TextToggle;
    public UnityEngine.UI.Toggle NumberToggle;
    public UIManager uIManager;
    [Header("Debug mode")]
    public bool DebugMode;

    public void SortPanels()
    {
        Debug.Log("Sorting Started");
        if (isSortingNumber)
        {
            if (IsAscendOrder)
            {
                if (DebugMode) Debug.Log("The type is: Sort by number in ascending order");
                SortByNumber(IsAscendOrder);
            }
            else
            {
                if (DebugMode) Debug.Log("The type is: Sort by number in Decending order");
                SortByNumber(IsAscendOrder);
            }
        }
        else if (IsAscendOrder)
        {
            if (DebugMode) Debug.Log("The type is: Sort by text in ascending order");
            SortByText(IsAscendOrder);
        }
        else
        {
            if (DebugMode) Debug.Log("The type is: Sort by text in Decending order");
            SortByText(IsAscendOrder);
        }
    }

    public void ChangeSortType(UnityEngine.UI.Toggle toggle, bool isAscend)
    {
        if (toggle == NumberToggle) isSortingNumber = true;
        else isSortingNumber = false;
        IsAscendOrder = isAscend;
        SortPanels();
    }
    public void SortByNumber(bool isAscendOrder)
    {
        DoNumberBubbleSort(isAscendOrder);
        if (DebugMode) Debug.Log("Bubble sorted");
    }

    public void SortByText(bool isAscendOrder)
    {
        DoTextBubbleSort(isAscendOrder);
    }


    public void DoNumberBubbleSort(bool isAscendOrder)
    {
        if (isAscendOrder)
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
    public void DoTextBubbleSort(bool isAscendOrder)
    {
        if (isAscendOrder)
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
}
