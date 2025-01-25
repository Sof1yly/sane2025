using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float smoothSpeed = 5.0f; 
    public float offsetX = 0.0f; 
    public float minY = -5.0f; 
    public float maxY = 5.0f;

    public float minX = -5.0f;
    public float maxX = 5.0f;



    void Update()
    {
        // ตำแหน่งของเมาส์ในโลก 2D
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // คำนวณตำแหน่งเป้าหมายในแกน X และ Y
        float targetX = Mathf.Clamp(mousePosition.x, minX, maxX);
        float targetY = Mathf.Clamp(mousePosition.y, minY, maxY); // จำกัดแกน Y ให้อยู่ในช่วง minY ถึง maxY

        // ตำแหน่งใหม่ของกล้อง (แกน Z คงที่)
        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

        // ใช้ Lerp เพื่อเคลื่อนที่แบบ Smooth
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
