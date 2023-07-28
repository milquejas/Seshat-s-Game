using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fruit Basket Puzzle", menuName = "Puzzles/FruitBasketPuzzle")]
public class FruitBasketQuestSO : ScriptableObject
{
    public ConversationSO StartingConversation;
    public List<InventoryWeightedItem> BasketItems;
    public int WeightLimit;
    public int ValueGoal;
    public bool QuestHasGrapes;
}
