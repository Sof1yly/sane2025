using UnityEngine;

public class NPC : MonoBehaviour
{
    [Header("Assigned NPC Data after spawn")]
    [SerializeField] private NPCData npcData;

    [Header("References")]
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    // ** ตัวแปรสำหรับเก็บอารมณ์ของ NPC แต่ละตัว (ไม่เกี่ยวกับ ScriptableObject แล้ว) **
    private string currentemotion;

    /// <summary>
    /// เรียกใช้หลัง Instantiate เพื่อเซ็ต Data ให้ NPC
    /// </summary>
    public void Init(NPCData data)
    {
        npcData = data;

        // 1) สุ่ม emotion จาก npcData.emotion
        if (npcData.emotion != null && npcData.emotion.Length > 0)
        {
            int randIndex = Random.Range(0, npcData.emotion.Length);
            currentemotion = npcData.emotion[randIndex];
        }
        else
        {
            currentemotion = "Neutral";
        }

        // 2) ตั้งชื่อ GameObject ให้เห็นชัด (เช่น NPC_Poring_Happy)
        gameObject.name = "NPC_" + npcData.characterName + "_" + currentemotion;

        // 3) ตั้งค่า Sprite, Animator
        if (spriteRenderer != null && npcData.characterSprite != null)
        {
            spriteRenderer.sprite = npcData.characterSprite;
        }

        if (animator != null && npcData.animatorCtrl != null)
        {
            animator.runtimeAnimatorController = npcData.animatorCtrl;

            // ถ้าอยากให้ animator เล่นท่าทางตรงตาม emotion ด้วย อาจใช้ trigger
            // animator.SetTrigger(currentemotion);
        }

        // ทดสอบ
        Debug.Log($"[{gameObject.name}] emotion = {currentemotion}");
    }

    // ตัวอย่าง Getter เผื่อภายนอกอยากรู้ว่า NPC ตัวนี้กำลังมี emotion อะไร
    public string GetCurrentEmotion()
    {
        return currentemotion;
    }
}
