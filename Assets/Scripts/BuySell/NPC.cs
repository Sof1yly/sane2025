using System;
using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public Action OnNPCDestroyed;

    [Header("Assigned NPC Data")]
    [SerializeField] private NPCData npcData;

    public SpriteRenderer spriteRenderer;
    public Animator animator;

    private string currentemotion;
    private DialogUI attachedDialogUI;
    private GameObject craftUI;

    private BubbleCombiner bubbleCombiner;
    private CrafingUIMangaer gamemanager;
    private bool hasChecked = false;

    [Header("Audio Settings")]
    public AudioSource audioSourceNPC;           // AudioSource สำหรับเล่นเสียงตอบกลับ
    public AudioClip correctAnswerClip;       // เสียงเมื่อคำตอบถูกต้อง
    public AudioClip wrongAnswerClip;

    public void Init(NPCData data, DialogUI dialogUI, GameObject craftUIObject, AudioSource audioSource, AudioClip Fxcorrect, AudioClip Fxwrong)
    {
        npcData = data;
        attachedDialogUI = dialogUI;
        craftUI = craftUIObject;
        audioSourceNPC = audioSource;
        correctAnswerClip = Fxcorrect;
        wrongAnswerClip = Fxwrong;
        // สุ่ม emotion
        if (npcData.emotion != null && npcData.emotion.Length > 0)
        {
            int randIndex = UnityEngine.Random.Range(0, npcData.emotion.Length);
            currentemotion = npcData.emotion[randIndex];
        }
        else
        {
            currentemotion = "Neutral";
        }

        // ตั้งชื่อ
        gameObject.name = "NPC_" + npcData.characterName + "_" + currentemotion;

        // ตั้ง Sprite/Animator
        if (spriteRenderer != null && npcData.characterSprite != null)
        {
            spriteRenderer.sprite = npcData.characterSprite;
        }
        if (animator != null && npcData.animatorCtrl != null)
        {
            animator.runtimeAnimatorController = npcData.animatorCtrl;
        }

        // ** ตรงนี้คือจุดเริ่ม Dialog **
        if (attachedDialogUI != null)
        {
            // 1) เปิด IsTalking ให้เป็น true ก่อนเริ่ม Dialog
            if (animator != null)
            {
                animator.SetBool("IsTalking", true);
            }

            // 2) เรียก StartDialog แล้วส่ง Callback ตอนจบ
            attachedDialogUI.StartDialog(currentemotion, new int[] { 1, 2 }, () =>
            {
                // *** พอจบบทสนทนา → Set IsTalking = false ***
                if (animator != null)
                {
                    animator.SetBool("IsTalking", false);
                }

                // ... คำสั่งอื่น ๆ หลังจบ Dialog ...
                craftUI.gameObject.SetActive(true);

                if (bubbleCombiner != null)
                {
                    bubbleCombiner.imageToHide1.SetActive(true);
                    bubbleCombiner.imageToHide2.SetActive(true);
                    //bubbleCombiner.imageConfirm.SetActive(true);
                }
            });
        }

        Debug.Log($"NPC [{gameObject.name}] spawned and called StartDialog({currentemotion})");
    }

    private void Start()
    {
        bubbleCombiner = FindObjectOfType<BubbleCombiner>();
        gamemanager = FindObjectOfType<CrafingUIMangaer>();
        if (bubbleCombiner == null)
        {
            Debug.LogWarning("NPC cannot find BubbleCombiner in the scene!");
        }
    }

    private void Update()
    {
        if (hasChecked) return;

        if (bubbleCombiner != null &&
            bubbleCombiner.resultSlot != null &&
            bubbleCombiner.resultSlot.currentBubble != null)
        {
            string resultName = bubbleCombiner.resultSlot.currentBubble.bubbleName;
            if (resultName == currentemotion)
            {
                gamemanager.AddScore(10);
                Debug.Log($"[NPC {name}] MATCH: {resultName} == {currentemotion}");

                PlaySound(correctAnswerClip);

                attachedDialogUI.StartDialog("correctAns", new int[] { 1, 2, 3, 4, 5 }, () =>
                {
                    // จบบทสนทนา → ปิด IsTalking อีกครั้ง
                    if (animator != null)
                    {
                        animator.SetBool("IsTalking", false);
                    }
                    DestroySelf();
                });
            }
            else
            {
                gamemanager.AddWrong(1);
                Debug.Log($"Not match. (result = {resultName}, emotion = {currentemotion})");
                PlaySound(wrongAnswerClip);

                attachedDialogUI.StartDialog("WrongAns", new int[] { 1, 2, 3, 4, 5 }, () =>
                {
                    // จบบทสนทนา → ปิด IsTalking
                    if (animator != null)
                    {
                        animator.SetBool("IsTalking", false);
                    }
                    DestroySelf();
                });
            }

            hasChecked = true;
            StartCoroutine(EndSequence(3f));
        }
    }

    private IEnumerator EndSequence(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (bubbleCombiner != null && bubbleCombiner.resultSlot != null)
        {
            bubbleCombiner.resultSlot.ClearSlot();
        }

        if (craftUI != null)
        {
            craftUI.SetActive(false);
        }
    }

    private void DestroySelf()
    {
        OnNPCDestroyed?.Invoke();
        Destroy(gameObject);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSourceNPC != null && clip != null)
        {
            audioSourceNPC.PlayOneShot(clip);
            Debug.Log($"Playing sound: {clip.name}");
        }
        else
        {
            Debug.LogWarning("AudioSource หรือ AudioClip ไม่ถูกกำหนด!");
        }
    }
}
