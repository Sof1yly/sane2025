using UnityEngine;
using System.Collections.Generic;

public class BubbleCombiner : MonoBehaviour
{
    [Header("References to our two crafting slots (auto-found in Start if null)")]
    public CraftingSlot slotA;
    public CraftingSlot slotB;

    [Header("Optional - A slot to show the result (auto-found in Start if null)")]
    public CraftingSlot resultSlot;

    [Header("All possible bubble recipes")]
    public BubbleRecipe[] bubbleRecipes;

    [Header("Images to Hide after confirm (auto-found in Start if null)")]
    public GameObject imageToHide1;
    public GameObject imageToHide2;
    public GameObject imageConfirm;
    public GameObject allUIcom;


    private void Start()
    {
        if (imageToHide1 != null) imageToHide1.SetActive(true);
        if (imageToHide2 != null) imageToHide2.SetActive(true);
        if (imageConfirm != null) imageConfirm.SetActive(true);
        // If any of these references are missing, try to find them by name in children:
        if (slotA == null)
        {
            Transform child = transform.Find("SlotA");
            if (child != null)
                slotA = child.GetComponent<CraftingSlot>();
        }

        if (slotB == null)
        {
            Transform child = transform.Find("SlotB");
            if (child != null)
                slotB = child.GetComponent<CraftingSlot>();
        }

        if (resultSlot == null)
        {
            Transform child = transform.Find("ResultSlot");
            if (child != null)
                resultSlot = child.GetComponent<CraftingSlot>();
        }

        if (imageToHide1 == null)
        {
            Transform child = transform.Find("ImageToHide1");
            if (child != null)
                imageToHide1 = child.gameObject;
        }

        if (imageToHide2 == null)
        {
            Transform child = transform.Find("ImageToHide2");
            if (child != null)
                imageToHide2 = child.gameObject;
        }

        if (imageConfirm == null)
        {
            Transform child = transform.Find("ImageConfirm");
            if (child != null)
                imageConfirm = child.gameObject;
        }
    }

    // Called by the "Confirm" button
    public void ConfirmCraft()
    {
        // 1. Grab the current bubbles in each slot
        BubbleData bubbleA = slotA.currentBubble;
        BubbleData bubbleB = slotB.currentBubble;

        // 2. Make sure both slots have something
        if (bubbleA == null || bubbleB == null)
        {
            Debug.LogWarning("Cannot craft: one or both slots are empty!");
            return;
        }

        // 3. Try to find a matching recipe among our array of recipes
        bool foundMatch = false;

        foreach (BubbleRecipe recipe in bubbleRecipes)
        {
            // Check if they match the inputs of this recipe (in any order)
            bool matchNormal = (bubbleA == recipe.input1 && bubbleB == recipe.input2);
            bool matchReverse = (bubbleA == recipe.input2 && bubbleB == recipe.input1);

            if (matchNormal || matchReverse)
            {
                Debug.Log($"Success! Crafting new bubble: {recipe.result.bubbleName}");

                // Place the result in the resultSlot (if assigned)
                if (resultSlot != null)
                {
                    resultSlot.SetBubble(recipe.result);
                }
                else
                {
                    Debug.LogWarning("No result slot assigned. The new bubble won't be shown in the UI!");
                }

                // Clear out the inputs
                slotA.ClearSlot();
                slotB.ClearSlot();

                // [Optional] Hide the specified images/objects
                if (imageToHide1 != null) imageToHide1.SetActive(false);
                if (imageToHide2 != null) imageToHide2.SetActive(false);
                if (imageConfirm != null) imageConfirm.SetActive(false);
                if (allUIcom != null) allUIcom.SetActive(false);

                foundMatch = true;
                break;  // Stop searching once we find the first valid recipe
            }
        }

        // If we finish the loop without finding a match, log a message
        if (!foundMatch)
        {
            Debug.Log("No matching recipe found for these two bubbles!");
        }
    }
}
