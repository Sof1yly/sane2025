using UnityEngine;
using TMPro;

public class BubbleCreator : MonoBehaviour
{
    public BubbleData[] bubbleDataArray; // Array of BubbleData ScriptableObjects
    public GameObject bubblePrefab;      // Prefab to instantiate for each bubble
    public Transform parentTransform;    // Optional: Parent to organize created bubbles in the hierarchy

    private void Start()
    {
        CreateBubbles();
    }

    private void CreateBubbles()
    {
        foreach (var bubbleData in bubbleDataArray)
        {
            // Instantiate the prefab
            GameObject bubbleObject = Instantiate(bubblePrefab, parentTransform);

            // Set the name of the GameObject
            bubbleObject.name = bubbleData.bubbleName;

            // Set the image (SpriteRenderer or UI Image)
            var spriteRenderer = bubbleObject.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = bubbleData.bubbleImage;
            }

            //var uiImage = bubbleObject.GetComponent<UnityEngine.UI.Image>();
            //if (uiImage != null)
            //{
            //    uiImage.sprite = bubbleData.bubbleImage;
            //}

            // Set the TextMeshPro text
            var textMeshPro = bubbleObject.GetComponentInChildren<TextMeshPro>();
            if (textMeshPro != null)
            {
                textMeshPro.text = bubbleData.bubbleText;
            }

            // Additional setup: Log or handle tiers and price
            Debug.Log($"Created Bubble: {bubbleData.bubbleName}, Tiers: {bubbleData.bubbleTiers}, Price: {bubbleData.bubblePrice}");
        }
    }
}
