using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] Transform originParent;
    public Transform Canvas;
    Vector3 Offset;
    CanvasGroup canvasGroup;
    Image image;
    [Header("Debugging")]
    [SerializeField] bool showRayCastResult;
    [SerializeField] string DestinationAreaTag = "DropArea";
    void Awake()
    {
        image = gameObject.GetComponent<Image>();
        if (gameObject.GetComponent<CanvasGroup>() == null)
            gameObject.AddComponent<CanvasGroup>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition + Offset;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        originParent = gameObject.transform.parent;
        gameObject.transform.SetParent(Canvas);

        Offset = transform.position - Input.mousePosition;

        image.maskable = false;
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RaycastResult raycastResult = eventData.pointerCurrentRaycast;

        if (showRayCastResult == true) Debug.Log("Raycast Result is " + raycastResult.gameObject);

        AssignToContent(raycastResult, originParent);
        // if (raycastResult.gameObject?.tag == DestinationAreaTag)
        //     TransferFromViewPortToContent(raycastResult);
        // else if (raycastResult.gameObject?.transform.parent.tag == DestinationAreaTag)
        //     TransferFromPanelToContent(raycastResult);
        // else if (raycastResult.gameObject?.transform.parent.parent.tag == DestinationAreaTag)
        //     TransferFromTextToContent(raycastResult);
        // else
        //     gameObject.transform.SetParent(originParent); 

        image.maskable = true;
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

    }

    void TransferFromViewPortToContent(RaycastResult raycastResult)
    {
        gameObject.transform.SetParent(raycastResult.gameObject?.transform.GetChild(0));
    }

    void TransferFromTextToContent(RaycastResult raycastResult)
    {
        gameObject.transform.SetParent(raycastResult.gameObject?.transform.parent.parent);
    }
    void TransferFromPanelToContent(RaycastResult raycastResult)
    {
        gameObject.transform.SetParent(raycastResult.gameObject?.transform.parent);
    }
    void AssignToContent(RaycastResult raycastResult, Transform firstParent)
    {

        Transform ViewPortTransform;
        Transform currentTransform = raycastResult.gameObject?.transform;
        
        ViewPortTransform = FindViewPort(currentTransform);
        if (ViewPortTransform == null) ViewPortTransform = firstParent;
        

        gameObject.transform.SetParent(ViewPortTransform.GetChild(0));
        Debug.Log(ViewPortTransform.GetChild(0) + " is a parent of the game object");
    }

    Transform FindViewPort(Transform currentT){
        Transform ViewPortTransform = null;
        Transform parentTransform = currentT;
        for (int i = 0; i < 5; i++)
        {
            if (parentTransform.tag == DestinationAreaTag)
            {
                ViewPortTransform = parentTransform;
                Debug.Log("Target Transform is " + ViewPortTransform);
                return ViewPortTransform;
            }
            else
            {
                Debug.Log(parentTransform + " is not " + DestinationAreaTag);
                parentTransform = parentTransform.parent;
            }
        }
        if (ViewPortTransform == null) 
        { 
            Debug.Log("View Port has not been found.");
        }
        return null;

    }


}
