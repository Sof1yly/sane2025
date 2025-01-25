[System.Serializable]
public class Dialog
{
    public string dialogName;
    public int dialogID;
    public string[] dialogLines;
}

[System.Serializable]
public class DialogCollection
{
    public Dialog[] dialogs;
}