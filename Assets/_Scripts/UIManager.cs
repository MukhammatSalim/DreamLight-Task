using TMPro;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    [Header("Content Generation")]
    [SerializeField] int numberOfItemsToGenerate;

    [Header("Prefabs")]
    public GameObject PanelPrefab;
    public GameObject BlankPanelPrefab;

    [Header("Lists")]
    public Transform LeftContent;
    public Transform RightContent;
    [Header("List Names")]
    public TMP_Text SortingListName;
    public TMP_Text FreeListName;

    [Header("Checks")]
    GameObject BlankPanel;

    public void Regenerate(){
        ClearAllPanels();
        GeneratePanels(PanelPrefab);
        UpdateAllLists();
    }
    private void Start()
    {
        GeneratePanels(PanelPrefab);
        UpdateAllLists();
    }
    void GeneratePanels(GameObject panelPrefab)
    {
        string stBase = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        for (int i = 0; i < numberOfItemsToGenerate; i++)
        {
            GameObject newPanel = Instantiate(PanelPrefab);
            newPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = (Random.Range(0, numberOfItemsToGenerate).ToString());
            newPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = stBase[Random.Range(0, stBase.Length)].ToString();
            newPanel.transform.SetParent(LeftContent);
            newPanel.GetComponent<DragDropUI>().UIManager = gameObject.GetComponent<UIManager>();
        }
        for (int i = 0; i < numberOfItemsToGenerate; i++)
        {
            GameObject newPanel = Instantiate(PanelPrefab);
            newPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = (Random.Range(0, numberOfItemsToGenerate).ToString());
            newPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = stBase[Random.Range(0, stBase.Length)].ToString();
            newPanel.transform.SetParent(RightContent);
            newPanel.GetComponent<DragDropUI>().UIManager = gameObject.GetComponent<UIManager>();
        }
    }
    public void HoverSpaceForNewPanel(GameObject panelToMove)
    {
        if (isViewPort(panelToMove))
        {
            if (BlankPanel == null)
            {
                if (IsLeftContent(panelToMove)) BlankPanel = CreateBlankPanel(LeftContent);
                else BlankPanel = CreateBlankPanel(RightContent);
            }
            else if (IsLeftContent(panelToMove))
            {
                BlankPanel.transform.SetParent(LeftContent);
                BlankPanel.transform.SetSiblingIndex(LeftContent.childCount - 1);
            }
            else
            {
                BlankPanel.transform.SetParent(RightContent);
                BlankPanel.transform.SetSiblingIndex(RightContent.childCount - 1);
            }

        }
        else
        {
            int targetPanelIndex = panelToMove.transform.GetSiblingIndex();

            if (BlankPanel == null)
            {
                if (IsLeftContent(panelToMove)) BlankPanel = CreateBlankPanel(LeftContent);
                else BlankPanel = CreateBlankPanel(RightContent);
            }
            else if (IsLeftContent(panelToMove))
            {
                BlankPanel.transform.SetParent(LeftContent);
                BlankPanel.transform.SetSiblingIndex(targetPanelIndex);
            }
            else
            {
                BlankPanel.transform.SetParent(RightContent);
                BlankPanel.transform.SetSiblingIndex(targetPanelIndex);
            }
            Debug.Log("Blank panel is on sibling index " + targetPanelIndex);
        }
    }

    public bool IsLeftContent(GameObject panel)
    {
        if (panel.transform == LeftContent.parent) return true;
        else if (panel.transform == RightContent.parent) return false;

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
    public bool IsBlankPanel(GameObject panel)
    {
        if (panel == BlankPanel) return true;
        else return false;
    }

    public void AssignToBlankPanel(GameObject panel)
    {
        panel.transform.SetParent(BlankPanel.transform.parent);
        panel.transform.SetSiblingIndex(BlankPanel.transform.GetSiblingIndex());
        UpdateAllLists();
        Destroy(BlankPanel);

    }
    public bool isViewPort(GameObject obj)
    {
        if ((obj.transform == LeftContent.parent) || (obj.transform == RightContent.parent)) return true;
        else return false;

    }
    public void Swap(int first, int second)
    {
        RightContent.transform.GetChild(first).transform.SetSiblingIndex(RightContent.transform.GetChild(second).GetSiblingIndex());

    }
    public void UpdateListName(TMP_Text ListName)
    {
        if (ListName == FreeListName) ListName.text = "Free panels: " + LeftContent.childCount.ToString();
        else ListName.text = "Sorted panels: " + RightContent.childCount.ToString();
    }
    void UpdateAllLists()
    {
        UpdateListName(FreeListName);
        UpdateListName(SortingListName);
    }
    void ClearContent(Transform content)
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    public void ClearAllPanels()
    {
        ClearContent(LeftContent);
        ClearContent(RightContent);

    }
    public void LoadPanel(PanelData panelData)
    {
        GameObject newPanel = Instantiate(PanelPrefab);
        newPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = panelData.PanelText;
        newPanel.transform.GetChild(1).GetComponent<TMP_Text>().text = panelData.PanelNumber;
        newPanel.GetComponent<DragDropUI>().UIManager = gameObject.GetComponent<UIManager>();
        if (panelData.side == "left") newPanel.transform.SetParent(LeftContent);
        else newPanel.transform.SetParent(RightContent);
        newPanel.transform.SetSiblingIndex(panelData.Index);
    }

    public void ExitGame(){
        Application.Quit();
    }

}
