using System.Collections;
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

    [Header("Animator Controller")]
    public AnimatorController animator;

    [Header("Spawn Position")]
    public Vector3 spawnPosition = new Vector3(0, 0, 0);

    [Header("Audio Settings")]
    public AudioSource audioSource; // AudioSource สำหรับเสียงดีเลย์
    public AudioClip npcDestroyedClip; // เสียงเมื่อ NPC ถูกทำลาย
    public AudioClip spawnNewNPCClip; // เสียงเมื่อสร้าง NPC ใหม่

    [Header("Delay Settings")]
    public float delayBeforeSpawn = 1f; // เวลาหน่วงก่อนสร้าง NPC ใหม่ (วินาที)

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
                Debug.Log("NPCSpawner received OnNPCDestroyed -> Starting spawn sequence...");
                StartCoroutine(SpawnSequence());
            };
        }

        Debug.Log("SpawnNewNPC: NPC spawned successfully with randomData = " + randomData.characterName);
    }

    /// <summary>
    /// Coroutine สำหรับจัดการดีเลย์และเล่นเสียงก่อนสร้าง NPC ใหม่
    /// </summary>
    private IEnumerator SpawnSequence()
    {
        // เล่นเสียงเมื่อ NPC ถูกทำลาย
        if (audioSource != null && npcDestroyedClip != null)
        {
            audioSource.PlayOneShot(npcDestroyedClip);
            Debug.Log("Playing NPC Destroyed Sound");
        }

        // หน่วงเวลาตามที่กำหนด
        yield return new WaitForSeconds(delayBeforeSpawn);

        // เล่นเสียงก่อนสร้าง NPC ใหม่
        if (audioSource != null && spawnNewNPCClip != null)
        {
            audioSource.PlayOneShot(spawnNewNPCClip);
            Debug.Log("Playing Spawn New NPC Sound");
        }

        // หน่วงเวลาเพิ่มเติมเล็กน้อยเพื่อให้เสียงก่อนสร้างเล่นครบ
        yield return new WaitForSeconds(0.5f);

        // สร้าง NPC ใหม่
        SpawnNewNPC();
    }
}
