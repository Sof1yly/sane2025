using System.Collections;
using UnityEngine;
using System;

public class NPCSpawner : MonoBehaviour
{
    [Header("NPC Prefab")]
    public GameObject npcPrefab;  // Prefab ที่มีคอมโพเนนต์ NPC อยู่

    [Header("NPC Data")]
    public NPCData npcData;       // Data ต้นแบบ

    [Header("DialogUI Reference")]
    public DialogUI dialogUI;     // อ้างอิงถึง DialogUI ในซีน

    [Header("Settings")]
    public int spawnCount = 4;    // ต้องการ Spawn กี่ตัว
    public Vector3 spawnPosition; // ตำแหน่งที่จะ Spawn

    // เริ่มขั้นตอน Spawn NPC แบบ Coroutine
    private void Start()
    {
        StartCoroutine(SpawnNPCSequence());
    }

    private IEnumerator SpawnNPCSequence()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // 1) สุ่ม emotion จาก npcData.emotion แล้วเซ็ตเป็น currentemotion
            if (npcData.emotion != null && npcData.emotion.Length > 0)
            {
                int randIndex = UnityEngine.Random.Range(0, npcData.emotion.Length);
                npcData.currentemotion = npcData.emotion[randIndex];
            }
            else
            {
                npcData.currentemotion = "Neutral"; // ถ้าไม่มี emotion ใน array
            }

            // 2) สร้าง NPC จาก Prefab
            GameObject npcObj = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
            NPC npcComp = npcObj.GetComponent<NPC>();
            Debug.Log("Create");

            // 3) เซ็ตข้อมูล NPC
            if (npcComp != null)
            {
                npcComp.Init(npcData);

                // 4) เรียก TriggerDialog พร้อม Callback ว่าจบแล้ว
                bool dialogEnded = false;

                npcComp.TriggerDialog(dialogUI, new int[] { 1 }, () =>
                {
                    // Callback เมื่อ EndDialog() เรียกใช้
                    dialogEnded = true;
                });

                // 5) รอจนกว่าบทสนทนาจะจบ
                yield return new WaitUntil(() => dialogEnded);

                // 6) ทำลาย NPC ตัวนี้
                Destroy(npcObj);
                Debug.Log("Destoy");
            }
            else
            {
                Debug.LogWarning("No NPC component found on the spawned prefab!");
            }

            // (ตัวอย่าง) หน่วงเวลาเล็กน้อยก่อนสร้างตัวถัดไป (ถ้าต้องการ)
            yield return new WaitForSeconds(0.2f);
        }

        Debug.Log("NPC spawning loop finished!");
    }
}
