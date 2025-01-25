using UnityEngine;

[CreateAssetMenu(fileName = "NewBubbleRecipe", menuName = "ScriptableObjects/BubbleRecipe", order = 2)]
public class BubbleRecipe : ScriptableObject
{
    public BubbleData input1;
    public BubbleData input2;
    public BubbleData result;
}
