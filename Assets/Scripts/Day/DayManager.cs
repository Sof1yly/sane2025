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
        // Example inputs to demonstrate functionality
        if (Input.GetKeyDown(KeyCode.U))
        {
            day.UpdateDay(100, 2, Day.Upgrade.Hints); // Example update with Hints upgrade
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            day.Save(); // Save the current state
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            day.Load(); // Load the saved state
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            day.NextDay(); // Go to the next day
        }
    }
}
