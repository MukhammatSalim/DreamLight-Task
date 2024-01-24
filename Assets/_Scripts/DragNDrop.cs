using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    public Transform Canvas;
    public GameObject EmptyElementPrefab;
    GameObject EmptyElement;
    Transform originParent;
    Vector3 offset;
    CanvasGroup canvasGroup;
    Transform savedPanel;
    Image image;
    string destinationAreaTag = "DropArea";
    string uiElementTag = "UI_Element";
    [Header("Debugging")]
    [SerializeField] bool showRayCastResult;
    
    [SerializeField] bool DebugAssignToContent;
    [SerializeField] bool showComparison;
    [SerializeField] bool showEmptyPanelCreation;
    void Awake()
    {
        image = gameObject.GetComponent<Image>();
        if (gameObject.GetComponent<CanvasGroup>() == null)
            gameObject.AddComponent<CanvasGroup>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + offset;
        RaycastResult raycastResult = eventData.pointerCurrentRaycast;
        if (showRayCastResult == true) Debug.Log("Raycast Result is " + raycastResult.gameObject);
        if (GetElementPanel(raycastResult.gameObject) != null) //object is a part of the panel
        {
            if (checkForNewElement(raycastResult)) //Is this a new Panel?
            {
                savedPanel = GetElementPanel(raycastResult.gameObject).transform; //Save the transform of the new Panel
                if (EmptyElement == null)
                {
                    EmptyElement = createBlankSpace(EmptyElementPrefab, savedPanel.transform.parent);
                    if (showEmptyPanelCreation == true) Debug.Log("Created new empty panel in " + savedPanel.transform.parent);
                }
                else 
                {
                    Destroy(EmptyElement);
                    EmptyElement = createBlankSpace(EmptyElementPrefab, savedPanel.transform.parent);
                }
                savedPanel = GetElementPanel(raycastResult.gameObject);
                //CreateBlankSpaceAbove(savedPanel.transform);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originParent = gameObject.transform.parent;
        savedPanel = gameObject.transform;
        gameObject.transform.SetParent(Canvas);

        offset = transform.position - Input.mousePosition;

        image.maskable = false;
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RaycastResult raycastResult = eventData.pointerCurrentRaycast;

        if (showRayCastResult == true) Debug.Log("Raycast Result is " + raycastResult.gameObject);

        AssignToContent(raycastResult, originParent);
        Destroy(EmptyElement);

        image.maskable = true;
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

    }

    void AssignToContent(RaycastResult raycastResult, Transform firstParent)
    {

        Transform ViewPortTransform;
        Transform currentTransform = raycastResult.gameObject?.transform;

        ViewPortTransform = FindViewPort(currentTransform);
        if (ViewPortTransform == null) ViewPortTransform = firstParent;


        gameObject.transform.SetParent(ViewPortTransform.GetChild(0));
        if (DebugAssignToContent == true) Debug.Log(ViewPortTransform.GetChild(0) + " is a parent of the game object");
    }

    Transform FindViewPort(Transform currentT)
    {
        Transform ViewPortTransform = null;
        Transform parentTransform = currentT;
        for (int i = 0; i < 5; i++)
        {
            if (parentTransform.tag == destinationAreaTag)
            {
                ViewPortTransform = parentTransform;
                if (DebugAssignToContent == true) Debug.Log("Target Transform is " + ViewPortTransform);
                return ViewPortTransform;
            }
            else
            {
                if (DebugAssignToContent == true) Debug.Log(parentTransform + " is not " + destinationAreaTag);
                parentTransform = parentTransform.parent;
            }
        }
        if (ViewPortTransform == null)
        {
            if (DebugAssignToContent == true) Debug.Log("View Port has not been found.");
        }
        return null;

    }
    Transform GetElementPanel(GameObject obj) // works fine, always gets panel
    {
        Transform currentObject = obj.transform;
        for (int i = 0; i < 2; i++)
        {
            if (currentObject.tag == uiElementTag)
            {
                return currentObject;
            }
            else currentObject = currentObject.parent;
        }
        return null;
    }

    int GetObjectChildIndex(Transform panel)
    {
        for (int i = 0; i < panel.parent.childCount; i++)
        {
            if (panel.parent.GetChild(i) == panel) return i;
        }
        return 0;
    }
    GameObject createBlankSpace(GameObject emptyElement, Transform content)
    {
        GameObject NewBlankElement = Instantiate(emptyElement, content);
        return NewBlankElement;
    }

    bool checkForNewElement(RaycastResult raycastResult) 
    {
        if(GetElementPanel(raycastResult.gameObject) == GetElementPanel(savedPanel.gameObject)) 
        {
            if (showComparison == true) Debug.Log(raycastResult.gameObject + " panel is equal to saved " + savedPanel);
            return false;
        }
        else 
        {
            if (showComparison == true) Debug.Log(raycastResult.gameObject + " and " + savedPanel + " panels are different, new element");
            return true;
        }

    }

}
