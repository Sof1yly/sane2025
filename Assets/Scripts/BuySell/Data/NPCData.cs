using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/NPCData")]
public class NPCData : ScriptableObject
{
    public string characterName;
    public Sprite characterSprite;
    public RuntimeAnimatorController animatorCtrl;
    public string[] emotion;
    public string currentemotion;
    public int score;
}