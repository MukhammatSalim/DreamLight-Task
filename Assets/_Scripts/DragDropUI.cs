using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DragDropUI : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Connections")]
    public UIManager UIManager;

    [Header("Drag essentials")]
    Vector3 offset;
    CanvasGroup canvasGroup;
    Image image;

    [Header("Checking ")]
    public string panelTag = "UI_Element";

    void Awake()
    {
        image = gameObject.GetComponent<Image>();
        gameObject.AddComponent<CanvasGroup>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        RaycastResult raycastResult = eventData.pointerCurrentRaycast;

        transform.position = Input.mousePosition + offset;

        if (IsPanel(raycastResult.gameObject))
        {
            if (!UIManager.IsBlankPanel(raycastResult.gameObject))
            {
                UIManager.HoverSpaceForNewPanel(raycastResult.gameObject);
            }
        }
        else if (UIManager.isViewPort(raycastResult.gameObject))
        {
            Debug.Log("This is a ViewPort! Hovering new space!");
            UIManager.HoverSpaceForNewPanel(raycastResult.gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        UIManager.HoverSpaceForNewPanel(gameObject);
        gameObject.transform.SetParent(UIManager.transform.GetChild(0));
        offset = transform.position - Input.mousePosition;

        image.maskable = false;
        canvasGroup.alpha = 0.5f;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        UIManager.AssignToBlankPanel(gameObject);
        image.maskable = true;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    bool IsPanel(GameObject obj)
    {
        if (obj.tag == panelTag) return true;
        else return false;
    }
}

