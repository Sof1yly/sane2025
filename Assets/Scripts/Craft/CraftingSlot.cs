using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlot : MonoBehaviour, IDropHandler
{
    public Image slotImage;
    public BubbleData currentBubble;

    private void Awake()
    {
        if (slotImage == null)
            slotImage = GetComponent<Image>();

        // If you're using this as a Result slot, you might want 
        // to make it not raycast target or something similar. 
        // Or simply not implement IDropHandler on the result slot.
    }

    public void SetBubble(BubbleData bubble)
    {
        currentBubble = bubble;
        slotImage.sprite = bubble.bubbleImage;
        slotImage.enabled = true;
    }

    public void ClearSlot()
    {
        currentBubble = null;
        slotImage.sprite = null;
        slotImage.enabled = false;
    }

    // This method allows us to drop a DraggableBubble onto this slot
    public void OnDrop(PointerEventData eventData)
    {
        // Get the dragged object
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject != null)
        {
            DraggableBubble draggableBubble = draggedObject.GetComponent<DraggableBubble>();
            if (draggableBubble != null)
            {
                // Store the bubble data in this slot
                SetBubble(draggableBubble.bubbleData);

                // Immediately return the actual draggable object 
                // back to its original slot/position
                draggableBubble.ResetToOriginalPosition();

                Debug.Log($"Bubble '{draggableBubble.bubbleData.bubbleName}' placed in {gameObject.name} (data saved) and returned to original slot.");
            }
        }
    }

}
