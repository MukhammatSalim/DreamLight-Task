using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] int maxNumber;
    [SerializeField] GameObject[] _interactiveElements;
    [SerializeField] GameObject leftViewPort;
    [SerializeField] GameObject RightViewPort;
    public GameObject InteractableUIElement;

    private void Awake()
    {
        _interactiveElements = GameObject.FindGameObjectsWithTag("UI_Element");
        GenerateContent(maxNumber);
        RandomizeContent(); //Позже убрать на кнопку
    }

    public void RandomizeContent()
    {
        foreach (GameObject element in _interactiveElements)
        {
            TMP_Text _numberToChange;
            GameObject _elementText = element.transform.GetChild(0).gameObject;
            GameObject _elementNumber = element.transform.GetChild(1).gameObject;
            _numberToChange = _elementNumber.GetComponent<TMP_Text>();
            _numberToChange.text = (Random.Range(0,maxNumber)).ToString();
        }
    }
    public void GenerateContent(int number){
        for (int i = 0; i < number; i++){
            Instantiate(InteractableUIElement, leftViewPort.transform);
            Instantiate(InteractableUIElement, RightViewPort.transform);

        }
    }
}
