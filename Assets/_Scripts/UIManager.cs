using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    [SerializeField] int maxNumber;
    [SerializeField] GameObject leftContent;
    [SerializeField] GameObject RightContent;
    [SerializeField] Transform Canvas;
    public GameObject PanelPrefab;
    public GameObject BlankPanelPrefab;
    [SerializeField] List<GameObject> leftList;
    [SerializeField] List<GameObject> rightList;

    private void Awake()
    {
        GenerateContent(maxNumber);
        RandomizeContent(maxNumber); //Позже убрать на кнопку
    }

    public void RandomizeContent(int number)
    {
        foreach (GameObject element in leftList)
        {
            TMP_Text _numberToChange;
            GameObject _elementNumber = element.transform.GetChild(1).gameObject;
            _numberToChange = _elementNumber.GetComponent<TMP_Text>();
            _numberToChange.text = (Random.Range(0, number)).ToString();
        }
        foreach (GameObject element in rightList)
        {
            TMP_Text _numberToChange;
            GameObject _elementNumber = element.transform.GetChild(1).gameObject;
            _numberToChange = _elementNumber.GetComponent<TMP_Text>();
            _numberToChange.text = (Random.Range(0, number)).ToString();
        }
    }
    public void GenerateContent(int number)
    {
        for (int i = 0; i < number; i++)
        {
            CreateElement(PanelPrefab, leftContent.transform, leftList);
            CreateElement(PanelPrefab, RightContent.transform, rightList);
        }
    }

    void CreateElement(GameObject prefab, Transform parent, List<GameObject> list)
    {
        GameObject NewElement = Instantiate(prefab, parent);
        DragNDrop NewElementDND = NewElement.GetComponent<DragNDrop>();
        NewElementDND.Canvas = Canvas;
        NewElementDND.EmptyElementPrefab = BlankPanelPrefab;
        list.Add(NewElement);
    }
}
