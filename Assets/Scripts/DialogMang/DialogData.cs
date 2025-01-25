using UnityEngine;

[System.Serializable]
public class DialogEntry
{
    public NPC npc;              // The NPC who speaks the dialog
    public BubbleData bubble;    // The bubble associated with the dialog
    public string dialogText;    // The dialog text
}

[CreateAssetMenu(fileName = "NewDialogData", menuName = "ScriptableObjects/DialogData", order = 1)]
public class DialogData : ScriptableObject
{
    public DialogEntry[] dialogEntries; // Array of dialog entries
}
