using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New weight item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    public ItemType ItemName;
    public Sprite ItemImage;
    public int ItemWeight;
    public bool Weight;
}

public enum ItemType
{
    Apple, 
    Cantaloupe, 
    Citrus, 
    Grapes, 
    Herbs, 
    Olives, 
    Onion, 
    Orange, 
    Pomegranate, 
    Potato, 
    Watermelon, 
    Weight1to10,
    Weight20to100,
    Weight500to1000,
    Weight5000to10000,
}