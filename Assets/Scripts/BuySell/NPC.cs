using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private NPCData npcData; // อ้างอิง Data

    [Header("Scene References")]
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    // เรียกใช้เมื่อเราสร้าง NPC ตัวใหม่ 
    // เพื่อกำหนด npcData และอัปเดต sprite/animator
    public void Init(NPCData data)
    {
        npcData = data;

        // ตั้งชื่อ GameObject เผื่อดูง่ายใน Hierarchy
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

        // ถ้ามี Logic เกี่ยวกับ emotion หรือ score
        // ก็สามารถอัปเดตได้ตรงนี้ เช่น
        // animator.SetTrigger(npcData.emotion);
    }

    // ตัวอย่างฟังก์ชันเรียกเปิดบทสนทนา
    public void TriggerDialog(DialogUI dialogUI)
    {
        if (dialogUI == null)
        {
            Debug.LogWarning("DialogUI is null!");
            return;
        }

        int[] dialogIDs = { 1, 2};

        // เรียก StartDialog โดยส่งชื่อ NPC จาก npcData 
        dialogUI.StartDialog(npcData.characterName, dialogIDs);
    }
}
