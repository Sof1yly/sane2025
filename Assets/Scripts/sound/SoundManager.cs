using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Background Music Clips")]
    public AudioClip[] musicClips; // เก็บเพลง 6 เพลง

    [Header("Settings")]
    public float delayBetweenSongs = 1f; // เวลาหน่วงระหว่างเพลง (วินาที)

    private AudioSource audioSource;
    private int currentClipIndex = 0;

    private void Awake()
    {
        // ทำให้ MusicManager เป็น Singleton เพื่อไม่ให้ถูกทำลายเมื่อเปลี่ยน Scene
        DontDestroyOnLoad(gameObject);

        // หา AudioSource ในตัวเอง
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("MusicManager: No AudioSource found!");
        }

        // เริ่ม Coroutine สำหรับเล่นเพลง
        if (musicClips.Length > 0)
        {
            StartCoroutine(PlayMusicSequence());
        }
        else
        {
            Debug.LogWarning("MusicManager: No music clips assigned!");
        }
    }

    private IEnumerator PlayMusicSequence()
    {
        while (true) // วนลูปเรื่อย ๆ
        {
            // ตั้งค่าเพลงปัจจุบัน
            audioSource.clip = musicClips[currentClipIndex];
            audioSource.Play();

            Debug.Log($"MusicManager: Playing clip {currentClipIndex + 1} - {musicClips[currentClipIndex].name}");

            // รอจนกว่าเพลงจะเล่นจบ
            yield return new WaitForSeconds(audioSource.clip.length);

            // หน่วงเวลาก่อนเล่นเพลงถัดไป
            yield return new WaitForSeconds(delayBetweenSongs);

            // เลื่อน index ไปเพลงถัดไป
            currentClipIndex++;
            if (currentClipIndex >= musicClips.Length)
            {
                currentClipIndex = 0; // รีเซ็ตกลับไปเพลงแรก
            }
        }
    }
}
