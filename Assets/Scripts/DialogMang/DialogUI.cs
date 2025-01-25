using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    public TMP_Text dialogText;
    public DialogManager dialogManager;
    public DialogHistoryManager dialogHistoryManager;
    private int currentLineIndex = 0;
    private Dialog currentDialog;

    private RandomNumberGenerator rng = new RandomNumberGenerator();

    private void Awake()
    {
        // Set initial scale to 0
        transform.localScale = Vector3.zero;
    }

    public void StartDialog(string dialogName, int[] dialogID)
    {
        gameObject.SetActive(true);
        dialogHistoryManager.ClearHistory();
        int randomValue = rng.GetRandomNumber(dialogID);
        currentDialog = dialogManager.GetDialogByCharacter(dialogName, randomValue);

        if (currentDialog != null)
        {
            currentLineIndex = 0;
            StartCoroutine(ScaleUI(Vector3.one, () => ShowNextLine())); // Expand to scale 1 and show the first line
        }
        else
        {
            Debug.LogError("Dialog not found for character: " + dialogName);
        }
    }

    public void ShowNextLine()
    {
        if (currentDialog != null && currentLineIndex < currentDialog.dialogLines.Length)
        {
            StartCoroutine(ScaleUI(Vector3.zero, () =>
            {
                string currentLine = currentDialog.dialogLines[currentLineIndex];
                dialogText.text = currentLine;
                dialogHistoryManager.AddToHistory(currentLine);
                currentLineIndex++;
                StartCoroutine(ScaleUI(Vector3.one, null)); // Expand back to scale 1
            }));
        }
        else
        {
            EndDialog();
        }
    }

    public void EndDialog()
    {
        StartCoroutine(ScaleUI(Vector3.zero, () =>
        {
            gameObject.SetActive(false);
            dialogText.text = "End of Dialog.";
        }));
    }

    private IEnumerator ScaleUI(Vector3 targetScale, System.Action onComplete)
    {
        Vector3 originalScale = transform.localScale;
        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
            yield return null;
        }

        transform.localScale = targetScale;
        onComplete?.Invoke();
    }

    public void testDialog()
    {
        StartDialog("correctAns", new int[] { 1 });
    }
}
