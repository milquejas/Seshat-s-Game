using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

/*
 * Setup item so it takes info from SO when enabled so it can be pooled
 * Spawning back to inventory if dropped outside the game. 
 * Animation when placed into inventory
 * Needs reference of coordinates of item slot in inventory? Or just fly into the middle of the inventory?
 * Draggable
*/

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class DraggableWeightedItem : MonoBehaviour, IInteractable
{
    [field: SerializeField] public bool InRange { get; set; }
    public ItemSO Item;

    [field: NonSerialized] public Rigidbody2D RBody;

    [field: NonSerialized] public SpriteRenderer itemImage;
    public ScaleMinigameInventoryItem originInventoryItem;

    private List<GameObject> listOfColliderChildren = new List<GameObject>();

    public Vector2 originPoolPosition;

    // awake runs before OnEnable?
    private void Awake()
    {
        if (listOfColliderChildren.Count == 0)
        {
            foreach (Transform child in transform)
            {
                listOfColliderChildren.Add(child.gameObject);
            }
        }
    }

    public Transform Interact()
    {
        return transform;
    }

    private void OnEnable()
    {
        itemImage = GetComponent<SpriteRenderer>();
        itemImage.sprite = Item.ItemImage;
        RBody = GetComponent<Rigidbody2D>();
        RBody.mass = Item.ItemWeight;
        itemImage.color = Color.white;

        //EnableItemCollider(true);
    }

    // also gravity disabled while collider disabled
    public void EnableItemCollider(bool toggle)
    {
        RBody.gravityScale = Convert.ToInt32(toggle);

        switch (Item.ItemName)
        {
            case ItemType.Apple:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Cantaloupe:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Citrus:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Grapes:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Herbs:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Olives:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Onion:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Orange:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Pomegranate:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Potato:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Watermelon:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            default:
                Debug.Log("case for item type collider not set?");
                break;
        }
    }
}
