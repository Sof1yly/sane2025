using UnityEngine;
using TMPro;

[System.Serializable]
public class Bubbles
{
    public string name;         // Name of the bubble
    public Sprite image;        // Image (Sprite) of the bubble
    public TextMeshPro text;    // Text (TextMeshPro) associated with the bubble
    public int tiers;           // Tier level of the bubble
    public int price;           // Price of the bubble

    // Constructor to initialize a Bubble instance
    public Bubbles(string name, Sprite image, TextMeshPro text, int tiers, int price)
    {
        this.name = name;
        this.image = image;
        this.text = text;
        this.tiers = tiers;
        this.price = price;
    }
}
