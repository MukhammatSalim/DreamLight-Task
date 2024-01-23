using TMPro;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    [SerializeField] int maxNumber;
    [SerializeField] GameObject[] _interactiveElements;
    [SerializeField] GameObject leftContent;
    [SerializeField] GameObject RightContent;
    [SerializeField] Transform Canvas;
    public GameObject InteractableUIElement;
    public GameObject EmptyUIElement;

    private void Awake()
    {
        GenerateContent(maxNumber);
        _interactiveElements = GameObject.FindGameObjectsWithTag("UI_Element");
        RandomizeContent(maxNumber); //Позже убрать на кнопку
    }

    public void RandomizeContent(int number)
    {
        foreach (GameObject element in _interactiveElements)
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
            CreateElement(InteractableUIElement, leftContent.transform, Canvas);
            CreateElement(InteractableUIElement, RightContent.transform, Canvas);

        }
    }

    void CreateElement(GameObject prefab, Transform parent, Transform mainCanvas)
    {
        GameObject NewElement = Instantiate(prefab, parent);
        DragNDrop NewElementDND = NewElement.GetComponent<DragNDrop>();
        NewElementDND.Canvas = mainCanvas;
        NewElementDND.EmptyElementPrefab = EmptyUIElement;
    }
}
