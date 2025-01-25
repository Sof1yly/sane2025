using UnityEngine;

[System.Serializable]
public class NPCData : ScriptableObject
{
    public string npcName;                     // Name of the NPC
    public Sprite npcSprite;                   // Sprite for the NPC
    public RuntimeAnimatorController npcAnimation; // Animation controller
}