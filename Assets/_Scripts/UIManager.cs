using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class UIManager : MonoBehaviour
{
    [Header("Content Generation")]
    [SerializeField] int numberOfItemsToGenerate;

    [Header("Prefabs")]
    public GameObject PanelPrefab;
    public GameObject BlankPanelPrefab;

    [Header("List related")]
    public Transform LeftContent;
    public Transform RightContent;

    [Header("Checks")]
    GameObject BlankPanel;

    private void Start()
    {
        GeneratePanels(PanelPrefab);
    }
    void GeneratePanels(GameObject panelPrefab)
    {
        for (int i = 0; i < numberOfItemsToGenerate; i++)
        {
            GameObject item_go = Instantiate(PanelPrefab);
            item_go.transform.GetChild(1).GetComponent<TMP_Text>().text = (Random.Range(0, numberOfItemsToGenerate).ToString());
            item_go.transform.SetParent(LeftContent);
            item_go.GetComponent<DragDropUI>().UIManager = gameObject.GetComponent<UIManager>();
        }
    }
    public void HoverSpaceForNewPanel(GameObject panelToMove)
    {
        int targetPanelIndex = panelToMove.transform.GetSiblingIndex();
        if (IsLeftContent(panelToMove)) BlankPanel = CreateBlankPanel(LeftContent);
        else BlankPanel = CreateBlankPanel(RightContent);
        BlankPanel.transform.SetSiblingIndex(targetPanelIndex);
    }

    public bool IsLeftContent(GameObject panel)
    {
        if (panel.transform.parent == LeftContent) return true;
        else return false;
    }
    GameObject CreateBlankPanel(Transform side)
    {
        if (BlankPanel == null)
        {
            BlankPanel = Instantiate(BlankPanelPrefab, side);
        }
        else
        {
            Destroy(BlankPanel);
            BlankPanel = Instantiate(BlankPanelPrefab, side);
        }
        return BlankPanel;
    }
    public bool IsNotBlankPanel(GameObject panel){
        if (panel != BlankPanel) return true;
        else return false;
    }

    public void AssignToBlankPanel(GameObject panel){
        panel.transform.SetParent(BlankPanel.transform.parent);
        panel.transform.SetSiblingIndex(BlankPanel.transform.GetSiblingIndex());
        Destroy(BlankPanel);
    }
}
