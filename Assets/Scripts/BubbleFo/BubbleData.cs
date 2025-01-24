using UnityEngine;
using TMPro;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewBubbleData", menuName = "ScriptableObjects/BubbleData", order = 1)]
public class BubbleData : ScriptableObject
{
    public string bubbleName;     // Name of the bubble
    public Sprite bubbleImage;    // Image (Sprite) of the bubble
    public string bubbleText;     // Text for the bubble (string for simplicity)
    public int bubbleTiers;       // Tier level of the bubble
    public int bubblePrice;       // Price of the bubble
}
