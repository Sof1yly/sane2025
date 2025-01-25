using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogHistoryToggle : MonoBehaviour
{
    public GameObject dialogHistoryUI; // อ้างอิงถึง UI ของ DialogHistory

    // เรียกใช้เมื่อกดปุ่ม
    public void ToggleDialogHistory()
    {
        if (dialogHistoryUI != null)
        {
            // สลับสถานะเปิด/ปิด
            bool isActive = dialogHistoryUI.activeSelf;
            dialogHistoryUI.SetActive(!isActive);

            Debug.Log($"DialogHistory is now {(isActive ? "hidden" : "visible")}");
        }
        else
        {
            Debug.LogError("DialogHistory UI reference is missing!");
        }
    }
}
