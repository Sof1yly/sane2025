//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class MainUI : MonoBehaviour
//{
//    public Day dayData;                     // Reference to the Day data
//    public DialogData dialogData;           // Reference to the DialogData
//    public GameObject inventoryUI;          // Reference to the Inventory UI            
//    public GameObject barUI;                // Reference to the Bar UI
//    public GameObject upgradeUI;            // Reference to the Upgrade UI
//    public GameObject buttonMenu;           // Reference to the Button Menu
//    public TMP_Text dayNightText;           // UI Text for Day/Night status
//    public TMP_Text moneyText;              // UI Text for Money
//    public TMP_Text mailText;               // UI Text for Mail
//    public TMP_Text dialogText;             // UI Text for Dialog
//    public Image bubbleImage;               // Image for displaying bubbles

//    private bool isDay = true;              // Tracks whether it's day or night

//    private void Start()
//    {
//        UpdateDayUI();
//    }

//    // Ends the current day and transitions to the next
//    public void EndDay()
//    {
//        dayData.NextDay();
//        isDay = !isDay; // Toggle Day/Night
//        UpdateDayUI();
//        Debug.Log($"Day ended. Now it's {(isDay ? "Day" : "Night")}, Day {dayData.day}.");
//    }

//    // Updates UI elements to reflect the current day, money, and mail
//    private void UpdateDayUI()
//    {
//        dayNightText.text = isDay ? "Daytime" : "Nighttime";
//        moneyText.text = $"Money: {dayData.money}";
//        mailText.text = $"Mail: {dayData.mail}";
//    }

//    // Shows a dialog from an NPC
//    public void ShowDialog(int dialogIndex)
//    {
//        if (dialogIndex < 0 || dialogIndex >= dialogData.dialogEntries.Length)
//        {
//            Debug.LogWarning("Invalid dialog index.");
//            return;
//        }

//        var entry = dialogData.dialogEntries[dialogIndex];
//        dialogText.text = $"{entry.npc.npcName}: {entry.dialogText}";
//        Debug.Log($"Dialog: {entry.npc.npcName} says \"{entry.dialogText}\".");
//    }

//    // Displays a bubble on the UI
//    public void ShowBubble(BubbleData bubble)
//    {
//        if (bubble == null)
//        {
//            Debug.LogWarning("Bubble data is null.");
//            return;
//        }

//        bubbleImage.sprite = bubble.bubbleImage;
//        Debug.Log($"Displaying bubble: {bubble.bubbleName}");
//    }

//    // Toggles the Button Menu UI
//    public void ToggleButtonMenu()
//    {
//        buttonMenu.SetActive(!buttonMenu.activeSelf);
//        Debug.Log("Toggled Button Menu.");
//    }

//    // Toggles the Inventory UI
//    public void ToggleInventoryUI()
//    {
//        inventoryUI.SetActive(!inventoryUI.activeSelf);
//        Debug.Log("Toggled Inventory UI.");
//    }
  

//    // Toggles the Bar UI
//    public void ToggleBarUI()
//    {
//        barUI.SetActive(!barUI.activeSelf);
//        Debug.Log("Toggled Bar UI.");
//    }

//    // Toggles the Upgrade UI
//    public void ToggleUpgradeUI()
//    {
//        upgradeUI.SetActive(!upgradeUI.activeSelf);
//        Debug.Log("Toggled Upgrade UI.");
//    }

//    // Handles Day/Night transitions
//    public void ToggleDayNight()
//    {
//        isDay = !isDay;
//        dayNightText.text = isDay ? "Daytime" : "Nighttime";
//        Debug.Log($"Switched to {(isDay ? "Daytime" : "Nighttime")}.");
//    }
//}
