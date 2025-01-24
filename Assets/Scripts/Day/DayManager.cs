using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    private Day day;
    private void Start()
    {
        // Initialize day
        day = new Day();
        day.Load(); // Load saved state if available

        // Log initial state
        Debug.Log($"Day {day.day}, Money: {day.money}, Mail: {day.mail}, Upgrade: {day.currentUpgrade}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
