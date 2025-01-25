<<<<<<< HEAD
//using UnityEngine;

//public class DialogManager : MonoBehaviour
//{
//    public DialogData dialogData; // Reference to the DialogData ScriptableObject

//    private int currentDialogIndex = 0;

//    private void Start()
//    {
//        DisplayDialog();
//    }

//    public void DisplayDialog()
//    {
//        if (currentDialogIndex < dialogData.dialogEntries.Length)
//        {
//            var dialogEntry = dialogData.dialogEntries[currentDialogIndex];

//            Debug.Log($"NPC: {dialogEntry.npc.npcName}");
//            Debug.Log($"Dialog: {dialogEntry.dialogText}");
//            Debug.Log($"Bubble: {dialogEntry.bubble.bubbleName}");

//            // Proceed to the next dialog line (Example for stepping through dialogs)
//            currentDialogIndex++;
//        }
//        else
//        {
//            Debug.Log("End of dialog.");
//        }
//    }
//}
=======
using UnityEngine;
using System.IO;

public class DialogManager : MonoBehaviour
{
    public DialogCollection dialogCollection;


    void Awake()
    {
        LoadDialogData();
    }

    void LoadDialogData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "dialog.json");

        if (File.Exists(filePath))
        {
            string jsonContent = File.ReadAllText(filePath);
            dialogCollection = JsonUtility.FromJson<DialogCollection>(jsonContent);

            Debug.Log("Dialogs Loaded Successfully!");
        }
        else
        {
            Debug.LogError("Dialog file not found: " + filePath);
        }
    }

    public Dialog GetDialogByCharacter(string dialogName, int dialogID)
    {
        foreach (var dialog in dialogCollection.dialogs)
        {
            if (dialog.dialogName == dialogName && dialog.dialogID == dialogID)
            {
                return dialog;
            }
        }
        return null;
    }
}
>>>>>>> main
