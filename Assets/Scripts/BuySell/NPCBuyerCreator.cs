//using UnityEngine;
//using TMPro;

//public class NPCBuyerCreator : MonoBehaviour
//{
//    public BuyerData[] buyerDataArray;  // Array of BuyerData ScriptableObjects
//    public GameObject npcPrefab;        // Prefab to instantiate for each NPC
//    public Transform parentTransform;   // Optional: Parent to organize created NPCs in the hierarchy

//    private void Start()
//    {
//        CreateNPCBuyers();
//    }

//    private void CreateNPCBuyers()
//    {
//        foreach (var buyerData in buyerDataArray)
//        {
//            // Instantiate the prefab
//            GameObject npcObject = Instantiate(npcPrefab, parentTransform);

//            // Set the name of the GameObject
//            npcObject.name = buyerData.npcName;

//            // Set the sprite (SpriteRenderer or UI Image)
//            var spriteRenderer = npcObject.GetComponent<SpriteRenderer>();
//            if (spriteRenderer != null)
//            {
//                spriteRenderer.sprite = buyerData.npcSprite;
//            }

//            // Set the animation (Animator)
//            var animator = npcObject.GetComponent<Animator>();
//            if (animator != null)
//            {
//                animator.runtimeAnimatorController = buyerData.npcAnimation;
//            }

//            // Set the TextMeshPro text for the dialog (if needed)
//            var textMeshPro = npcObject.GetComponentInChildren<TextMeshProUGUI>();
//            if (textMeshPro != null)
//            {
//                // You can choose how to display the buyer's dialog or other info
//                textMeshPro.text = buyerData.dialog.Length > 0 ? buyerData.dialog[0] : "No dialog";
//            }

//            // Additional setup: Reward and other attributes can be logged or displayed
//            Debug.Log($"Created NPC: {buyerData.npcName}, Reward: {buyerData.reward}, Dialog: {buyerData.dialog.Length} lines");

//            // Initialize Buyer instance and set it up
//            Buyer buyer = new Buyer(
//                buyerData.npcName,              // Set NPC name
//                buyerData.npcSprite,            // Set NPC sprite
//                buyerData.npcAnimation,         // Set NPC animation
//                buyerData.reward,               // Set reward
//                buyerData.answer,               // Set answer
//                buyerData.dialog           // Set dialog lines
//            );

//            // Optionally, you can pass the Buyer data to another component for further handling
//            //NPCBuyer npcBuyer = npcObject.GetComponent<NPCBuyer>();
//            //if (npcBuyer != null)
//            //{
//            //    npcBuyer.InitializeBuyer(buyer);
//            //}
//        }
//    }
//}
