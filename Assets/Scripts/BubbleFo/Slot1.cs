using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public BubbleData currentBubble; // The bubble currently in this slot
    public Image slotImage;          // The UI Image for the slot
    public bool isAcceptingInput;    // Can this slot accept drag-and-drop?

    public void SetBubble(BubbleData bubble)
    {
        currentBubble = bubble;
        if (bubble != null)
        {
            slotImage.sprite = bubble.bubbleImage;
            slotImage.color = Color.white;
        }
        else
        {
            slotImage.sprite = null;
            slotImage.color = Color.clear;
        }
    }

    public BubbleData GetBubble()
    {
        return currentBubble;
    }
}
