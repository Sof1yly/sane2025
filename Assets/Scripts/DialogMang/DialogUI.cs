using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogSpriteMapping
{
    public string dialogName; // ชื่อตัวละครหรือ dialogName
    public Sprite sprite;     // ภาพที่ต้องการแสดง
}

public class DialogUI : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text dialogText;
    public DialogManager dialogManager;
    public DialogHistoryManager dialogHistoryManager;

    [Header("UI Image to Change Sprite")]
    public Image dialogImage;  // <--- อ้างอิง Image ใน Canvas ที่จะแสดง Sprite

    [Header("Sprite Mappings")]
    public List<DialogSpriteMapping> dialogSprites;
    // ใส่ dialogName และ sprite ที่ตรงกันใน Inspector

    [Header("Default Sprite (If Not Found)")]
    public Sprite defaultSprite;
    // Sprite ที่จะใช้ถ้าไม่เจอ dialogName ตรงใน list

    [Header("Audio")]
    public AudioSource audioSource; // สำหรับ Scale Up / Scale Down
    public AudioClip scaleUpClip;
    public AudioClip scaleDownClip;

    [Header("Typing Audio")]
    public AudioSource typingAudioSource; // เพิ่ม AudioSource สำหรับเสียงพิมพ์
    public AudioClip typingClip; // Optional: ถ้าต้องการเสียงต่อเนื่องอื่น ๆ

    private int currentLineIndex = 0;
    private Dialog currentDialog;

    private RandomNumberGenerator rng = new RandomNumberGenerator();
    private Coroutine typingCoroutine;

    private Action onDialogEndCallback;

    private void Awake()
    {
        // ตั้งค่า scale เริ่มต้นให้เป็น 0
        transform.localScale = Vector3.zero;

        // ตรวจสอบ AudioSource
        if (audioSource == null)
        {
            Debug.LogWarning("DialogUI: AudioSource for scale UI is not assigned!");
        }

        if (typingAudioSource == null)
        {
            Debug.LogWarning("DialogUI: Typing AudioSource is not assigned!");
        }
    }

    public void StartDialog(string dialogName, int[] dialogID, Action onEndDialogCallback = null)
    {
        // 1) กำหนด Sprite ตาม dialogName
        dialogImage.sprite = GetSpriteByDialogName(dialogName);
        onDialogEndCallback = onEndDialogCallback;

        // 2) ตั้งค่าการทำงานของ Dialog
        gameObject.SetActive(true);
        dialogHistoryManager.ClearHistory();
        int randomValue = rng.GetRandomNumber(dialogID);
        currentDialog = dialogManager.GetDialogByCharacter(dialogName, randomValue);

        if (currentDialog != null)
        {
            currentLineIndex = 0;
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
                StopCoroutine(typingCoroutine);
            }

            StartCoroutine(ScaleUI(Vector3.zero, () =>
            {
                string currentLine = currentDialog.dialogLines[currentLineIndex];
                typingCoroutine = StartCoroutine(TypeText(currentLine));
                dialogHistoryManager.AddToHistory(currentLine);
                currentLineIndex++;
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

        StartCoroutine(ScaleUI(Vector3.zero, () =>
        {
            gameObject.SetActive(false);
            dialogText.text = "End of Dialog.";
            onDialogEndCallback?.Invoke();
            typingAudioSource.Stop();
        }));
    }

    /// <summary>
    /// ฟังก์ชัน Lerp ขยาย-ย่อ UI พร้อม CallBack 
    /// </summary>
    private IEnumerator ScaleUI(Vector3 targetScale, System.Action onComplete)
    {
        Vector3 originalScale = transform.localScale;
        float duration = 0.3f;
        float elapsed = 0f;

        // เล่นเสียงตามสถานะขยาย/ย่อ
        if (audioSource != null)
        {
            if (targetScale == Vector3.one && scaleUpClip != null)
            {
                Debug.Log("Playing Scale Up Clip");
                audioSource.PlayOneShot(scaleUpClip);
            }
            else if (targetScale == Vector3.zero && scaleDownClip != null)
            {
                Debug.Log("Playing Scale Down Clip");
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

    /// <summary>
    /// เอฟเฟกต์พิมพ์ Text ทีละตัวอักษร
    /// </summary>
    private IEnumerator TypeText(string text)
    {
        dialogText.text = "";

        // เริ่มเล่นเสียงพิมพ์
        if (typingAudioSource != null && typingClip != null)
        {
            typingAudioSource.clip = typingClip;
            typingAudioSource.loop = true; // ตั้งให้ loop ถ้าต้องการเสียงต่อเนื่อง
            typingAudioSource.Play();
            Debug.Log("Started Typing Sound");
        }

        foreach (char letter in text.ToCharArray())
        {
            dialogText.text += letter;

            // ถ้าต้องการเสียงพิมพ์ per character, สามารถใช้ audioSource.PlayOneShot(typingClip) ได้
            // แต่ถ้าใช้ looping sound, ไม่ต้องทำ
            /*
            if (audioSource != null && typingClip != null)
            {
                Debug.Log("Playing Typing Clip");
                audioSource.PlayOneShot(typingClip);
            }
            */

            yield return new WaitForSeconds(0.05f);
        }

        // หยุดเล่นเสียงพิมพ์เมื่อพิมพ์เสร็จ
        if (typingAudioSource != null && typingAudioSource.isPlaying)
        {
            typingAudioSource.Stop();
            Debug.Log("Stopped Typing Sound");
        }
    }

    /// <summary>
    /// ถ้าไม่เจอ dialogName ใน list ให้คืน defaultSprite แทน
    /// </summary>
    private Sprite GetSpriteByDialogName(string nameToFind)
    {
        foreach (DialogSpriteMapping mapping in dialogSprites)
        {
            if (mapping.dialogName == nameToFind)
            {
                return mapping.sprite;
            }
        }
        return defaultSprite; // ไม่มีใน List → ใช้ defaultSprite
    }

    /// <summary>
    /// ฟังก์ชันทดสอบ
    /// </summary>
    public void testDialog()
    {
        StartDialog("Pride", new int[] { 1 });
    }
}
