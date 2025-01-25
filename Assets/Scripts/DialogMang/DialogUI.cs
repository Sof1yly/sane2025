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

    public void StartDialog(string dialogName, int[] dialogID)
    {
        gameObject.SetActive(true);
        dialogHistoryManager.ClearHistory();
        int randomValue = rng.GetRandomNumber(dialogID);
        currentDialog = dialogManager.GetDialogByCharacter(dialogName, randomValue);
        if (currentDialog != null)
        {
            currentLineIndex = 0;
            ShowNextLine();
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
            string currentLine = currentDialog.dialogLines[currentLineIndex];
            dialogText.text = currentLine;
            dialogHistoryManager.AddToHistory(currentLine);
            currentLineIndex++;
        }
        else
        {
            EndDialog();
        }
    }

    public void EndDialog()
    {
        gameObject.SetActive(false);
        dialogText.text = "End of Dialog.";
    }

    public void testDialog()
    {
        //int[] numbers = { 10, 20, 30, 40, 50 };
        //StartDialog("Anger", new int[] {1, 2});
        StartDialog("correctAns", new int[] {1});
    }
}