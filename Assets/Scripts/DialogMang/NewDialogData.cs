using UnityEngine;

[CreateAssetMenu(fileName = "NewDialog", menuName = "Dialog/Create New Dialog")]
public class NewDialogData : ScriptableObject
{
    [TextArea(2, 5)] // ช่วยให้ข้อความใน Inspector อ่านง่ายขึ้น
    public string[] dialogLines; // เก็บข้อความบทสนทนาแต่ละบรรทัด
}