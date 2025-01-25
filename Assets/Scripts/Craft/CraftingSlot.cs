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

        // Make the slot image semi-transparent or fully transparent at the start
        // so the player knows it’s empty.
        Color color = slotImage.color;
        color.a = 0f;        // Set to 0f if you want it fully invisible
        slotImage.color = color;

        // Optionally, you can keep slotImage.enabled = true so we only change transparency.
        // Alternatively, you could keep it disabled until a bubble is placed.
        slotImage.enabled = true;
    }

    public void SetBubble(BubbleData bubble)
    {
        currentBubble = bubble;

        // Assign sprite
        slotImage.sprite = bubble.bubbleImage;

        // Make it fully visible now that it’s occupied
        Color color = slotImage.color;
        color.a = 1f;
        slotImage.color = color;

        slotImage.enabled = true;
    }

    public void ClearSlot()
    {
        currentBubble = null;
        slotImage.sprite = null;

        // Make the slot semi-transparent again
        Color color = slotImage.color;
        color.a = 0f;
        slotImage.color = color;

        // Or, if you’d rather hide it entirely, you could do:
        // slotImage.enabled = false;
    }

    // OnDrop: When a DraggableBubble is dropped onto this slot
    public void OnDrop(PointerEventData eventData)
    {
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
