using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scale Quest", menuName = "Puzzles/ScaleMinigameQuest")]
public class ScaleMinigameQuestSO : ScriptableObject
{
    [Header("Leave empty for no dialog")]
    public ConversationSO QuestStartDialogue;

    [TextArea(2, 4)]
    public string Description;
    public List<InventoryWeightedItem> QuestPlayerInventory;
    public ScaleQuestType QuestType;
    public bool LastQuestInRow;

    public List<ItemSO> PlaceTheseProduce;

    public ItemSO MerchantTradeItem;
    public TradeRatio[] MerchantTradeRatios;

    public enum ScaleQuestType
    {
        PlaceOnOneCup,
        PlaceBalanced,
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