using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlideUpCanvas : MonoBehaviour
{
    public RectTransform canvasTransform; // Assign your canvas RectTransform here
    public Button triggerButton; // Assign your button here
    public float slideDuration = 0.5f; // Duration of the slide animation in seconds
    public float slideDistance = 200f; // Distance the canvas will slide up by (relative to its current position)

    private Vector2 originalPosition;
    private bool isAtOriginalPosition = true;

    void Start()
    {
        if (canvasTransform == null || triggerButton == null)
        {
            Debug.LogError("Please assign the canvasTransform and triggerButton in the inspector.");
            return;
        }

        // Store the original position of the canvas
        originalPosition = canvasTransform.anchoredPosition;

        // Add a listener to the button
        triggerButton.onClick.AddListener(() => StartCoroutine(ToggleSlide()));
    }

    private IEnumerator ToggleSlide()
    {
        float elapsedTime = 0f;
        Vector2 startPosition = canvasTransform.anchoredPosition;
        Vector2 endPosition = isAtOriginalPosition ? new Vector2(startPosition.x, startPosition.y + slideDistance) : originalPosition;

        while (elapsedTime < slideDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / slideDuration;

            // Smoothly interpolate the position using Mathf.SmoothStep
            canvasTransform.anchoredPosition = new Vector2(startPosition.x, Mathf.SmoothStep(startPosition.y, endPosition.y, t));

            yield return null;
        }

        // Ensure the canvas is at the final position
        canvasTransform.anchoredPosition = endPosition;
        isAtOriginalPosition = !isAtOriginalPosition;
    }
}
