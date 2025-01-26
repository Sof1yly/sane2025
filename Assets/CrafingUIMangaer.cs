using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrafingUIMangaer : MonoBehaviour
{
    [Header("Game Settings")]
    public int score = 0;
    public int wrongCount = 0;

    [Header("Game Over Settings")]
    [Tooltip("UI GameObject ที่จะแสดงเมื่อจบเกม")]
    public GameObject gameOverUI;

    // ตัวแปรเพื่อป้องกันการเรียก EndGame หลายครั้ง
    private bool gameEnded = false;

    // เพิ่มเมธอดช่วย
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score is now: " + score);
    }

    public void AddWrong(int amount)
    {
        wrongCount += amount;
        Debug.Log("WrongCount is now: " + wrongCount);

        if (wrongCount > 3 && !gameEnded)
        {
            EndGame();
        }
    }

    /// <summary>
    /// เมธอดสำหรับจบเกม
    /// </summary>
    private void EndGame()
    {
        gameEnded = true;
        Debug.Log("Game Over! WrongCount exceeded 3.");

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
        else
        {
            Debug.LogError("GameOverUI is not assigned in the Inspector.");
        }

        // หยุดเกมโดยการตั้ง Time.timeScale เป็น 0
        Time.timeScale = 0f;
        StopAllSounds();
    }
    private void StopAllSounds()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in audioSources)
        {
            audio.Stop();
        }
        Debug.Log("All sounds have been stopped.");
    }

    private void Update()
    {
        // ตรวจสอบถ้ากดปุ่ม "R" (Restart)
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
    public void RestartGame()
    {
        // รีโหลด Scene ปัจจุบัน
        SceneManager.LoadSceneAsync(0);
        Debug.Log("Restarting the game...");
    }
}
