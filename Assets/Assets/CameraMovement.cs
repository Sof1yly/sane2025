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
        // ���˹觢ͧ�������š 2D
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // �ӹǳ���˹���������᡹ X ��� Y
        float targetX = Mathf.Clamp(mousePosition.x, minX, maxX);
        float targetY = Mathf.Clamp(mousePosition.y, minY, maxY); // �ӡѴ᡹ Y �������㹪�ǧ minY �֧ maxY

        // ���˹�����ͧ���ͧ (᡹ Z �����)
        Vector3 targetPosition = new Vector3(targetX, targetY, transform.position.z);

        // �� Lerp ��������͹���Ẻ Smooth
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}
