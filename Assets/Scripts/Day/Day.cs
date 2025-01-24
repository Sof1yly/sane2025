using System;
using UnityEngine;

[System.Serializable]
public class Day
{
    public int day;              // Current day
    public int money;            // Current money
    public int mail;             // Number of mails
    public Upgrade currentUpgrade; // Current upgrade

    // Enum for upgrades
    public enum Upgrade
    {
        Stocks,
        Hints,
        RateRewards,
        LowerTheCost,
        MoreCust,
        MorePenalty
    }

    // Constructor
    public Day(int startDay = 1, int startMoney = 0, int startMail = 0, Upgrade startUpgrade = Upgrade.Stocks)
    {
        day = startDay;
        money = startMoney;
        mail = startMail;
        currentUpgrade = startUpgrade;
    }

    // Update day attributes
    public void UpdateDay(int moneyChange, int mailChange, Upgrade? upgradeChange = null)
    {
        money += moneyChange;
        mail += mailChange;

        if (upgradeChange.HasValue)
        {
            currentUpgrade = upgradeChange.Value;
        }

        Debug.Log($"Day updated: Day {day}, Money: {money}, Mail: {mail}, Upgrade: {currentUpgrade}");
    }

    // Save data to PlayerPrefs
    public void Save()
    {
        PlayerPrefs.SetInt("Day", day);
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("Mail", mail);
        PlayerPrefs.SetInt("Upgrade", (int)currentUpgrade);
        PlayerPrefs.Save();
        Debug.Log("Game saved.");
    }

    // Load data from PlayerPrefs
    public void Load()
    {
        day = PlayerPrefs.GetInt("Day", 1); // Default to day 1 if not found
        money = PlayerPrefs.GetInt("Money", 0); // Default to 0 money
        mail = PlayerPrefs.GetInt("Mail", 0); // Default to 0 mail
        currentUpgrade = (Upgrade)PlayerPrefs.GetInt("Upgrade", 0); // Default to Upgrade.Stocks
        Debug.Log($"Game loaded: Day {day}, Money: {money}, Mail: {mail}, Upgrade: {currentUpgrade}");
    }

    // Proceed to the next day
    public void NextDay()
    {
        day++;
        mail = 0; // Reset mail for the new day (optional)
        Debug.Log($"Next day: Day {day}. Money: {money}, Mail reset.");
    }
}
