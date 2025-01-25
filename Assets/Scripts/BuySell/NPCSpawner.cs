using System.Collections;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [Header("NPC Prefab")]
    public GameObject npcPrefab; // Prefab ที่มี NPC.cs อยู่ภายใน

    [Header("NPC Data")]
    public NPCData npcData;      // ScriptableObject เก็บข้อมูล

    [Header("Spawn Settings")]
    public int spawnCount = 4;
    public Vector3 spawnPosition = new Vector3(0, 0, 0);

    private void Start()
    {
        // เมื่อเกมเริ่ม สั่งให้ Spawn NPC ตามจำนวน
        StartCoroutine(SpawnNPCs());
    }

    private IEnumerator SpawnNPCs()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // 1) Instantiate NPC จาก Prefab
            GameObject npcObject = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);

            // 2) เรียก Init เพื่อให้ NPC จัดการสุ่ม emotion ของตัวเอง
            NPC npcComponent = npcObject.GetComponent<NPC>();
            if (npcComponent != null)
            {
                npcComponent.Init(npcData);
            }
            else
            {
                Debug.LogWarning("The spawned prefab does not have an NPC component!");
            }

            // ตัวอย่าง: หน่วงเวลานิดหน่อยก่อนสร้างตัวถัดไป
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("Spawned all NPCs complete!");
    }
}
