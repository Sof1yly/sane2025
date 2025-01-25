using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Assigned NPC Data")]
    [SerializeField] private NPCData npcData;
    
    [Header("References")]
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    private string currentemotion;
    private DialogUI attachedDialogUI;

    /// <summary>
    /// เรียกใช้หลัง Instantiate เพื่อเซ็ต Data และ DialogUI ให้ NPC
    /// </summary>
    public void Init(NPCData data, DialogUI dialogUI)
    {
        // เก็บไว้ในตัวแปรภายใน
        npcData = data;
        attachedDialogUI = dialogUI;

        // 1) สุ่ม emotion
        if (npcData.emotion != null && npcData.emotion.Length > 0)
        {
            int randIndex = Random.Range(0, npcData.emotion.Length);
            currentemotion = npcData.emotion[randIndex];
        }
        else
        {
            currentemotion = "Neutral";
        }

        // 2) ตั้งชื่อ GameObject เช่น NPC_Poring_Happy
        gameObject.name = "NPC_" + npcData.characterName + "_" + currentemotion;

        // 3) ตั้งค่า Sprite/Animator
        if (spriteRenderer != null && npcData.characterSprite != null)
        {
            spriteRenderer.sprite = npcData.characterSprite;
        }
        if (animator != null && npcData.animatorCtrl != null)
        {
            animator.runtimeAnimatorController = npcData.animatorCtrl;
            // animator.SetTrigger(currentemotion);
        }

        // 4) เรียก StartDialog() โดยใช้ currentemotion เป็น dialogName
        if (attachedDialogUI != null)
        {
            attachedDialogUI.StartDialog(currentemotion, new int[] {1, 2});
        }

        // (ถ้าอยาก Debug ดู)
        Debug.Log($"NPC [{gameObject.name}] spawned and called StartDialog({currentemotion})");
    }

    // เผื่อภายนอกอยากเช็ค emotion ปัจจุบัน
    public string GetCurrentEmotion()
    {
        return currentemotion;
    }
}
