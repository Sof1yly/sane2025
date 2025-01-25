using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [Header("NPC Prefab")]
    public GameObject npcPrefab;  // Prefab ที่มีคอมโพเนนต์ NPC อยู่

    // ฟังก์ชันสำหรับสร้าง NPC ใหม่ในตำแหน่งที่กำหนด
    public void CreateNPC(NPCData data, Vector3 spawnPosition)
    {
        if (npcPrefab == null)
        {
            Debug.LogError("NPC Prefab is not assigned!");
            return;
        }

        // สร้าง GameObject ใหม่จาก Prefab
        GameObject newNPCObject = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);

        // เข้าถึงคอมโพเนนต์ NPC
        NPC npcComponent = newNPCObject.GetComponent<NPC>();
        if (npcComponent != null)
        {
            // เรียก Init เพื่อเซ็ต Data และอัปเดต sprite/animator 
            npcComponent.Init(data);
        }
        else
        {
            Debug.LogWarning("The spawned prefab does not have an NPC component!");
        }
    }

    // ตัวอย่างการทดสอบเรียกสร้าง
    [ContextMenu("Test Spawn NPC")]
    public void TestSpawn()
    {
        // สมมติเรามี Data ใน Assets เช่น "TestNPCData"
        NPCData testData = Resources.Load<NPCData>("TestNPCData");
        if (testData != null)
        {
            CreateNPC(testData, new Vector3(0, 0, 0));
        }
        else
        {
            Debug.LogWarning("Can't find TestNPCData in Resources folder!");
        }
    }
}
