using UnityEditor.Animations;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [Header("NPC Prefab")]
    public GameObject npcPrefab; // Prefab ที่มี NPC.cs

    [Header("NPC Data List (Random Pick)")]
    public NPCData[] npcDataList; // <-- เก็บ NPCData หลายตัว

    [Header("Dialog UI")]
    public DialogUI dialogUI; // อ้างอิง DialogUI ใน Scene
    public GameObject craftUI;
    public AnimatorController animator;

    [Header("Spawn Position")]
    public Vector3 spawnPosition = new Vector3(0, 0, 0);

    private void Start()
    {
        // เริ่มสร้าง 1 ตัว (หรือจะเรียกได้เรื่อย ๆ)
        SpawnNewNPC();
    }

    public void SpawnNewNPC()
    {
        // 1) สุ่มเลือก NPCData จาก npcDataList
        if (npcDataList == null || npcDataList.Length == 0)
        {
            Debug.LogWarning("npcDataList is empty! Cannot spawn NPC with random data.");
            return;
        }
        int randIndex = Random.Range(0, npcDataList.Length);
        NPCData randomData = npcDataList[randIndex];

        // 2) สร้าง NPC จาก Prefab
        GameObject npcObject = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);

        // 3) ตั้งค่า
        NPC npcComponent = npcObject.GetComponent<NPC>();
        if (npcComponent != null)
        {
            // ส่ง randomData แทนที่จะเป็น npcData
            npcComponent.Init(randomData, dialogUI, craftUI);

            // (ถ้าใช้ Callback ให้ Spawn อีกตัวเมื่อ NPC ถูกทำลาย)
            npcComponent.OnNPCDestroyed = () =>
            {
                Debug.Log("NPCSpawner received OnNPCDestroyed -> Spawning the next NPC...");
                SpawnNewNPC();
            };
        }

        Debug.Log("SpawnNewNPC: NPC spawned successfully with randomData = " + randomData.characterName);
    }
}
