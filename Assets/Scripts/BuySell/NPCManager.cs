//using UnityEngine;

//public class NPCManager : MonoBehaviour
//{
//    public Buyer[] buyers;     // Array of buyers
//    public Seller[] sellers;   // Array of sellers

//    private void Start()
//    {
//        // Example of a Buyer interaction
//        if (buyers.Length > 0)
//        {
//            Buyer buyer = buyers[0];
//            Debug.Log($"Buyer Name: {buyer.npcName}, Hint: {buyer.hint}");
//            Debug.Log($"Reward: {buyer.Buy()}");
//        }

//        // Example of a Seller interaction
//        if (sellers.Length > 0)
//        {
//            Seller seller = sellers[0];
//            Debug.Log($"Seller Name: {seller.npcName}, Piece: {seller.piece}");
//            Bubbles emotion = seller.Sell();
//            if (emotion != null)
//            {
//                Debug.Log($"Seller Emotion: {emotion.emotionName}");
//            }
//        }
//    }
//}
