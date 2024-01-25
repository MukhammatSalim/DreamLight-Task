using UnityEngine;
using UnityEngine.UI;

public class DynamicCheckmark : MonoBehaviour
{
    public GameObject ArrowUp;
    public GameObject ArrowDown;
    public Toggle AnotherToggle;
    public SortingManager sortingManager;

    public void Press()
    {
        if (ArrowDown.activeInHierarchy || ArrowUp.activeInHierarchy) 
        {
            AnotherToggle.gameObject.GetComponent<DynamicCheckmark>().HideAllArrows();
            ReverseArrows();
        }

        else if (gameObject.GetComponent<Toggle>().isOn == true)
        {
            ArrowUp.SetActive(true);
        }
        else ArrowDown.SetActive(true);

        AnotherToggle.gameObject.GetComponent<DynamicCheckmark>().HideAllArrows();
        ChangeSortType();
    }
    public void ReverseArrows()
    {
        ReverseActiveValue(ArrowDown);
        ReverseActiveValue(ArrowUp);
    }
    void ReverseActiveValue(GameObject obj)
    {
        if (obj.activeInHierarchy) obj.SetActive(false);
        else obj.SetActive(true);
    }
    public void HideAllArrows(){
        ArrowDown.SetActive(false);
        ArrowUp.SetActive(false);
    }
    bool GetOrder(){
        if(ArrowDown.activeInHierarchy) return false;
        else return true;
    }
    public void ChangeSortType(){
        sortingManager.ChangeSortType(gameObject.GetComponent<Toggle>(), GetOrder());
    }
}
