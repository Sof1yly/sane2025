using System;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private NPCData npcData;

    [Header("Scene References")]
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    // เรียกใช้หลัง Instantiate เพื่อเซ็ต Data ให้ NPC
    public void Init(NPCData data)
    {
        npcData = data;

        // ตั้งชื่อ GameObject เพื่อให้ง่ายต่อการดูใน Hierarchy
        gameObject.name = "NPC_" + npcData.characterName;

        // ตั้งค่า Sprite
        if (spriteRenderer != null && npcData.characterSprite != null)
        {
            spriteRenderer.sprite = npcData.characterSprite;
        }

        // ตั้งค่า Animator Controller
        if (animator != null && npcData.animatorCtrl != null)
        {
            animator.runtimeAnimatorController = npcData.animatorCtrl;
        }

        // ถ้าต้องใช้ currentemotion กับ Animator เช่น:
        // animator.SetTrigger(npcData.currentemotion);
    }

    // เรียกเพื่อให้ NPC แสดงบทสนทนาผ่าน DialogUI
    // พร้อม Callback เมื่อบทสนทนาจบ
    public void TriggerDialog(DialogUI dialogUI, int[] dialogIDs, Action onDialogEnd)
    {
        if (dialogUI == null)
        {
            Debug.LogWarning("DialogUI is null!");
            onDialogEnd?.Invoke();
            return;
        }

        // สั่ง DialogUI ให้เริ่มบทสนทนา โดยใช้ npcData.currentemotion เป็น dialogName
        dialogUI.StartDialog(npcData.currentemotion, dialogIDs, onDialogEnd);
    }
}
