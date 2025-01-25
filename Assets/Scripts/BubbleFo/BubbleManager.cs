using System.Collections.Generic;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    public List<BubbleData> allBubbles; // A list of all BubbleData assets

    // Get a BubbleData object by its name
    public BubbleData GetBubbleByName(string name)
    {
        foreach (BubbleData bubble in allBubbles)
        {
            if (bubble.bubbleName == name)
            {
                return bubble;
            }
        }

        Debug.LogWarning($"Bubble with name {name} not found!");
        return null;
    }

    // Get a random BubbleData object
    public BubbleData GetRandomBubble()
    {
        if (allBubbles.Count == 0)
        {
            Debug.LogWarning("No bubbles available in the manager!");
            return null;
        }

        return allBubbles[Random.Range(0, allBubbles.Count)];
    }

    // Check if a bubble exists by name
    public bool BubbleExists(string name)
    {
        foreach (BubbleData bubble in allBubbles)
        {
            if (bubble.bubbleName == name)
            {
                return true;
            }
        }

        return false;
    }

    //// Filter bubbles by tier
    //public List<BubbleData> GetBubblesByTier(int tier)
    //{
    //    List<BubbleData> result = new List<BubbleData>();

    //    foreach (BubbleData bubble in allBubbles)
    //    {
    //        if (bubble.bubbleTiers == tier)
    //        {
    //            result.Add(bubble);
    //        }
    //    }

    //    return result;
    //}
}
