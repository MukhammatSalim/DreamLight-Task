using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragNDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] Transform firstParent;
    [SerializeField] Transform canvas;
    Vector3 Offset;
    CanvasGroup canvasGroup;
    Image image;
    [Header("Debugging")]
    [SerializeField] bool showRayCastResult;
    [SerializeField] bool showRayCastResultParents;
    [SerializeField] string DestinationAreaTag = "Drop Area";
    void Awake()
    {
        firstParent = canvas;
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
        firstParent = gameObject.transform.parent;
        gameObject.transform.SetParent(canvas);

        Offset = transform.position - Input.mousePosition;

        image.maskable = false;
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        RaycastResult raycastResult = eventData.pointerCurrentRaycast;

        if (showRayCastResult == true) Debug.Log(raycastResult.gameObject);
        if (showRayCastResultParents == true) Debug.Log(raycastResult.gameObject?.transform.parent.parent);

        if (raycastResult.gameObject?.tag == DestinationAreaTag)
            TransferFromViewPortToContent(raycastResult);
        else if (raycastResult.gameObject?.transform.parent.tag == DestinationAreaTag)
            TransferFromPanelToContent(raycastResult);
        else if (raycastResult.gameObject?.transform.parent.parent.tag == DestinationAreaTag)
            TransferFromTextToContent(raycastResult);
        else
            gameObject.transform.SetParent(firstParent);
            
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


}
