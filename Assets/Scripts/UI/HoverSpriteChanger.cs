using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HoverSpriteChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Sprite Settings")]
    [Tooltip("สไปร์ต์ที่จะแสดงเมื่อเมาส์ชี้")]
    public Sprite hoverSprite;

    [Tooltip("สไปร์ต์ที่จะแสดงในสภาพปกติ")]
    public Sprite normalSprite;

    private Image uiImage;

    private void Awake()
    {
        // ดึงคอมโพเนนต์ Image ที่แนบอยู่กับ GameObject นี้
        uiImage = GetComponent<Image>();

        if (uiImage == null)
        {
            Debug.LogError("HoverSpriteChanger: ไม่พบ Image component บน GameObject นี้.");
        }

        // กำหนดสไปร์ต์ปกติเป็นสไปร์ต์เริ่มต้น
        if (normalSprite != null)
        {
            uiImage.sprite = normalSprite;
        }
        else
        {
            Debug.LogWarning("HoverSpriteChanger: ยังไม่ได้กำหนด Normal Sprite.");
        }
    }

    /// <summary>
    /// เรียกใช้เมื่อเมาส์ชี้ที่ UI Image
    /// </summary>
    /// <param name="eventData">ข้อมูลเหตุการณ์</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSprite != null)
        {
            uiImage.sprite = hoverSprite;
            Debug.Log($"HoverSpriteChanger: เปลี่ยนสไปร์ต์เป็น {hoverSprite.name}");
        }
    }

    /// <summary>
    /// เรียกใช้เมื่อเมาส์ไม่ชี้ที่ UI Image
    /// </summary>
    /// <param name="eventData">ข้อมูลเหตุการณ์</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (normalSprite != null)
        {
            uiImage.sprite = normalSprite;
            Debug.Log($"HoverSpriteChanger: กลับสไปร์ต์เป็น {normalSprite.name}");
        }
    }
}
