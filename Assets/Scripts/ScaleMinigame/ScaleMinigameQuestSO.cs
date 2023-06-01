using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scale Quest", menuName = "Puzzles/ScaleMinigameQuest")]
public class ScaleMinigameQuestSO : ScriptableObject
{
    [TextArea(2, 4)]
    public string Description;
    public List<InventoryWeightedItem> QuestPlayerInventory = new List<InventoryWeightedItem>();
    public ScaleQuestType QuestType;

    public List<ItemSO> PlaceTheseProduce;

    public ItemSO MerchantTradeItem;
    public TradeRatio[] MerchantTradeRatios;

    [Header("Leave empty for no dialog")]
    public ConversationSO QuestStartDialogue;

    public enum ScaleQuestType
    {
        PlaceProduce,
        PlaceWeights,
        FreeTrading,
    }
}

// ratios checked by int overriding floats? get ratios with modulo?
[System.Serializable]
public class TradeRatio
{
    public ItemSO Item;
    public float Ratio;
}