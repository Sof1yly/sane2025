using UnityEngine;
using TMPro;

public class DialogHistoryManager : MonoBehaviour
{
    public TMP_Text dialogHistoryText; // TextMeshPro สำหรับแสดงประวัติ
    private string history = ""; // เก็บประวัติการสนทนา

    // เพิ่มข้อความใหม่ในประวัติ
    public void AddToHistory(string dialogLine)
    {
        string newEntry = $"{dialogLine}\n"; // จัดรูปแบบข้อความ
        history += newEntry; // เพิ่มข้อความในประวัติ
        dialogHistoryText.text = history; // อัปเดต UI
    }

    // ล้างประวัติการสนทนา
    public void ClearHistory()
    {
        history = "";
        dialogHistoryText.text = history;
    }

    public void clicker()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}
