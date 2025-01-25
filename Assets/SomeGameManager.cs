//using UnityEngine;

//public class SomeGameManager : MonoBehaviour
//{
//    public GameObject craftUIPrefab;
//    public BubbleCombiner bubbleCombiner; // The script in the scene

//    private GameObject currentCraftUI;

//    public void SpawnCraftUI()
//    {
//        // 1) Instantiate the prefab
//        currentCraftUI = Instantiate(craftUIPrefab, parentUITransform);

//        // 2) Find SlotA, SlotB, etc. in the new prefab instance.
//        //    (You can find by name, tag, or a custom script on the child.)
//        var slotAObject = currentCraftUI.transform.Find("SlotA");
//        var slotBObject = currentCraftUI.transform.Find("SlotB");
//        var resultSlotObject = currentCraftUI.transform.Find("ResultSlot");

//        // 3) Get the CraftingSlot components
//        CraftingSlot slotA = slotAObject.GetComponent<CraftingSlot>();
//        CraftingSlot slotB = slotBObject.GetComponent<CraftingSlot>();
//        CraftingSlot resultSlot = resultSlotObject.GetComponent<CraftingSlot>();

//        // 4) Tell the BubbleCombiner which slots to use
//        bubbleCombiner.slotA = slotA;
//        bubbleCombiner.slotB = slotB;
//        bubbleCombiner.resultSlot = resultSlot;
//    }
//}