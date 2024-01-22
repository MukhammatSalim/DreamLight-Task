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
            _numberToChange.text = (Random.Range(0,number)).ToString();
        }
    }
    public void GenerateContent(int number){
        for (int i = 0; i < number; i++){
            Instantiate(InteractableUIElement, leftViewPort.transform);
            Instantiate(InteractableUIElement, RightViewPort.transform);

        }
    }
}
