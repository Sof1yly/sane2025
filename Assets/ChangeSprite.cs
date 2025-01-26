using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    [Header("Sprites List")]
    [Tooltip("เรียงลำดับสไปร์ต์ตามหมายเลขที่ต้องการ เช่น index 0 = สไปร์ต์หมายเลข 1, index 1 = สไปร์ต์หมายเลข 2")]
    public Sprite[] sprites; // Array ของสไปร์ต์ที่คุณต้องการเปลี่ยน
    public CrafingUIMangaer scoremanger;

    private SpriteRenderer spriteRenderer;

    [Header("UI Image")]
    public Image uiImage;

    private void Awake()
    {
        // ดึงคอมโพเนนต์ SpriteRenderer ที่แนบอยู่กับ GameObject นี้
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteChanger: ไม่พบ SpriteRenderer บน GameObject นี้.");
        }
    }
    private void Update()
    {
        if (scoremanger.wrongCount >= sprites.Length)
        {
            uiImage.sprite = sprites[scoremanger.wrongCount];
        }
    }
}

