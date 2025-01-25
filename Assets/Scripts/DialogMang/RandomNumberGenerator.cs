using System;
using UnityEngine;
using Random = System.Random;

public class RandomNumberGenerator
{
    // ฟังก์ชันสุ่มเลขจาก int array
    public int GetRandomNumber(int[] numbers)
    {
        if (numbers == null || numbers.Length == 0)
        {
            Debug.LogError("Input array is null or empty.");
            return -1; // คืนค่าผิดปกติเมื่ออาร์เรย์ว่าง
        }

        Random random = new Random();
        int randomIndex = random.Next(numbers.Length); // สุ่มเลือกดัชนีจากอาร์เรย์
        return numbers[randomIndex]; // คืนค่าที่สุ่มได้
    }
}
