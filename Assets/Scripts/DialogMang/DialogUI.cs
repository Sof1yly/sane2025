using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text dialogText;
    public DialogManager dialogManager;
    public DialogHistoryManager dialogHistoryManager;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip scaleUpClip;
    public AudioClip scaleDownClip;
    public AudioClip typingClip;

    private int currentLineIndex = 0;
    private Dialog currentDialog;

    private RandomNumberGenerator rng = new RandomNumberGenerator();
    private Coroutine typingCoroutine;

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
            // ขยาย UI เป็น 1 พร้อมกับเรียก ShowNextLine
            StartCoroutine(ScaleUI(Vector3.one, () => ShowNextLine()));
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
            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine); // หยุดเอฟเฟ็กต์พิมพ์ก่อนหน้าถ้ามี
            }

            // ย่อ UI กลับลงมาเป็น 0 ก่อน จากนั้นค่อยเริ่ม typing
            StartCoroutine(ScaleUI(Vector3.zero, () =>
            {
                string currentLine = currentDialog.dialogLines[currentLineIndex];
                typingCoroutine = StartCoroutine(TypeText(currentLine)); // เริ่มเอฟเฟ็กต์พิมพ์
                dialogHistoryManager.AddToHistory(currentLine);
                currentLineIndex++;

                // ขยายกลับเป็น 1 อีกครั้งหลังจากเริ่มพิมพ์
                StartCoroutine(ScaleUI(Vector3.one, null));
            }));
        }
        else
        {
            EndDialog();
        }
    }

    public void EndDialog()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // ย่อ UI ลงมาแล้วปิด
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

        // เล่นเสียงก่อนทำ Lerp
        if (audioSource != null)
        {
            // ถ้าต้องการขยาย (Scale ขึ้น)
            if (targetScale == Vector3.one && scaleUpClip != null)
            {
                audioSource.PlayOneShot(scaleUpClip);
            }
            // ถ้าต้องการย่อ (Scale ลง)
            else if (targetScale == Vector3.zero && scaleDownClip != null)
            {
                audioSource.PlayOneShot(scaleDownClip);
            }
        }

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
            yield return null;
        }

        transform.localScale = targetScale;
        onComplete?.Invoke();
    }

    private IEnumerator TypeText(string text)
    {
        dialogText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            // เพิ่มตัวอักษรทีละตัว
            dialogText.text += letter;

            // เล่นเสียงตอนพิมพ์ตัวอักษร (ถ้ามี AudioClip, AudioSource)
            if (audioSource != null && typingClip != null)
            {
                audioSource.PlayOneShot(typingClip);
            }

            // ปรับความเร็วตามต้องการ
            yield return new WaitForSeconds(0.05f);
        }
    }

    // ทดสอบการเริ่ม Dialog ได้
    public void testDialog()
    {
        StartDialog("correctAns", new int[] { 1 });
    }
}
