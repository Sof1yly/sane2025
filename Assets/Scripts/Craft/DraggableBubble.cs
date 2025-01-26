using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableBubble : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Bubble Data")]
    public BubbleData bubbleData;            // The BubbleData associated with this bubble
    public Transform originalParent;         // The original slot/bubble slot this bubble belongs to

    [Header("Audio (Optional)")]
    public AudioSource audioSource;          // If null, no sound will play
    public AudioClip beginDragSound;
    public AudioClip endDragSound;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Image bubbleImage;

    // Variables to track dragging
    private Vector2 pointerOffset;           // Offset between cursor and bubble's anchored position
    private Transform temporaryParent;       // Temporary parent during dragging

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        bubbleImage = GetComponent<Image>();

        if (canvasGroup == null)
        {
            // Add a CanvasGroup if not present
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Get the Canvas from the parent hierarchy
        canvas = GetComponentInParent<Canvas>();

        // Initialize as fully transparent
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f; // Fully transparent
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    private void Start()
    {
        // Save the original parent when the object starts
        if (originalParent == null)
            originalParent = transform.parent;

        // Set the bubble's image if available
        if (bubbleImage != null && bubbleData != null)
        {
            bubbleImage.sprite = bubbleData.bubbleImage;
            bubbleImage.SetNativeSize(); // Adjust size to match sprite
        }

        // Ensure the bubble matches the original parent's size initially
        ResetToOriginalPosition();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Play a sound when dragging starts
        if (audioSource != null && beginDragSound != null)
            audioSource.PlayOneShot(beginDragSound);

        // Capture the offset between the cursor and the bubble's position
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localPointerPosition))
        {
            pointerOffset = rectTransform.anchoredPosition - localPointerPosition;
        }

        // Make the bubble semi-transparent when dragging starts
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0.6f; // Semi-transparent
            canvasGroup.blocksRaycasts = false; // Allow drop zones to receive raycasts
        }

        // Reparent the bubble to the root canvas to ensure it appears above other UI elements
        temporaryParent = canvas.transform;
        transform.SetParent(temporaryParent, true);

        // Optionally, scale up the bubble for visual feedback
        rectTransform.localScale = Vector3.one * 1.1f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null)
            return;

        // Convert the screen point to a position relative to the canvas
        Vector2 canvasPosition;
        RectTransform canvasRect = canvas.transform as RectTransform;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            eventData.position,
            eventData.pressEventCamera,
            out canvasPosition))
        {
            // Apply the offset to ensure the bubble follows the cursor accurately
            rectTransform.anchoredPosition = canvasPosition + pointerOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Play a sound when dragging ends
        if (audioSource != null && endDragSound != null)
            audioSource.PlayOneShot(endDragSound);

        // Restore full opacity only if not in originalParent
        if (transform.parent != originalParent && canvasGroup != null)
        {
            canvasGroup.alpha = 1f; // Fully opaque
        }
        else if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f; // Fully transparent
        }

        // Restore the original scale
        rectTransform.localScale = Vector3.one;

        // Restore raycasts
        if (canvasGroup != null)
        {
            canvasGroup.blocksRaycasts = true; // Block raycasts again
        }

        // If not dropped on a valid target, return to original position
        // This logic assumes that valid drop targets will re-parent the bubble
        // Otherwise, it will return to the original parent
        if (transform.parent == originalParent)
        {
            ResetToOriginalPosition();
        }
        else
        {
            // Optionally, set the bubble's visibility based on its new parent
            // For example, if placed in a slot, make it fully visible
            // If placed elsewhere, handle accordingly
            // This can be managed by the CraftingSlot script via SetVisibility
        }
    }

    public void ResetToOriginalPosition()
    {
        // Move the bubble back to its original parent/slot
        transform.SetParent(originalParent, false);

        RectTransform originalRect = originalParent.GetComponent<RectTransform>();
        if (originalRect != null)
        {
            // Match anchors
            rectTransform.anchorMin = originalRect.anchorMin;
            rectTransform.anchorMax = originalRect.anchorMax;

            // Match pivot
            rectTransform.pivot = originalRect.pivot;

            // Match size
            rectTransform.sizeDelta = originalRect.sizeDelta;

            // Reset position
            rectTransform.anchoredPosition = Vector2.zero;

            // Ensure transparency
            SetVisibility(false);
        }
        else
        {
            Debug.LogWarning("Original parent does not have a RectTransform component.");
        }
    }

    /// <summary>
    /// Sets the visibility of the bubble.
    /// </summary>
    /// <param name="isVisible">Whether the bubble should be visible.</param>
    public void SetVisibility(bool isVisible)
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = isVisible ? 1f : 0f;
        }
    }
}
