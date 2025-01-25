using System.Collections;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [Header("NPC Prefab")]
    public GameObject npcPrefab; // Prefab ที่มี NPC.cs

    [Header("NPC Data")]
    public NPCData npcData;

    [Header("Dialog UI")]
    public DialogUI dialogUI; // อ้างอิง DialogUI ใน Scene

    [Header("Spawn Settings")]
    public int spawnCount = 1;
    public Vector3 spawnPosition = new Vector3(0, 0, 0);

    private void Start()
    {
        StartCoroutine(SpawnNPCs());
    }

    private IEnumerator SpawnNPCs()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // 1) สร้าง NPC
            GameObject npcObject = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);

            // 2) เรียก Init พร้อมส่ง DialogUI
            NPC npcComponent = npcObject.GetComponent<NPC>();
            if (npcComponent != null)
            {
                npcComponent.Init(npcData, dialogUI);
            }

            // หน่วงนิดหน่อย
            yield return new WaitForSeconds(1f);
        }

        Debug.Log("Spawned all NPCs complete!");
    }
}
