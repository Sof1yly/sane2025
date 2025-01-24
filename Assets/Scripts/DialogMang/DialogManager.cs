using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public DialogData dialogData; // Reference to the DialogData ScriptableObject

    private int currentDialogIndex = 0;

    private void Start()
    {
        DisplayDialog();
    }

    public void DisplayDialog()
    {
        if (currentDialogIndex < dialogData.dialogEntries.Length)
        {
            var dialogEntry = dialogData.dialogEntries[currentDialogIndex];

            Debug.Log($"NPC: {dialogEntry.npc.npcName}");
            Debug.Log($"Dialog: {dialogEntry.dialogText}");
            Debug.Log($"Bubble: {dialogEntry.bubble.bubbleName}");

            // Proceed to the next dialog line (Example for stepping through dialogs)
            currentDialogIndex++;
        }
        else
        {
            Debug.Log("End of dialog.");
        }
    }
}
