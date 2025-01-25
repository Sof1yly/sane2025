using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBubble : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    public BubbleData bubbleData;      // The BubbleData associated with this bubble
    public Transform originalParent;  // The original slot the bubble belongs to

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();

        // Set initial transparency
        canvasGroup.alpha = 0f; 
    }

    private void Start()
    {
        // Save the original parent when the object starts
        originalParent = transform.parent;

        // Optionally set the bubble's image dynamically from its BubbleData
        GetComponent<UnityEngine.UI.Image>().sprite = bubbleData.bubbleImage;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.85f; // Make the object semi-transparent
        canvasGroup.blocksRaycasts = false; // Allow drop zones to detect the drag
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // Move the object
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0f; // Restore full visibility
        canvasGroup.blocksRaycasts = true; // Block raycasts again

        // If not dropped on a valid target, return to the original position
        ResetToOriginalPosition();
    }

    public void ResetToOriginalPosition()
    {
        rectTransform.anchoredPosition = Vector2.zero;
        transform.SetParent(originalParent);
    }
}
