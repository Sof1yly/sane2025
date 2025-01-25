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

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void Start()
    {
        // Save the original parent when the object starts
        originalParent = transform.parent;

        // Optionally set the bubble's image if you have an Image component on the same GameObject
        Image img = GetComponent<Image>();
        if (img != null && bubbleData != null)
        {
            img.sprite = bubbleData.bubbleImage;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Play a sound when dragging starts
        if (audioSource != null && beginDragSound != null)
            audioSource.PlayOneShot(beginDragSound);

        // Make the bubble semi-transparent during drag
        canvasGroup.alpha = 0.6f;

        // Let drop zones detect the drag
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move the object with the mouse, scaled by the canvas factor
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Play a sound when dragging ends
        if (audioSource != null && endDragSound != null)
            audioSource.PlayOneShot(endDragSound);

        // Restore full visibility
        canvasGroup.alpha = 1f;

        // Block raycasts again
        canvasGroup.blocksRaycasts = true;

        // If not dropped on a valid target, return to original position
        ResetToOriginalPosition();
    }

    public void ResetToOriginalPosition()
    {
        // Move the bubble back to its original parent/slot
        transform.SetParent(originalParent);
        rectTransform.anchoredPosition = Vector2.zero;
    }
}
