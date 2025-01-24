using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Buyer : NPC
{
    public int reward;               // Reward for buying
    public string answer;            // Buyer's answer
    public string hint;              // Hint provided by the buyer
    public string[] dialog;          // Dialog array

    // Method for the buyer to return the reward
    public int Buy()
    {
        Debug.Log($"Buyer {npcName} completed the purchase. Reward: {reward}");
        return reward;
    }
}
