
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

