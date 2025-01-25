using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Canvas countdownCanvas; // The canvas to show during countdown
    public Image rollingCircle; // The circle image that rolls across the screen
    public float countdownTime = 3.6f; // Duration of the countdown

    private Vector2 circleStartPosition;
    private Vector2 circleEndPosition;

    private void Start()
    {
        // Ensure the canvas is hidden at the start
        countdownCanvas.gameObject.SetActive(false);

        // Define the start and end positions of the circle
        RectTransform circleTransform = rollingCircle.GetComponent<RectTransform>();
        circleStartPosition = new Vector2(-Screen.width / 2f, -Screen.height / 2.5f); // Bottom-left corner of the screen
        circleEndPosition = new Vector2(Screen.width / 2f, -Screen.height / 2.5f); // Bottom-right corner of the screen
    }

    public void PlayGame()
    {
        StartCoroutine(StartCountdownAndLoadScene());
    }

    private IEnumerator StartCountdownAndLoadScene()
    {
        // Show the countdown canvas
        countdownCanvas.gameObject.SetActive(true);

        // Start rolling the circle and countdown
        StartCoroutine(RollCircle());

        // Wait for the countdown time
        yield return new WaitForSeconds(countdownTime);

        // Load the next scene
        SceneManager.LoadSceneAsync(1);
    }

    private IEnumerator RollCircle()
    {
        float elapsedTime = 0f;
        RectTransform circleTransform = rollingCircle.GetComponent<RectTransform>();

        while (elapsedTime < countdownTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / countdownTime;

            // Move the circle along the bottom of the screen from start to end
            circleTransform.anchoredPosition = Vector2.Lerp(circleStartPosition, circleEndPosition, t);

            // Optionally, rotate the circle for a rolling effect
            circleTransform.Rotate(Vector3.forward, -360 * Time.deltaTime);

            yield return null;
        }

        // Ensure the circle ends at its final position
        circleTransform.anchoredPosition = circleEndPosition;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}