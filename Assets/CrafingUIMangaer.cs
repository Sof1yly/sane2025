using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafingUIMangaer : MonoBehaviour
{
    // Start is called before the first frame update
    public int score = 0;
    public int wrongCount = 0;
    // เพิ่มเมธอดช่วย ถ้าต้องการ
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Score is now: " + score);
    }

    public void AddWrong(int amount)
    {
        wrongCount += amount;
        Debug.Log("WrongCount is now: " + wrongCount);
    }
}
